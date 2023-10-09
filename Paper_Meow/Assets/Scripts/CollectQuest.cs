using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

[System.Serializable]

[CreateAssetMenu(fileName = "New Collect Quest", menuName = "Quest/Collect")]
public class CollectQuest : Quest
{
    public static CollectQuest instance;
    public int requiredItems;
    public int itemsCollected;

    public void CheckCompletion()
    {
        if (itemsCollected >= requiredItems && !isCompleted)
        {
            GameManager.instance.AddPunt(100);
            isCompleted = true;
            QuestManager.instance.CompleteQuest(this);
            Debug.Log("Misión de recolección completada.");
        }

    }

    public override void Reset()
    {
        base.Reset();
        itemsCollected = 0;
    }
}
