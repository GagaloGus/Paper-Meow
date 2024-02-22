using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class NpcEvents
{
    public event System.Action<bool> onPlayerInRange;
    public void InRange(bool inRange)
    {
        if (onPlayerInRange != null)
        {
            onPlayerInRange(inRange);
        }
    }
}
