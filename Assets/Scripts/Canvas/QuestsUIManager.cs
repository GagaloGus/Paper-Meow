using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class QuestsUIManager : MonoBehaviour
{
    Transform parentQuestPanel;

    [Header("Bools")]
    public bool questAvaliable = false;
    public bool questRunning = false;

    bool questLogPanelActive = false;

    [Header("Paneles")]
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

    [Header("Spacer")]
    public Transform qLogButtonSpacer;

    [Header("Info")]
    public TMP_Text questLogTitle;
    public TMP_Text questLogDescription;
    public TMP_Text questLogSummary;

    private void Awake()
    {
        FindChilds();
    }

    public void ShowQuestCanvas()
    {
        questLogPanelActive = true;
        ShowQuestLogPanel();
    }

    void FindChilds()
    {
        parentQuestPanel = transform.Find("Menu_Pause").Find("Quests");

        //Quest Log Panel
        questLogPanel = parentQuestPanel.transform.Find("QuestsLogPanel").gameObject;
        qLogButtonSpacer = questLogPanel.transform.Find("AvaliableQ").Find("QButtonSpace");


        Transform questLogDescParent = parentQuestPanel.transform.Find("QuestDescription");
        questLogTitle = questLogDescParent.Find("QuestName").GetComponent< TMP_Text>();
        questLogDescription = questLogDescParent.Find("QuestDescription").GetComponent< TMP_Text>();
        questLogSummary = questLogDescParent.Find("QuestSummary").GetComponent< TMP_Text>();

    }

    //Llamadas desde Quest Object
    public void CheckQuests(QuestObject questObject, Quest questToAdd)
    {
        currentQuestObject = questObject;
        QuestManager.instance.QuestRequest(questObject);

        if((questRunning || questAvaliable))
        {
            //Mostrar por la interfaz
            QuestManager.instance.AcceptQuest(questToAdd);
        }
        else
        {
            print($"No Quests Avaliable");
        }
    }
    

    public void ShowQuestLogPanel()
    {
        questLogPanel.SetActive(questLogPanelActive);
        if(questLogPanelActive )
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
        else
        {
            HideQuestLogPanel();
        }
    }

    public void ShowQuestLog(Quest activeQuest)
    {
        questLogTitle.text = activeQuest.title;
        questLogDescription.text = activeQuest.description;
        questLogSummary.text = $"{activeQuest.questObjective}: {activeQuest.questObjectiveCount}/{activeQuest.questObjectiveRequirement}";
        
        /*if(activeQuest.progress == Quest.QuestProgress.ACCEPTED)
        {
        }
        else if(activeQuest.progress == Quest.QuestProgress.COMPLETE)
        {
            questLogDescription.text = activeQuest.congratulation;
            questLogSummary.text = $"{activeQuest.questObjective}: {activeQuest.questObjectiveCount}/{activeQuest.questObjectiveRequirement}";
        }*/

    }

    //Ocultar quest log panel
    public void HideQuestLogPanel()
    {
        questLogPanelActive = false;

        //borrar lista de botones
        for (int i = 0;i < qButtons.Count; i++)
        {
            Destroy(qButtons[i]);
        }
        qButtons.Clear();
        questLogPanel.SetActive(questLogPanelActive);
    }
}
