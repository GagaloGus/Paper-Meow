using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] GameObject dialogueParent;
    [SerializeField] GameObject buttonHolder;
    [SerializeField] TMP_Text dialogueText;

    List<DialogueString> dialogueList;
    [SerializeField]List<GameObject> optionButtons;

    [SerializeField] float typingSpeed = 0.05f;
    [SerializeField] float turnSpeed = 2;

    [SerializeField] int currentDialogueIndex = 0;
    GameObject player;
    GameObject NPC;

    private void Start()
    {
        dialogueParent.SetActive(true);
        FindChilds();
        dialogueParent.SetActive(false);

        player = FindObjectOfType<SkoController>().gameObject;
    }

    void FindChilds()
    {
        dialogueText = dialogueParent.transform.Find("dial text").gameObject.GetComponent<TMP_Text>();
        buttonHolder = dialogueParent.GetComponentInChildren<GridLayoutGroup>().gameObject;

        for (int i = 0; i < buttonHolder.transform.childCount; i++)
        {
            optionButtons.Add(buttonHolder.transform.GetChild(i).gameObject);
        }
    }

    public void DialogueStart(List<DialogueString> textToPrint, GameObject NPC)
    {
        dialogueParent.SetActive(true);

        player.GetComponent<SkoController>().StartInteraction(NPC);
        this.NPC = NPC;

        dialogueList = textToPrint;
        currentDialogueIndex = 0;

        StartCoroutine(TurnNPCTowardsPlayer());
        StartCoroutine(PrintDialogue());

        buttonHolder.SetActive(false);
    }

    IEnumerator TurnNPCTowardsPlayer()
    {
        Quaternion startRot = NPC.transform.rotation;
        Quaternion targetRot = Quaternion.LookRotation(CoolFunctions.FlattenVector3(player.transform.position) - CoolFunctions.FlattenVector3(NPC.transform.position));

        float elapsedTime = 0;
        while (elapsedTime < 1)
        {
            NPC.transform.rotation = Quaternion.Slerp(startRot, targetRot, elapsedTime);
            elapsedTime += Time.deltaTime* turnSpeed;
            yield return null;
        }

        NPC.transform.rotation = targetRot;
    }


    bool optionSelected = false;
    IEnumerator PrintDialogue()
    {
        while(currentDialogueIndex < dialogueList.Count)
        {
            DialogueString line = dialogueList[currentDialogueIndex];

            line.startDialogueEvent?.Invoke();

            if (line.isQuestion)
            {
                yield return StartCoroutine(TypeText(line.text));

                buttonHolder.SetActive(true);

                int buttonAmountToEnable = 0;
                for (int i = 0; i < buttonHolder.transform.childCount; i++)
                {
                    if(buttonAmountToEnable < line.optionButtons.Count)
                    {
                        optionButtons[i].SetActive(true);
                        optionButtons[i].GetComponentInChildren<TMP_Text>().text =
                            line.optionButtons[i].answer;

                        int jumpIndex = line.optionButtons[i].jumpIndex;
                        optionButtons[i].GetComponent<Button>().onClick.AddListener(() =>
                        HandleOptionSelected(jumpIndex));
                    }
                    else
                    {
                        optionButtons[i].SetActive(false);
                    }

                    buttonAmountToEnable++;
                }

                yield return new WaitUntil(() => optionSelected);
            }
            else
            {
                yield return StartCoroutine(TypeText(line.text));
            }

            line.endDialogueEvent?.Invoke();

            optionSelected = false;

        }

        DialogueStop();
    }


    private void HandleOptionSelected(int indexJump)
    {
        optionSelected = true;
        buttonHolder.SetActive(false);

        currentDialogueIndex = indexJump;
    }

    IEnumerator TypeText(string text)
    {
        dialogueText.text = "";
        foreach(char letter in text.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        if (!dialogueList[currentDialogueIndex].isQuestion)
        {
            yield return new WaitUntil(() => Input.GetMouseButton(0));
        }

        if (dialogueList[currentDialogueIndex].isEnd)
        {
            DialogueStop();
        }

        currentDialogueIndex++;
    }

    private void DialogueStop()
    {
        StopAllCoroutines();
        dialogueText.text = "";

        dialogueParent.SetActive(false);
        NPC.GetComponent<Interactable>().enabled = true;
        player.GetComponent<SkoController>().EndInteraction();
    }
}
