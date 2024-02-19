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
                    //quest UI
                    questsUIManager.questRunning = true;
                    questsUIManager.activeQuests.Add(currentQuest);

                    print($"Quest ID: {recievableQuest} is {currentQuest.progress}");

                    if(currentQuest.progress == Quest.QuestProgress.COMPLETE)
                    {
                        CompleteQuest(currentQuest);
                    }
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
            }
        }
    }

    //Completar Quest
    public void CompleteQuest(Quest completedQuest)
    {
        for(int i = 0; i< currentQuestList.Count; i++)
        {
            Quest quest = currentQuestList[i];
            if(quest == completedQuest && quest.progress == Quest.QuestProgress.COMPLETE)
            {
                quest.progress = Quest.QuestProgress.FINISHED;
                currentQuestList.Remove(quest);

                //Recompensa

            }
        }

        //Si hay algun quest encadenado a este
        if(completedQuest.nextQuest != null)
        {
            CheckChainQuest(completedQuest);
            print($"Next quest:{completedQuest.title}");
        }
    }

    //Quest encadenado
    void CheckChainQuest(Quest chainedQuest)
    {
        int tempID = -1;
        for(int i = 0;i < questList.Count; i++)
        {
            Quest quest = questList[i];
            if(quest == chainedQuest && quest.nextQuest.ID > 0)
            {
                tempID = quest.nextQuest.ID;
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
                    AcceptQuest(quest);

                }
            }
        }
    }


    //Añadir items
    public void AddQuestItem(Quest quest, int itemAmount)
    {
        for (int i = 0; i < currentQuestList.Count; i++)
        {
            Quest currentQuest = currentQuestList[i];

            if (currentQuest.progress == Quest.QuestProgress.ACCEPTED)
            {
                if (currentQuest == quest)
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
                Quest avaliableQuest = questObj.avaliableQuests[j];
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
    public void ShowQuestLog(Quest questToShow)
    {
        for(int i = 0; i< currentQuestList.Count; i++)
        {
            Quest quest = currentQuestList[i];
            if(quest == questToShow)
            {
                FindObjectOfType<QuestsUIManager>().ShowQuestLog(quest);
            }
        }
    }
}
