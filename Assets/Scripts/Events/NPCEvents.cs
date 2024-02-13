using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCEvents
{
    public event Action<bool, GameObject> onInteraction;
    public void Interaction(bool start, GameObject npc)
    {
        if(onInteraction != null)
        {
            onInteraction(start, npc);
        }
    }
}
