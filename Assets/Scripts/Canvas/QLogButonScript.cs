using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QLogButonScript : MonoBehaviour
{
    public int questID;
    public TMP_Text questTitle;
    
    public void ShowAllInfos()
    {
        QuestManager.instance.ShowQuestLog(questID);
    }

    public void ClosePanel()
    {
        FindObjectOfType<QuestsUIManager>().HideQuestPanel();
    }
}
