using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Content;
using UnityEngine;

public class MiscEvents
{
    public event System.Action<int> onCoinCollected;
    public void CoinCollected(int coinAmount)
    {
        if (onCoinCollected != null)
        {
            GameEventsManager.instance.miscEvents.ThingObtained("Meowcoins", coinAmount, Resources.Load<Sprite>("Sprites/Meowcoin"), Rarity.Common);
            GameManager.instance.MoneyUpdate(coinAmount);
            onCoinCollected(coinAmount);
        }
    }

    public event Action<int> onExperienceGained;
    public void ExperienceGained(int experience)
    {
        if (onExperienceGained != null)
        {
            GameEventsManager.instance.miscEvents.ThingObtained("XP", experience, Resources.Load<Sprite>("Sprites/XP"), Rarity.Common);
            onExperienceGained(experience);
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

    public event System.Action<string, int, Sprite, Rarity> onThingObtained;
    public void ThingObtained(string name, int amount, Sprite icon, Rarity thingRarity)
    {
        if (onThingObtained != null)
        {
            Debug.Log($"Item obtenido: {name} ({thingRarity}) x {amount}");
            onThingObtained(name, amount, icon, thingRarity);
        }
    }    
}
