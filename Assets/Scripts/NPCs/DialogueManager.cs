using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class DialogueManager : MonoBehaviour
{
    [Header("Canvas objects")]
    [SerializeField] GameObject dialogueParent; //el parent de todo
    [SerializeField] GameObject buttonHolder; //el grid que tiene de hijos a los botones de opciones
    [SerializeField] List<GameObject> optionButtons; // los botones de opciones
    [SerializeField] TMP_Text dialogueText; // texto de dialogo
    [SerializeField] TMP_Text nameText; //nombre del que esta hablando
    [SerializeField] Image talkIcon; //icono del que esta hablando

    List<DialogueString> dialogueList; //lista con los dialogos

    [SerializeField] float typingSpeed = 0.05f; //velocidad de escritura
    [SerializeField] float turnSpeed = 2; //velocidad de girarse

    [SerializeField] int currentDialogueIndex = 0; //dialogo en el que esta ahora mismo
    GameObject player;
    GameObject NPC;
    NPCData npcData;

    private void Start()
    {
        //coje todos los hijos del canvas y desactiva el padre
        dialogueParent.SetActive(true);
        FindChilds();
        dialogueParent.SetActive(false);

        player = FindObjectOfType<SkoController>().gameObject;
    }

    void FindChilds()
    {
        //coje todos los hijos necesarios
        dialogueText = dialogueParent.transform.Find("dial text").gameObject.GetComponent<TMP_Text>();
        nameText = dialogueParent.transform.Find("name").gameObject.GetComponent<TMP_Text>();
        talkIcon = dialogueParent.transform.Find("icon").gameObject.GetComponent<Image>();
        buttonHolder = dialogueParent.GetComponentInChildren<GridLayoutGroup>().gameObject;

        //Coje los hijos del grid de botones en orden
        for (int i = 0; i < buttonHolder.transform.childCount; i++)
        {
            optionButtons.Add(buttonHolder.transform.GetChild(i).gameObject);
        }
    }

    public void DialogueStart(List<DialogueString> textToPrint, NPCData npcData ,GameObject NPC)
    {
        //activa el cuadro de dialogo
        dialogueParent.SetActive(true);
        
        //manda la señal al script del player
        player.GetComponent<SkoController>().StartInteraction(NPC);

        //coje el npc que esta hablando
        this.NPC = NPC;
        this.npcData = npcData;

        //coje la lista de dialogos del npc
        dialogueList = textToPrint;
        currentDialogueIndex = 0;

        //empieza las corrutinas de girar y de escribir el texto
        StartCoroutine(TurnNPCTowardsTargetPos(player.transform.position));
        StartCoroutine(PrintDialogue());

        //desactiva los botones de opciones
        buttonHolder.SetActive(false);
    }



    bool optionSelected = false;
    IEnumerator PrintDialogue()
    {
        //mientras que no se hayan acabdo los dialogos
        while(currentDialogueIndex < dialogueList.Count)
        {
            //coje la linea en la que estamos
            DialogueString line = dialogueList[currentDialogueIndex];

            //Llama el evento de inicio, si tiene
            line.startDialogueEvent?.Invoke();

            //cambia el nombre del que habla
            nameText.text = (line.NPCTalks? npcData.name : line.newName);

            //cambia el icono
            if (line.NPCTalks && line.NPCIcon != null)
            {
                talkIcon.sprite = line.NPCIcon;
            }
            else if (!line.NPCTalks) 
            {
                talkIcon.sprite = line.newIcon;
            }

            //Escribe el texto en pantalla
            yield return StartCoroutine(TypeText(line.text));

            //Si es una pregunta
            if (line.isQuestion)
            {
                //activa los botones
                buttonHolder.SetActive(true);

                //funcion que activa cuantos botones le hayamos puesto en el dialogo en el inspector (maximo 4)
                //y les añade un listener para que salten a su respectiva linea al pulsarlos
                int buttonAmountToEnable = 0;
                for (int i = 0; i < buttonHolder.transform.childCount; i++)
                {
                    if(buttonAmountToEnable < line.optionButtons.Count)
                    {
                        optionButtons[i].SetActive(true);
                        optionButtons[i].GetComponentInChildren<TMP_Text>().text =
                            line.optionButtons[i].answer;

                        int jumpIndex = line.optionButtons[i].jumpIndex;

                        optionButtons[i].GetComponent<Button>().onClick.RemoveAllListeners();
                        optionButtons[i].GetComponent<Button>().onClick.AddListener(() =>
                        HandleOptionSelected(jumpIndex));
                    }
                    else
                    {
                        optionButtons[i].SetActive(false);
                    }

                    buttonAmountToEnable++;
                }

                //espera hasta que le demos a alguna opcion
                yield return new WaitUntil(() => optionSelected);
            }

            //Llama al evento del final, si tiene
            line.endDialogueEvent?.Invoke();

            optionSelected = false;
        }

        //detiene el dialogo una vez que se hayan terminado los dialogos
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
        //coje la linea en la que estamos
        DialogueString line = dialogueList[currentDialogueIndex];
        
        //escribe la linea
        dialogueText.text = "";
        foreach(char letter in text.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        //si no es una pregunta espera hasta que pulsemos para escribir el siguiente dialogo
        if (!line.isQuestion)
        {
            yield return new WaitUntil(() => Input.GetMouseButton(0));
        }

        //Si la opcion de final de dialogo esta activada, se termina el dialogo
        if (line.isEnd)
        {
            DialogueStop();
        }

        //Salta de dialogos si la opcion de jumpToIndex es distina de 0, sino avanza al siguiente
        if(line.jumpToIndex != 0)
        {
            currentDialogueIndex = (int)line.jumpToIndex;
        }
        else
        {
            currentDialogueIndex++;
        }
    }

    private void DialogueStop()
    {
        //detiene las corrutinas
        StopAllCoroutines();
        dialogueText.text = "";

        StartCoroutine(TurnNPCTowardsTargetRot(npcData.originalRot));

        //desactiva el cuadro de dialogo
        dialogueParent.SetActive(false);

        //reactiva el interactuable del NPC y manda una señal al player de que se acabo la interaccion
        NPC.GetComponent<Interactable>().enabled = true;
        player.GetComponent<SkoController>().EndInteraction();
    }

    //rotar al NPC
    IEnumerator TurnNPCTowardsTargetPos(Vector3 target)
    {
        Quaternion startRot = NPC.transform.rotation;
        Quaternion targetRot = Quaternion.LookRotation(CoolFunctions.FlattenVector3(target) - CoolFunctions.FlattenVector3(NPC.transform.position));


        float elapsedTime = 0;
        while (elapsedTime < 1)
        {
            NPC.transform.rotation = Quaternion.Slerp(startRot, targetRot, elapsedTime);
            elapsedTime += Time.deltaTime* turnSpeed;
            yield return null;
        }

        NPC.transform.rotation = targetRot;
    }

    IEnumerator TurnNPCTowardsTargetRot(Quaternion targetRot)
    {
        Quaternion startRot = NPC.transform.rotation;


        float elapsedTime = 0;
        while (elapsedTime < 1)
        {
            NPC.transform.rotation = Quaternion.Slerp(startRot, targetRot, elapsedTime);
            elapsedTime += Time.deltaTime * turnSpeed;
            yield return null;
        }

        NPC.transform.rotation = targetRot;
    }
}
