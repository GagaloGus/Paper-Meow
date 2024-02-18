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
        if(questObj.avaliableQuests.Count > 0)
        {
            for(int i = 0; questList.Count > i; i++)
            {
                Quest quest = questList[i];
                for(int j = 0; j < questObj.avaliableQuests.Count; j++)
                {
                    Quest avaliableQuest = questObj.avaliableQuests[j];
                    if(quest == avaliableQuest && quest.progress == Quest.QuestProgress.AVALIABLE)
                    {
                        print($"Quest ID: {avaliableQuest.ID} + {quest.progress}");

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
            for (int j = 0; j < questObj.recievableQuests.Count; j++)
            {
                Quest recievableQuest = questObj.recievableQuests[j];
                if(currentQuest == recievableQuest && currentQuest.progress == Quest.QuestProgress.ACCEPTED 
                    || currentQuest.progress == Quest.QuestProgress.COMPLETE)
                {
                    print($"Quest ID: {recievableQuest.ID} is {currentQuest.progress}");

                    //quest UI
                    questsUIManager.questRunning = true;
                    questsUIManager.activeQuests.Add(currentQuest);
                }
                
            }
        }
        questObj.SetQuestIcon();
    }

    //Aceptar quest
    public void AcceptQuest(Quest questToAccept)
    {
        for (int i = 0; i < questList.Count; i++)
        {
            Quest quest = questList[i];
            if(quest == questToAccept & quest.progress == Quest.QuestProgress.AVALIABLE)
            {
                currentQuestList.Add(quest);
                quest.progress = Quest.QuestProgress.ACCEPTED;

                GameManager.instance.ContinueGame();
                break;
            }
        }
    }

    //Completar Quest
    public void CompleteQuest(Quest questToComplete)
    {
        for(int i = 0; i< currentQuestList.Count; i++)
        {
            Quest quest = currentQuestList[i];
            if(quest == questToComplete && quest.progress == Quest.QuestProgress.COMPLETE)
            {
                quest.progress = Quest.QuestProgress.FINISHED;
                currentQuestList.Remove(quest);

                //Recompensa

                GameManager.instance.ContinueGame();
                break;
            }
        }

        //Si hay algun quest encadenado a este
        CheckChainQuest(questToComplete);
    }

    //Quest encadenado
    void CheckChainQuest(Quest chainedQuest)
    {
        int tempID = -1;
        for(int i = 0;i < questList.Count; i++)
        {
            Quest quest = questList[i];
            if(quest == chainedQuest && quest.nextQuestID > 0)
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
    public void AddQuestItem(Quest questToAddItem, int itemAmount)
    {
        for (int i = 0; i < currentQuestList.Count; i++)
        {
            Quest currentQuest = currentQuestList[i];

            if (currentQuest.progress == Quest.QuestProgress.ACCEPTED)
            {
                if (currentQuest == questToAddItem)
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
    public bool RequestAvaliableQuest(Quest quest)
    {
        for(int i = 0; i < questList.Count; i++)
        {
            if (questList[i] == quest && 
                questList[i].progress == Quest.QuestProgress.AVALIABLE) 
            { return true; }

        }
        return false;
    }

    public bool RequestAcceptedQuest(Quest quest)
    {
        for (int i = 0; i < questList.Count; i++)
        {
            if (questList[i] == quest && questList[i].progress == Quest.QuestProgress.ACCEPTED)
            { return true; }

        }
        return false;
    }

    public bool RequestCompleteQuest(Quest quest)
    {
        for (int i = 0; i < questList.Count; i++)
        {
            if (questList[i] == quest && questList[i].progress == Quest.QuestProgress.COMPLETE)
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
            for(int j = 0;j < questObj.avaliableQuests.Count; j++) 
            {
                Quest avaliableQuest = questObj.avaliableQuests[j];
                if (quest == avaliableQuest && quest.progress == Quest.QuestProgress.AVALIABLE)
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
            for (int j = 0; j < questObj.recievableQuests.Count; j++)
            {
                Quest avaliableQuest = questObj.recievableQuests[j];
                if (quest == avaliableQuest && quest.progress == Quest.QuestProgress.ACCEPTED)
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
            for (int j = 0; j < questObj.recievableQuests.Count; j++)
            {
                Quest avaliableQuest = questObj.recievableQuests[j];
                if (quest == avaliableQuest && quest.progress == Quest.QuestProgress.FINISHED)
                {
                    return true;
                }
            }
        }
        return false;
    }

    //Mostrar el quest log
    public void ShowQuestLog(Quest questLogToShow)
    {
        for(int i = 0; i< currentQuestList.Count; i++)
        {
            Quest quest = currentQuestList[i];
            if(quest == questLogToShow)
            {
                FindObjectOfType<QuestsUIManager>().ShowQuestLog(quest);
            }
        }
    }
}
