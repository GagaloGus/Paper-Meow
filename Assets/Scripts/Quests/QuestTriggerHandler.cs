using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTriggerHandler : MonoBehaviour
{
    public int questID;
    public int itemAmount;

    public float radiusActivation;
    bool inside = false;
    GameObject player;
    private void Start()
    {
        player = FindObjectOfType<SkoController>().gameObject;
    }

    private void Update()
    {
        if(Vector3.Distance(transform.position, player.transform.position) < radiusActivation && !inside)
        {
            QuestManager.instance.AddQuestItem(questID, itemAmount);

            QuestObject[] currentQuestNPCs = FindObjectsByType<QuestObject>(FindObjectsSortMode.None);

            foreach (QuestObject npc in currentQuestNPCs)
            {
                //Set quest marker
                npc.SetQuestIcon();

            }

            inside = true;
        }

        if (Vector3.Distance(transform.position, player.transform.position) > radiusActivation && inside)
        {
            inside = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, radiusActivation);
    }
}
