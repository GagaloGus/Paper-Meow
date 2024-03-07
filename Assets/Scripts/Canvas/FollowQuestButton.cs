using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FollowQuestButton : MonoBehaviour
{
    public Quest quest;
    public bool following = false;

    private void Start()
    {
        following = false;
    }

    private void Update()
    {
        if (following) 
        { 
            GetComponent<Image>().color = Color.grey;
            GetComponentInChildren<TMP_Text>().text = "Following";
        }
        else 
        { 
            GetComponent<Image>().color = Color.white;
            GetComponentInChildren<TMP_Text>().text = "Follow Quest";
        }

    }

    public void FollowQuest()
    {
        //Seguir el Quest
        following = !following;
        if (following) { Follow(); }
        else { Unfollow(); }

    }

    public void Follow()
    {
        QuestManager.instance.FollowQuestHandle(quest, true);
    }

    public void Unfollow()
    {
        QuestManager.instance.FollowQuestHandle(quest, false);
    }
}
