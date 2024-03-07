using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestsUIManager : MonoBehaviour
{
    Transform parentQuestPanel;

    bool questLogPanelActive = false;

    [Header("Paneles")]
    public GameObject questLogPanel;
    public Transform questLogDescParent;

    [Header("Buttons")]
    public GameObject qLogButton;
    public List<GameObject> qButtons = new List<GameObject>();

    [Header("Spacer")]
    public Transform qLogButtonSpacer;

    [Header("Info")]
    public TMP_Text questLogTitle;
    public TMP_Text questLogDescription;
    public TMP_Text questLogSummary;
    public TMP_Text questLogReward;
    public GameObject questFollowButton;

    private void Awake()
    {
        FindChilds();
    }

    private void OnEnable()
    {
        GameEventsManager.instance.npcEvents.onAddedQuest += AddedQuest;
        GameEventsManager.instance.npcEvents.onFinishedQuest += FinishedQuest;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.npcEvents.onAddedQuest -= AddedQuest;
        GameEventsManager.instance.npcEvents.onFinishedQuest -= FinishedQuest;
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


        questLogDescParent = parentQuestPanel.transform.Find("QuestDescription");
        questLogTitle = questLogDescParent.Find("QuestName").GetComponent< TMP_Text>();
        questLogDescription = questLogDescParent.Find("QuestDescription").GetComponent< TMP_Text>();
        questLogSummary = questLogDescParent.Find("QuestSummary").GetComponent< TMP_Text>();
        questLogReward = questLogDescParent.Find("QuestRewards").GetComponent< TMP_Text>();
        questFollowButton = questLogDescParent.Find("FollowQuest").gameObject;
    }

    void AddedQuest(Quest questAdded)
    {
        GameObject questButton = Instantiate(qLogButton);
        QLogButonScript qBLScript = questButton.GetComponent<QLogButonScript>();

        qBLScript.quest = questAdded;
        qBLScript.questTitle.text = questAdded.title;

        questButton.transform.SetParent(qLogButtonSpacer, false);
        qButtons.Add(questButton);

        Debug.Log($"Quest {questAdded.ID_name} añadido a la UI");
    }

    void FinishedQuest(Quest questFinished)
     {
        GameObject questButton = Array.Find(qButtons.ToArray(), x => x.GetComponent<QLogButonScript>().quest == questFinished);
        if(questButton != null )
        {
            qButtons.Remove(questButton);
            Destroy(questButton);
            Debug.Log($"Quest {questFinished.ID_name} borrado de la UI");
        }
        else
        {
            Debug.LogWarning($"No se encontró el boton {questFinished.ID_name} en la interfaz");
        }
    }
    

    void ShowQuestLogPanel()
    {
        questLogTitle.text = "";
        questLogDescription.text = "";
        questLogSummary.text = "";
        questLogReward.text = "";
        questFollowButton.SetActive(false);

        questLogDescParent.gameObject.SetActive(false);
        questLogPanel.SetActive(questLogPanelActive);
    }

    public void ShowQuest(Quest activeQuest)
    {
        questLogDescParent.gameObject.SetActive(true);
        questLogTitle.text = activeQuest.title;
        questLogDescription.text = activeQuest.description;
        questLogSummary.text = $"{activeQuest.questObjective}: {activeQuest.questObjectiveCount}/{activeQuest.questObjectiveRequirement}";
        questLogReward.text = "Rewards:";

        questFollowButton.SetActive(true);
        questFollowButton.GetComponent<FollowQuestButton>().quest = activeQuest;
        questFollowButton.GetComponent<FollowQuestButton>().following = false;

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
        questLogTitle.text = "";
        questLogDescription.text = "";
        questLogSummary.text = "";
        questLogReward.text = "";
        questFollowButton.SetActive(false);
        questLogPanel.SetActive(questLogPanelActive);
    }
}
