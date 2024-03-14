using Ink.Runtime;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    [Header("Dialogue UI")]
    [SerializeField] GameObject dialogueParent;
    [SerializeField] TMP_Text dialogueText;
    [SerializeField] TMP_Text dialogueName;
    [SerializeField] Image dialogueSprite;
    [SerializeField] GameObject choiceParent;
    [SerializeField] GameObject continueText;
     
    Story currentStory;

    GameObject[] choiceButtons;
    Animator portraitAnimator;

    [Header("Variables globales JSON file")]
    [SerializeField] TextAsset globalsInkFile;

    [Header("Parametros")]
    public float typingSpeed = 0.05f;
    public GameObject npcInteracting;

    DialogueVariables dialogueVariables;
    InkExternalFunctions inkEternalFunctions;

    public string currentEmotion = "";

    const string SPEAKER_TAG = "speaker";
    const string EMOTION_TAG = "emotion";

    public DialogueNPCs dialogueNPCs;

    private void Awake()
    {
        if (!instance) //instance  != null  //Detecta que no haya otro manager en la escena.
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); //Si hay otro manager lo destruye.
        }

        //borra todos los playerprefs, la chupan
        PlayerPrefs.DeleteAll();

        dialogueVariables = new DialogueVariables(globalsInkFile);
        inkEternalFunctions = new InkExternalFunctions();

        dialogueParent = FindObjectOfType<TextController>().transform.Find("Dialogue panel").gameObject;
        dialogueText = dialogueParent.transform.Find("text").GetComponent<TMP_Text>();
        dialogueName = dialogueParent.transform.Find("name").GetComponent<TMP_Text>();
        dialogueSprite = dialogueParent.transform.Find("icon").GetComponent<Image>();
        portraitAnimator = dialogueSprite.gameObject.GetComponent<Animator>();

       
        choiceParent = dialogueParent.transform.Find("choiceButtons").gameObject;
        GameObject[] tempChilds = new GameObject[choiceParent.transform.childCount];
        for (int i = 0; i < choiceParent.transform.childCount; i++)
        {
            tempChilds[i] = choiceParent.transform.GetChild(i).gameObject;

            int currentIndex = i;
            choiceParent.transform.GetChild(i).GetComponent<Button>().onClick.RemoveAllListeners();
            choiceParent.transform.GetChild(i).GetComponent<Button>().onClick.AddListener(() => {
                MakeChoice(currentIndex);
            });
        }
        choiceButtons = tempChilds;

        continueText = dialogueParent.transform.Find("continuar text").gameObject;
    }

    private void Start()
    {
        choiceParent.GetComponent<Animator>().SetBool("open", false);
        choiceParent.SetActive(false);
        dialogueParent.SetActive(false);
        dialogueNPCs = GetComponent<DialogueNPCs>();
    }

    public void StartDialogue(TextAsset inkJSON, NPCBehaviour npcBehaviour)
    {
        currentStory = new Story(inkJSON.text);

        dialogueParent.SetActive(true);

        dialogueVariables.StartListening(currentStory);
        inkEternalFunctions.Bind(currentStory, npcBehaviour);
        npcInteracting = npcBehaviour.gameObject;

        GameManager.instance.StartInteraction(gameObject);
        StartCoroutine(ContinueStory());

        GameEventsManager.instance.npcEvents.UnfollowQuest();
    }
    IEnumerator ContinueStory()
    {
        while (currentStory.canContinue)
        {
            string nextLine = currentStory.Continue();

            //Si la linea contiene una funcion nos la saltamos hasta que haya texto
            while(nextLine == "\n")
            {
                nextLine = currentStory.Continue();
            }

            StopCoroutine(nameof(TypeLine));
            yield return StartCoroutine(TypeLine(nextLine));

            //Si al final de la linea no hay nada saltamos directamente al final
            if (nextLine == "" && !currentStory.canContinue)
            {
                ExitDialogueMode();
            }

        }

        ExitDialogueMode();

    }

    NPCBehaviour npcTalking = null;
    IEnumerator TypeLine(string line)
    {
        //desactiva las opciones y el texto de continuar
        dialogueText.text = "";
        choiceSelected = false;
        DisplayChoices(false);
        continueText.SetActive(false);

        //Tags
        HandleTags(currentStory.currentTags);

        dialogueNPCs.NPCTalking(npcTalking);
        portraitAnimator.speed = 1;


        foreach (char letter in line.ToCharArray())
        {
            dialogueText.text += letter;
            AudioManager.instance.PlaySFX2D(npcTalking.npcData.typingSfxs[UnityEngine.Random.Range(0, npcTalking.npcData.typingSfxs.Length)], npcTalking.npcData.typingVolume);
            
            float newTypingSpeed = typingSpeed / npcTalking.npcData.typingSpeedMult;

            float waitMultiplier = 1;
            if (letter == ',')
            { waitMultiplier = 5f; }
            else if (letter == '.' || letter == '!')
            { waitMultiplier = 8; }

            yield return new WaitForSeconds(newTypingSpeed * waitMultiplier);
        }

        dialogueNPCs.NPCStopTalking();
        //pausa el animator en su primer frame
        portraitAnimator.Play(portraitAnimator.GetCurrentAnimatorStateInfo(0).fullPathHash, 0, 0);
        portraitAnimator.speed = 0;

        if (currentStory.currentChoices.Count == 0)
        {
            continueText.SetActive(true);
            yield return new WaitUntil(() => (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Space)));
        }
        else
        {
            DisplayChoices(true);
            yield return new WaitUntil(() => choiceSelected);
        }
        

        void HandleTags(List<string> currentTags)
        {
            foreach(string tag in currentTags)
            {
                string[] splitTags = tag.Split(':');
                if(splitTags.Length != 2 )
                {
                    Debug.LogError($"Tag raro, no se pudo parsear: {tag}");
                }

                string tagKey = splitTags[0].Trim();
                string tagValue = splitTags[1].Trim();

                switch (tagKey)
                {
                    case SPEAKER_TAG:
                        npcTalking = dialogueNPCs.GetNPC(tagValue);
                        dialogueName.text = npcTalking.npcData.displayName;
                        break;
                    case EMOTION_TAG:
                        portraitAnimator.Play($"{npcTalking.npcData.ID_name}_{tagValue}");
                        break;
                    default:
                        Debug.LogWarning($"Se recogio el tag pero no se pudo identificar: {tag}");
                        break;
                }
            }
        }
    }


    bool choiceSelected = false;
    void DisplayChoices(bool showChoices)
    {
        if(showChoices)
        {
            choiceParent.SetActive(true);
            List<Choice> currentChoices = currentStory.currentChoices;

            if(currentChoices.Count > choiceButtons.Length) 
            { Debug.LogError("Demasiadas preguntas, la UI no da para tanto"); }

            //Activa los botones y les pone texto, desactiva el resto
            for(int i = 0; i < choiceButtons.Length; i++)
            {
                if(i < currentChoices.Count) 
                {
                    choiceButtons[i].SetActive(true);
                    choiceButtons[i].GetComponentInChildren<TMP_Text>().text = currentChoices[i].text;
                }
                else
                {
                    choiceButtons[i].SetActive(false);
                }
            }
            choiceParent.GetComponent<Animator>().SetBool("open", true);
        }
        else
        {
            choiceParent.GetComponent<Animator>().SetBool("open", false);
        }
    }

    public void MakeChoice(int choiceIndex)
    {
        currentStory.ChooseChoiceIndex(choiceIndex);
        choiceSelected = true;
    }

    private void ExitDialogueMode()
    {
        dialogueParent.SetActive(false);
        dialogueText.text = "";

        //dialogueVariables.StopListening(currentStory);
        inkEternalFunctions.UnBind(currentStory);

        dialogueNPCs.StopInteraction();
        GameManager.instance.EndInteraction();
    }

    //Queremos ver el parametro de alguna variable de INK en algun script
    public Ink.Runtime.Object GetVariableState(string variableName)
    {
        Ink.Runtime.Object variableValue = null;
        dialogueVariables.variables.TryGetValue(variableName, out variableValue);
        if(variableValue == null) 
        { Debug.LogWarning($"Ink Variable no existe: {variableName}"); }

        return variableValue;

    }

    public void ChangeVariableState(string variableName, string newValue)
    {
        currentStory.variablesState[variableName] = newValue;
    }


    private void OnApplicationQuit()
    {
        dialogueVariables?.SaveVariables();
    }

    public NPCBehaviour GetNPC(string nameID)
    {
        return dialogueNPCs.GetNPC(nameID);
    }
}
