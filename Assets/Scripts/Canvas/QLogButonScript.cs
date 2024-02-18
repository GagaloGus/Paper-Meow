using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QLogButonScript : MonoBehaviour
{
    public Quest quest;
    public TMP_Text questTitle;
    
    public void ShowAllInfos()
    {
        QuestManager.instance.ShowQuestLog(quest);
    }

    public void ClosePanel()
    {
        FindObjectOfType<QuestsUIManager>().HideQuestPanel();
    }
}
