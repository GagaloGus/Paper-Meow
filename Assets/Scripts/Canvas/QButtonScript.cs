using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QButtonScript : MonoBehaviour
{
    /*public Quest quest;
    public TMP_Text questTitle;



    //onclick
    public void ShowSelectedInfo()
    {
        QuestsUIManager questsUIManager = FindObjectOfType<QuestsUIManager>();
        questsUIManager.ShowSelectedQuest(questID);

        //Accept
        if (QuestManager.instance.RequestAvaliableQuest(questID))
        {
            questsUIManager.acceptButton.SetActive(true);
            questsUIManager.acceptButtonScript.questID = questID;
        }
        else
        {
            questsUIManager.acceptButton.SetActive(false);
        }

        //Complete
        if (QuestManager.instance.RequestCompleteQuest(questID))
        {
            questsUIManager.completeButton.SetActive(true);
            questsUIManager.completeButtonScript.questID = questID;
        }
        else
        {
            questsUIManager.completeButton.SetActive(false);
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
        QuestsUIManager questsUIManager = FindObjectOfType<QuestsUIManager>();
        questsUIManager.HideQuestPanel();
        questsUIManager.acceptButton.SetActive(true);
        questsUIManager.completeButton.SetActive(true);
    }*/
}
