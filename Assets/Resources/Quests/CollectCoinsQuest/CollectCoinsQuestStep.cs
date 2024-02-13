using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCoinsQuestStep : QuestStep
{
    int coinsCollected = 0;

    int coinsToComplete = 5;

    private void OnEnable()
    {
        GameEventsManager.instance.miscEvents.onCoinCollected += CoinCollected;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.miscEvents.onCoinCollected -= CoinCollected;
    }

    void CoinCollected()
    {
        if(coinsCollected < coinsToComplete)
        {
            coinsCollected++;
        }
        else
        {
            FinishQuestStep();
        }
    }
}
