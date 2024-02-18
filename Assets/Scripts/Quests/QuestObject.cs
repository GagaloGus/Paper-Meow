using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestObject : MonoBehaviour
{
    public List<Quest> avaliableQuests = new List<Quest>();
    public List<Quest> recievableQuests = new List<Quest>();

    public Sprite questAvaliableSprite;
    public Sprite questRecievableSprite;

    private void Start()
    {
        SetQuestIcon();
    }

     public void SetQuestIcon()
    {
        CanvasNPC canvasNPC = GetComponent<CanvasNPC>();
        if (QuestManager.instance.CheckFinishedQuests(this))
        {
            canvasNPC.SetQuestIcon(questRecievableSprite, Color.yellow);
        }
        else if(QuestManager.instance.CheckAvaliableQuests(this))
        {
            canvasNPC.SetQuestIcon(questAvaliableSprite, Color.yellow);
        }
        else if (QuestManager.instance.CheckAcceptedQuests(this))
        {
            canvasNPC.SetQuestIcon(questRecievableSprite, Color.gray);
        }
        else 
        { 
            canvasNPC.DisableQuestIcon();
        }
    }

    public void GiveQuest()
    {
        QuestsUIManager questsUIManager = FindObjectOfType<QuestsUIManager>();
        if (!questsUIManager.questPanelActive)
        {
            FindObjectOfType<QuestsUIManager>().CheckQuests(this);
        }
    }

}
