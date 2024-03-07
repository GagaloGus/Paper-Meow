using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class Coin : MonoBehaviour
{
    public int coinAmount;

    public void CollectCoin()
    {
        Destroy(gameObject);
        GameEventsManager.instance.miscEvents.CoinCollected(coinAmount);
    }
}
