using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Canvas_Quest : MonoBehaviour
{
    private TMPro.TMP_Text text;
    void Start()
    {
        text = GetComponent<TMP_Text>();
    }

    public void LateUpdate()
    {
        QuestManager questManager = QuestManager.instance;
        text.text = "";

        int num = 1;
        foreach (Quest q in questManager.activeQuests)
        {
            text.text += num + ". " + q.questName + '\n';
            switch (q.type)
            {
                case Quest.QuestType.COLLECT:
                    text.text += ((CollectQuest)q).itemsCollected + "/" + ((CollectQuest)q).requiredItems;
                    break;
                case Quest.QuestType.TALK:
                    text.text = text.text;
                    break;
                case Quest.QuestType.DEFEAT:
                    break;
            }
            num++;
        }
    }
}
