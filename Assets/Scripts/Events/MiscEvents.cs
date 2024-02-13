using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiscEvents : MonoBehaviour
{
    public event Action onCoinCollected;
    public void CoinCollected()
    {
        if (onCoinCollected != null)
        {
            onCoinCollected();
        }
    }

    public event Action onGemCollected;
    public void GemCollected()
    {
        if (onGemCollected != null)
        {
            onGemCollected();
        }
    }
}
