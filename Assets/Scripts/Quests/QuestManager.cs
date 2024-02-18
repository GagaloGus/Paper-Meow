using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;
    [SerializeField] QuestsUIManager questsUIManager;

    public List<Quest> questList = new(); //Todos los quests
    public List<Quest> currentQuestList = new(); //Quests activos

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

        questsUIManager = FindObjectOfType<QuestsUIManager>();
    }

    public void QuestRequest(QuestObject questObj)
    {
        if(questObj.avaliableQuestIDs.Count > 0)
        {
            for(int i = 0; questList.Count > i; i++)
            {
                Quest quest = questList[i];
                for(int j = 0; j < questObj.avaliableQuestIDs.Count; j++)
                {
                    int avaliableQuestID = questObj.avaliableQuestIDs[j];
                    if(quest.ID == avaliableQuestID && quest.progress == Quest.QuestProgress.AVALIABLE)
                    {
                        print($"Quest ID: {avaliableQuestID} + {quest.progress}");

                        //quest UI
                        questsUIManager.questAvaliable = true;
                        questsUIManager.avaliableQuests.Add(quest);
                    }
                }
            }
        }

        //Active quests
        for (int i = 0; currentQuestList.Count > i; i++)
        {
            Quest currentQuest = currentQuestList[i];
            for (int j = 0; j < questObj.recievableQuestIDs.Count; j++)
            {
                int recievableQuestID = questObj.recievableQuestIDs[j];
                if(currentQuest.ID == recievableQuestID && currentQuest.progress == Quest.QuestProgress.ACCEPTED 
                    || currentQuest.progress == Quest.QuestProgress.COMPLETE)
                {
                    print($"Quest ID: {recievableQuestID} is {currentQuest.progress}");

                    //quest UI
                    questsUIManager.questRunning = true;
                    questsUIManager.activeQuests.Add(currentQuest);
                }
                
            }
        }
        questObj.SetQuestIcon();
    }

    //Aceptar quest
    public void AcceptQuest(int questID)
    {
        for (int i = 0; i < questList.Count; i++)
        {
            Quest quest = questList[i];
            if(quest.ID == questID & quest.progress == Quest.QuestProgress.AVALIABLE)
            {
                currentQuestList.Add(quest);
                quest.progress = Quest.QuestProgress.ACCEPTED;
            }
        }
    }

    //Completar Quest
    public void CompleteQuest(int questID)
    {
        for(int i = 0; i< currentQuestList.Count; i++)
        {
            Quest quest = currentQuestList[i];
            if(quest.ID == questID && quest.progress == Quest.QuestProgress.COMPLETE)
            {
                quest.progress = Quest.QuestProgress.FINISHED;
                currentQuestList.Remove(quest);

                //Recompensa

            }
        }

        //Si hay algun quest encadenado a este
        CheckChainQuest(questID);
    }

    //Quest encadenado
    void CheckChainQuest(int questID)
    {
        int tempID = -1;
        for(int i = 0;i < questList.Count; i++)
        {
            Quest quest = questList[i];
            if(quest.ID == questID && quest.nextQuestID > 0)
            {
                tempID = quest.nextQuestID;
            }
        }

        if(tempID >= 0)
        {
            for(int i = 0;i<questList.Count;i++)
            {
                Quest quest = questList[i];
                if(quest.ID == tempID && quest.progress == Quest.QuestProgress.NOT_AVALIABLE)
                {
                    quest.progress = Quest.QuestProgress.AVALIABLE;
                }
            }
        }
    }


    //Añadir items
    public void AddQuestItem(int questID, int itemAmount)
    {
        for (int i = 0; i < currentQuestList.Count; i++)
        {
            Quest currentQuest = currentQuestList[i];

            if (currentQuest.progress == Quest.QuestProgress.ACCEPTED)
            {
                if (currentQuest.ID == questID)
                {
                    currentQuestList[i].questObjectiveCount += itemAmount;
                }

                if (currentQuest.questObjectiveCount >= currentQuest.questObjectiveRequirement)
                {
                    currentQuestList[i].progress = Quest.QuestProgress.COMPLETE;
                }
            }
        }
    }


    //Quitar items


    //Booleanos
    public bool RequestAvaliableQuest(int questID)
    {
        for(int i = 0; i < questList.Count; i++)
        {
            if (questList[i].ID == questID && 
                questList[i].progress == Quest.QuestProgress.AVALIABLE) 
            { return true; }

        }
        return false;
    }

    public bool RequestAcceptedQuest(int questID)
    {
        for (int i = 0; i < questList.Count; i++)
        {
            if (questList[i].ID == questID && questList[i].progress == Quest.QuestProgress.ACCEPTED)
            { return true; }

        }
        return false;
    }

    public bool RequestCompleteQuest(int questID)
    {
        for (int i = 0; i < questList.Count; i++)
        {
            if (questList[i].ID == questID && questList[i].progress == Quest.QuestProgress.COMPLETE)
            { return true; }

        }
        return false;
    }

    //Booleanos 2
    public bool CheckAvaliableQuests(QuestObject questObj)
    {
        for(int i=0; i<questList.Count; i++)
        {
            Quest quest = questList[i];
            for(int j = 0;j < questObj.avaliableQuestIDs.Count; j++) 
            {
                int avaliableQuestID = questObj.avaliableQuestIDs[j];
                if (quest.ID == avaliableQuestID && quest.progress == Quest.QuestProgress.AVALIABLE)
                {
                    return true;
                }   
            }
        }
        return false;
    }

    public bool CheckAcceptedQuests(QuestObject questObj)
    {
        for (int i = 0; i < questList.Count; i++)
        {
            Quest quest = questList[i];
            for (int j = 0; j < questObj.recievableQuestIDs.Count; j++)
            {
                int avaliableQuestID = questObj.recievableQuestIDs[j];
                if (quest.ID == avaliableQuestID && quest.progress == Quest.QuestProgress.ACCEPTED)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public bool CheckFinishedQuests(QuestObject questObj)
    {
        for (int i = 0; i < questList.Count; i++)
        {
            Quest quest = questList[i];
            for (int j = 0; j < questObj.recievableQuestIDs.Count; j++)
            {
                int avaliableQuestID = questObj.recievableQuestIDs[j];
                if (quest.ID == avaliableQuestID && quest.progress == Quest.QuestProgress.FINISHED)
                {
                    return true;
                }
            }
        }
        return false;
    }
}
