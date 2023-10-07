using System.Collections;
using UnityEngine;
using System.Collections.Generic;

[System.Serializable]


public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;
    public CollectQuest collectQuest;
    public List<Quest> activeQuests = new List<Quest>();
    public List<Quest> completedQuests = new List<Quest>();
    void Awake()
    {
        instance = this; // Asigna la instancia actual del QuestManager a la referencia estática.
    }

    public bool HasQuest(Quest quest)
    {
        return activeQuests.Contains(quest);
    }

    public void AcceptQuest(Quest quest)
    {
        activeQuests.Add(quest);
    }

    public void CompleteQuest(Quest quest)
    {
        quest.isCompleted = true;
        activeQuests.Remove(quest);
        completedQuests.Add(quest);
    }
}

