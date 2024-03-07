using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;
    [SerializeField] QuestsUIManager questsUIManager;

    [Header("Listas de quests")]
    public List<Quest> questList = new(); //Todos los quests
    public List<Quest> currentQuestList = new(); //Quests activos

    [Header("Follow Quest")]
    public Quest followingQuest;

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

        questList.Clear();
        questList = Resources.LoadAll<Quest>("Quests").ToList();

        //temporal, pone todos los quests en disponible
        foreach (Quest quest in questList)
        {
            quest.progress = Quest.QuestProgress.AVALIABLE;
            quest.questObjectiveCount = 0;
        }
    }

    public void QuestRequest(string questCheck_ID)
    {
        Quest questFound = System.Array.Find(questList.ToArray(), x => x.ID_name == questCheck_ID);
        if (questFound != null)
        {
            QuestRequest(questFound);
        }
        else { Debug.LogWarning($"Quest ID string {questCheck_ID} no encontrado en la lista de QuestManager"); }
    }

    public void QuestRequest(Quest questChecked)
    {
        Quest questFound = System.Array.Find(questList.ToArray(), x => x.ID_name == questChecked.ID_name);
        if (questFound != null)
        {
            if (questFound.progress == Quest.QuestProgress.AVALIABLE)
            {
                AcceptQuest(questFound);
            }
            else if (questFound.progress == Quest.QuestProgress.COMPLETE)
            {
                CompleteQuest(questFound);
            }
        }
        else
        {
            Debug.LogWarning($"Quest {questChecked.ID_name} no encontrado en la lista de QuestManager");
        }
    }

    //Aceptar quest
    void AcceptQuest(Quest questToAccept)
    {
        questToAccept.progress = Quest.QuestProgress.ACCEPTED;
        currentQuestList.Add(questToAccept);

        print($"Quest acceptado: {questToAccept.ID_name}");

        GameEventsManager.instance.npcEvents.AddedQuest(questToAccept);
    }

    //Completar Quest
    public void CompleteQuest(Quest completedQuest)
    {
        completedQuest.progress = Quest.QuestProgress.FINISHED;
        currentQuestList.Remove(completedQuest);

        print($"Quest completado: {completedQuest.ID_name}");

        GameEventsManager.instance.npcEvents.UnfollowQuest();
        GameEventsManager.instance.npcEvents.FinishedQuest(completedQuest);

        //recompensas
        if (completedQuest.expReward > 0) { GameEventsManager.instance.miscEvents.ExperienceGained(completedQuest.expReward); }
        if (completedQuest.goldReward > 0) { GameEventsManager.instance.miscEvents.CoinCollected(completedQuest.goldReward); }

        if (completedQuest.itemRewards.Count > 0)
        {
            foreach (Item item in completedQuest.itemRewards)
            {
                InventoryManager.instance.AddItem(item);
            }
        }

        //variables INK
        DialogueManager.instance.ChangeVariableState(completedQuest.nombreVariable, completedQuest.valorVariable);

        //Si hay algun quest encadenado a este
        if (completedQuest.nextQuest != null)
        {
            CheckChainQuest(completedQuest);
        }
    }

    //Quest encadenado
    void CheckChainQuest(Quest completedQuest)
    {
        //mira si esta en la lista general
        Quest chainedQuest = completedQuest.nextQuest;

        //Si lo encuentra y el quest no esta disponible
        if (chainedQuest != null && (chainedQuest.progress == Quest.QuestProgress.NOT_AVALIABLE || chainedQuest.progress == Quest.QuestProgress.AVALIABLE))
        {
            //Lo acepta automaticamente
            AcceptQuest(chainedQuest);
        }
        else if (chainedQuest == null)
        {
            Debug.LogWarning($"Quest {completedQuest.ID_name} no encontrado en la lista de Quest Manager");
        }
        else
        {
            Debug.LogWarning($"Quest {completedQuest.ID_name} ya estaba aceptado antes");
        }
    }


    //Añadir items, checka cuando el quest ha sido completado si cumplimos sus requisitos;
    public bool AddQuestItem(Quest quest, int itemAmount)
    {
        if (quest.progress == Quest.QuestProgress.ACCEPTED)
        {
            quest.questObjectiveCount += itemAmount;

            if (quest.questObjectiveCount >= quest.questObjectiveRequirement)
            {
                quest.progress = Quest.QuestProgress.COMPLETE;
                if (quest.completionType == Quest.CompleteType.AutoComplete)
                {
                    CompleteQuest(quest);
                }
            }
            return true;
        }
        else
        {
            Debug.LogWarning($"Quest {quest.ID_name} no esta aceptado");
            return false;
        }
    }

    public void AutoFinishQuest(string questID)
    {
        Quest questFound = System.Array.Find(questList.ToArray(), x => x.ID_name == questID);
        if (questFound != null)
        {
            CompleteQuest(questFound);
        }
        else { Debug.LogWarning($"Quest ID string {questID} no encontrado en la lista de QuestManager"); }
    }


    //Quitar items

    //Mostrar el quest log
    public void ShowQuestLog(Quest questToShow)
    {
        for (int i = 0; i < currentQuestList.Count; i++)
        {
            Quest quest = currentQuestList[i];
            if (quest == questToShow)
            {
                FindObjectOfType<QuestsUIManager>().ShowQuest(quest);
            }
        }
    }

    public void FollowQuestHandle(Quest quest, bool follow)
    {
        if (follow)
        {
            GameEventsManager.instance.npcEvents.FollowQuest(quest);
        }
        else
        {
            GameEventsManager.instance.npcEvents.UnfollowQuest();
        }
    }
}
