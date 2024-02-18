using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
{
    public enum QuestProgress { 
        NOT_AVALIABLE,
        AVALIABLE,
        ACCEPTED,
        COMPLETE,
        FINISHED
    }

    [Header("Data")]
    public string title;
    public int ID;
    public QuestProgress progress;

    [Header("Texto")]
    [TextArea(3,5)] public string description;
    public string hint;
    public string congratulation;
    public string summary;

    [Header("Objectives")]
    public int nextQuestID;
    public string questObjective;
    public int questObjectiveCount;
    public int questObjectiveRequirement;

    [Header("Rewards")]
    public int expReward;
    public int goldReward;
    public Item itemReward;
}
