using System.Collections;
using UnityEngine;
using System.Collections.Generic;

[System.Serializable]


public class QuestManager : MonoBehaviour
{
    Dictionary<string, Quest> questMap;

    private void Awake()
    {
        questMap = CreateQuestMap();

        Quest quest = GetQuestById("CollectCoinsQuest");
    }

    private void OnEnable()
    {
        GameEventsManager.instance.questEvents.onStartQuest += StartQuest;
        GameEventsManager.instance.questEvents.onAdvanceQuest += AdvanceQuest;
        GameEventsManager.instance.questEvents.onFinishQuest += FinishQuest;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.questEvents.onStartQuest -= StartQuest;
        GameEventsManager.instance.questEvents.onAdvanceQuest -= AdvanceQuest;
        GameEventsManager.instance.questEvents.onFinishQuest -= FinishQuest;
    }

    private void Start()
    {
        foreach(Quest quest in questMap.Values)
        {
            GameEventsManager.instance.questEvents.QuestStateChange(quest);
        }
    }

    void StartQuest(string id)
    {
        print($"Start quest: {id}");
    }

    void AdvanceQuest(string id)
    {
        print($"Advance quest: {id}");
    }

    void FinishQuest(string id)
    {
        print($"Finish quest: {id}");
    }

    Dictionary<string, Quest> CreateQuestMap()
    {
        //Carga todos los quests de la carpeta de Resources
        QuestInfoSO[] allQuests = Resources.LoadAll<QuestInfoSO>("Quests");

        Dictionary<string, Quest> idToQuestMap = new Dictionary<string, Quest>();
        foreach(QuestInfoSO questInfo in allQuests)
        {
            if (idToQuestMap.ContainsKey(questInfo.id))
            {
                Debug.LogWarning($"Se encontro un ID duplicado: {questInfo.id}");
            }

            idToQuestMap.Add(questInfo.id, new Quest(questInfo));
        }

        return idToQuestMap;
    }

    Quest GetQuestById(string id)
    {
        Quest quest = questMap[id];
        if(quest == null) { Debug.LogError($"ID no encontrado en el Quest Map: {id}"); }

        return quest;
    }
}

