using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiscEvents
{
    public event Action<int> onCoinCollected;
    public void CoinCollected()
    {
        if (onCoinCollected != null)
        {
            onCoinCollected(2);
        }
    }

    public event Action<bool> onPauseMenuOpen;
    public void PauseMenuOpen()
    {
        if (onPauseMenuOpen != null)
        {
            onPauseMenuOpen(true);
        }
    }

    public event Action<string> onThingObtained;
    public void ThingObtained(string thingName)
    {
        if (onThingObtained != null)
        {
            onThingObtained(thingName);
        }
    }    
}
