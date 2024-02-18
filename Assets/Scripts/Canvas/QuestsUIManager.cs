using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class QuestsUIManager : MonoBehaviour
{
    public Transform parentQuestPanel;

    [Header("Bools")]
    public bool questAvaliable = false;
    public bool questRunning = false;

    public bool questPanelActive = false;
    public bool questLogPanelActive = false;

    [Header("Paneles")]
    public GameObject questPanel;
    public GameObject questLogPanel;

    [Header("Quest Objects")]
    QuestObject currentQuestObject;

    [Header("Quest Lists")]
    public List<Quest> avaliableQuests = new List<Quest>();
    public List<Quest> activeQuests = new List<Quest>();

    [Header("Buttons")]
    public GameObject qButton;
    public GameObject qLogButton;
    List<GameObject> qButtons = new List<GameObject>();

    public GameObject acceptButton;
    public GameObject completeButton;
    [HideInInspector] public QButtonScript acceptButtonScript;
    [HideInInspector] public QButtonScript completeButtonScript;

    [Header("Spacer")]
    public Transform qButtonSpacer1;
    public Transform qButtonSpacer2;
    public Transform qLogButtonSpacer;

    [Header("Info")]
    public TMP_Text questTitle;
    public TMP_Text questDescription;
    public TMP_Text questSummary;
    
    public TMP_Text questLogTitle;
    public TMP_Text questLogDescription;
    public TMP_Text questLogSummary;

    private void Awake()
    {
        FindChilds();
    }


    private void Start()
    {
        acceptButtonScript = acceptButton.GetComponent<QButtonScript>();

        completeButtonScript = completeButton.GetComponent<QButtonScript>();

        acceptButton.SetActive(false);
        completeButton.SetActive(false);


        HideQuestPanel();
    }

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Q))
        {
            questLogPanelActive = !questLogPanelActive;
            ShowQuestLogPanel();

        }
    }

    void FindChilds()
    {
        //Quest panel
        questPanel = parentQuestPanel.Find("QuestsPanel").gameObject;

        qButtonSpacer1 = questPanel.transform.Find("AvaliableQ").Find("QButtonSpace");
        qButtonSpacer2 = questPanel.transform.Find("RecievableQ").Find("QButtonSpace");

        Transform questDescParent = questPanel.transform.Find("QuestDescription");
        questTitle = questDescParent.Find("QuestName").GetComponent<TMP_Text>();
        questDescription = questDescParent.Find("QuestDescription").GetComponent<TMP_Text>();
        questSummary = questDescParent.Find("QuestSummary").GetComponent<TMP_Text>();

        acceptButton = questDescParent.Find("ButtonHolder").Find("AcceptButton").gameObject;
        completeButton = questDescParent.Find("ButtonHolder").Find("CompleteButton").gameObject;

        //Quest Log Panel
        questLogPanel = parentQuestPanel.transform.Find("QuestsLogPanel").gameObject;

        qLogButtonSpacer = questLogPanel.transform.Find("AvaliableQ").Find("QButtonSpace");

        Transform questLogDescParent = questLogPanel.transform.Find("QuestDescription");
        questLogTitle = questLogDescParent.Find("QuestName").GetComponent< TMP_Text>();
        questLogDescription = questLogDescParent.Find("QuestDescription").GetComponent< TMP_Text>();
        questLogSummary = questLogDescParent.Find("QuestSummary").GetComponent< TMP_Text>();

    }

    //Llamadas desde Quest Object
    public void CheckQuests(QuestObject questObject)
    {
        currentQuestObject = questObject;
        QuestManager.instance.QuestRequest(questObject);

        if((questRunning || questAvaliable) && !questPanelActive)
        {
            ShowQuestPanel();
        }
        else
        {
            print($"No Quests Avaliable");
        }
    }
    
    //Show Panel
    public void ShowQuestPanel()
    {
        GameManager.instance.SoftPauseGame();


        questPanelActive = true;
        questPanel.SetActive(questPanelActive);


        //Relleno
        FillQuestButtons();
    }

    public void ShowQuestLogPanel()
    {
        questLogPanel.SetActive(questLogPanelActive);
        if(questLogPanelActive && !questPanelActive )
        {
            foreach(Quest curQuest in QuestManager.instance.currentQuestList)
            {
                GameObject questButton = Instantiate(qLogButton);
                QLogButonScript qBLScript = questButton.GetComponent<QLogButonScript>();

                qBLScript.quest = curQuest;
                qBLScript.questTitle.text = curQuest.title;

                questButton.transform.SetParent(qLogButtonSpacer, false);
                qButtons.Add(questButton);
            }
        }
        else if(!questLogPanelActive && !questPanelActive)
        {
            HideQuestLogPanel();
        }

        //Relleno
        FillQuestButtons();
    }

    public void ShowQuestLog(Quest activeQuest)
    {
        questLogTitle.text = activeQuest.title;
        if(activeQuest.progress == Quest.QuestProgress.ACCEPTED)
        {
            questLogDescription.text = activeQuest.hint;
            questLogSummary.text = $"{activeQuest.questObjective}: {activeQuest.questObjectiveCount}/{activeQuest.questObjectiveRequirement}";
        }
        else if(activeQuest.progress == Quest.QuestProgress.COMPLETE)
        {
            questLogDescription.text = activeQuest.congratulation;
            questLogSummary.text = $"{activeQuest.questObjective}: {activeQuest.questObjectiveCount}/{activeQuest.questObjectiveRequirement}";
        }

    }

    //Ocultar Panel
    public void HideQuestPanel()
    {
        questPanelActive = false;
        questAvaliable = false;
        questRunning = false;

        //Borrar texto
        questTitle.text = "";
        questDescription.text = "";
        questSummary.text = "";

        //Borrar listas
        avaliableQuests.Clear();
        activeQuests.Clear();

        //Borrar lista de botones
        for (int i = 0; i < qButtons.Count; i++)
        {
            Destroy(qButtons[i]);
        }

        qButtons.Clear();

        //Ocultar panel
        questPanel.SetActive(questPanelActive);
    }

    //Ocultar quest log panel
    public void HideQuestLogPanel()
    {
        questLogPanelActive = false;
        questLogTitle.text = "";
        questLogDescription.text = "";
        questLogSummary.text = "";

        //borrar lista de botones
        for (int i = 0;i < qButtons.Count; i++)
        {
            Destroy(qButtons[i]);
        }
        qButtons.Clear();
        questLogPanel.SetActive(questLogPanelActive);
    }


    //Rellenar botones para el quest panel
    void FillQuestButtons()
    {
        foreach(Quest avaliableQuest in avaliableQuests)
        {
            GameObject questButton = Instantiate(qButton);
            QButtonScript qBScript = questButton.GetComponent<QButtonScript>();

            qBScript.quest = avaliableQuest;
            qBScript.questTitle.text = avaliableQuest.title;

            questButton.transform.SetParent(qButtonSpacer1, false);
            qButtons.Add(questButton);
        }

        foreach(Quest activeQuest in activeQuests)
        {
            GameObject questButton = Instantiate(qButton);
            QButtonScript qBScript = questButton.GetComponent<QButtonScript>();

            qBScript.quest = activeQuest;
            qBScript.questTitle.text = activeQuest.title;

            questButton.transform.SetParent(qButtonSpacer2, false);
            qButtons.Add(questButton);

        }
    }

    //Mostrar datos del quest al pulsar los botones
    public void ShowSelectedQuest(Quest selectedQuest)
    {
        for(int i = 0; i < avaliableQuests.Count; i++)
        {
            Quest quest = avaliableQuests[i];
            if(quest == selectedQuest)
            {
                questTitle.text = quest.title;
                if(quest.progress == Quest.QuestProgress.AVALIABLE)
                {
                    questDescription.text = quest.description;
                    questSummary.text = $"{quest.questObjective}: {quest.questObjectiveCount}/{quest.questObjectiveRequirement}" ;
                }
            }
        }

        for(int i = 0; i < activeQuests.Count; i++)
        {
            Quest quest = activeQuests[i];
            if(quest == selectedQuest)
            {
                questTitle.text = quest.title;
                if(quest.progress == Quest.QuestProgress.ACCEPTED)
                {
                    questDescription.text = quest.hint;
                    questSummary.text = $"{quest.questObjective}: {quest.questObjectiveCount}/{quest.questObjectiveRequirement}" ;
                }
                else if(quest.progress == Quest.QuestProgress.COMPLETE)
                {
                    questDescription.text = quest.congratulation;
                    questSummary.text = $"{quest.questObjective}: {quest.questObjectiveCount}/{quest.questObjectiveRequirement}" ;
                }
            }
        }
    }
}
