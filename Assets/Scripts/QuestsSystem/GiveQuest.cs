using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveQuest : MonoBehaviour
{
    [Header("Quest")]
    [SerializeField] QuestInfoSO questInfoForPoint;
    string questID;
    QuestState currentQuestState;

    [Header("Config")]
    [SerializeField] bool startPoint = true;
    [SerializeField] bool finishPoint = true;

    private void Awake()
    {
        questID = questInfoForPoint.id;
    }

    private void OnEnable()
    {
        GameEventsManager.instance.questEvents.onQuestStateChange += QuestStateChange; 
    }

    private void OnDisable()
    {
        GameEventsManager.instance.questEvents.onQuestStateChange -= QuestStateChange;
    }

    void QuestStateChange(Quest quest)
    {
        if(quest.info.id == questID)
        {
            currentQuestState = quest.state;
            GetComponent<CanvasNPC>().SetState(currentQuestState, startPoint, finishPoint);
            Debug.Log($"Quest con ID: {questID} cambiado a estado: {currentQuestState}");
        }
    }

    public void QuestAccepted()
    {
        if(currentQuestState == QuestState.CAN_START)
        {
            GameEventsManager.instance.questEvents.StartQuest(questID);
        }
        else if(currentQuestState == QuestState.CAN_FINISH) 
        {
            GameEventsManager.instance.questEvents.FinishQuest(questID);
        }

    }
}
