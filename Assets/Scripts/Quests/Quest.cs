using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Quest")]
public class Quest : ScriptableObject
{
    public enum QuestProgress { 
        NOT_AVALIABLE,
        AVALIABLE,
        ACCEPTED,
        COMPLETE,
        FINISHED
    }

    public enum CompleteType { AutoComplete, TalkToComplete}

    [Header("Data")]
    public string title;
    public string ID_name;
    public QuestProgress progress;
    public CompleteType completionType;

    [Header("Texto")]
    [TextArea(3,5)] public string description;

    [Header("Objectives")]
    public Quest nextQuest;
    public string questObjective;
    public int questObjectiveCount;
    public int questObjectiveRequirement;

    [Header("Rewards")]
    public int expReward;
    public int goldReward;
    public List<Item> itemRewards;

    [Header("Cambiar variable de trama INK")]
    public string nombreVariable;
    public string valorVariable;

#if UNITY_EDITOR_WIN
    private void OnValidate()
    {
        ID_name = this.name;
        UnityEditor.EditorUtility.SetDirty(this);
    }
#endif
}
