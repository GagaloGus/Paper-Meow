using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QButtonScript : MonoBehaviour
{
    public int questID;
    public TMP_Text questTitle;

    GameObject acceptButton;
    GameObject completeButton;

    QButtonScript acceptButtonScript;
    QButtonScript completeButtonScript;

    private void Start()
    {
        questTitle = GetComponentInChildren<TMP_Text>();

        acceptButton = FindObjectOfType<QuestsUIManager>().acceptButton;
        acceptButtonScript = acceptButton.GetComponent<QButtonScript>();

        completeButton = FindObjectOfType<QuestsUIManager>().completeButton;
        completeButtonScript = completeButton.GetComponent<QButtonScript>();

        acceptButton.SetActive(false);
        completeButton.SetActive(false);
    }

    //onclick
    public void ShowSelectedInfo()
    {
        FindObjectOfType<QuestsUIManager>().ShowSelectedQuest(questID);

        //Accept
        if (QuestManager.instance.RequestAvaliableQuest(questID))
        {
            acceptButton.SetActive(true);
            acceptButtonScript.questID = questID;
        }
        else
        {
            acceptButton.SetActive(false);
        }

        //Complete
        if (QuestManager.instance.RequestCompleteQuest(questID))
        {
            completeButton.SetActive(true);
            completeButtonScript.questID = questID;
        }
        else
        {
            completeButton.SetActive(false);
        }
    }

    public void AcceptQuest()
    {
        QuestManager.instance.AcceptQuest(questID);
        FindObjectOfType<QuestsUIManager>().HideQuestPanel();

        //Update NPC icons
        QuestObject[] currentQuestNPCs = FindObjectsByType<QuestObject>(FindObjectsSortMode.None);

        foreach (QuestObject npc in currentQuestNPCs)
        {
            //Set quest marker
            npc.SetQuestIcon();
        }
    }

    public void CompleteQuest()
    {
        QuestManager.instance.CompleteQuest(questID);
        FindObjectOfType<QuestsUIManager>().HideQuestPanel();

        //Update NPC icons
        QuestObject[] currentQuestNPCs = FindObjectsByType<QuestObject>(FindObjectsSortMode.None);

        foreach (QuestObject npc in currentQuestNPCs)
        {
            //Set quest marker
            npc.SetQuestIcon();

        }
    }

    public void ClosePanel()
    {
        FindObjectOfType<QuestsUIManager>().HideQuestPanel();
    }
}
