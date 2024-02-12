using System.Collections;
using UnityEngine;
using System.Collections.Generic;

[System.Serializable]


public class QuestManager : MonoBehaviour
{
    private GameObject canvas;
    public static QuestManager instance;
    public CollectQuest collectQuest;
    public List<Quest> activeQuests = new List<Quest>();
    public List<Quest> completedQuests = new List<Quest>();

    void Awake()
    {
        if (!instance) //instance  != null  //Detecta que no haya otro GameManager en la escena.
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); //Si hay otro GameManager lo destruye.
        }
    }

    void Start()
    {
        instance = this; // Asigna la instancia actual del QuestManager a la referencia est�tica.
        canvas = FindAnyObjectByType<Canvas>().gameObject;
    }

    public bool HasQuest(Quest quest)
    {
        return activeQuests.Contains(quest);
    }

    public void AcceptQuest(Quest quest)
    {
        if(HasQuest(quest) && !quest.isCompleted) 
        {
            activeQuests.Add(quest);
            canvas.BroadcastMessage("UpdateCanvasQuests");
        }
        
    }

    public void CompleteQuest(Quest quest)
    {
        quest.isCompleted = true;
        activeQuests.Remove(quest);
        completedQuests.Add(quest);
        canvas.BroadcastMessage("LateUpdate");

        if(quest.unlockNewSkill != null)
        {
            //SkillManager.instance.GetSkill(quest.unlockNewSkill);
        }
    }

    private void OnDestroy()
    {
        foreach (Quest q in activeQuests)
        {
            q.Reset();
        }
        foreach (Quest q in completedQuests)
        {
            q.Reset();
        }
    }
}
