using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class NpcEvents
{
    public event Action<bool> onPlayerInRange;
    public void InRange(bool inRange)
    {
        if (onPlayerInRange != null)
        {
            onPlayerInRange(inRange);
        }
    }

    public event Action<Quest> onAddedQuest;
    public void AddedQuest(Quest addedQuest)
    {
        if(onAddedQuest != null)
        {
            onAddedQuest(addedQuest);
        }
    }

    public event Action<Quest> onFollowQuest;
    public void FollowQuest(Quest followedQuest)
    {
        if(onFollowQuest != null)
        {
            onFollowQuest(followedQuest);
        }
    }

    public event System.Action onUnfollowQuest;
    public void UnfollowQuest()
    {
        if(onUnfollowQuest != null)
        {
            onUnfollowQuest();
        }
    }
    
    public event Action<Quest> onFinishedQuest;
    public void FinishedQuest(Quest addedQuest)
    {
        if(onFinishedQuest != null)
        {
            onFinishedQuest(addedQuest);
        }
    }
}
