using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiscEvents
{
    public event System.Action onCoinCollected;
    public void CoinCollected()
    {
        if (onCoinCollected != null)
        {
            onCoinCollected();
        }
    }

    public event System.Action onPauseMenuOpen;
    public void PauseMenuOpen()
    {
        if (onPauseMenuOpen != null)
        {
            onPauseMenuOpen();
        }
    }

    public event System.Action<string> onThingObtained;
    public void ThingObtained(string thingName)
    {
        if (onThingObtained != null)
        {
            onThingObtained(thingName);
        }
    }    
}
