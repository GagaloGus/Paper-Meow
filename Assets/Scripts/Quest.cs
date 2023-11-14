using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = "Quest/Base Quest")]
public abstract class Quest : ScriptableObject
{
    public enum QuestType {COLLECT, TALK, DEFEAT};
    public QuestType type;
    public string questName;
    [TextArea(3, 5)]
    public string questDescription;
    public bool isCompleted;

    public virtual void Reset()
    {
        isCompleted = false;
    }
}