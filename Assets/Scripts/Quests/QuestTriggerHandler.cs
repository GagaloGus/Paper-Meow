using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTriggerHandler : MonoBehaviour
{
    public Quest quest;
    public int itemAmount;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<SkoController>())
        {
            QuestManager.instance.AddQuestItem(quest, itemAmount);

            QuestObject[] currentQuestNPCs = FindObjectsByType<QuestObject>(FindObjectsSortMode.None);

            foreach (QuestObject npc in currentQuestNPCs)
            {
                //Set quest marker
                npc.SetQuestIcon();

            }

            Destroy(gameObject);
        }
    }
}
