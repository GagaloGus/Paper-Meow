using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int coinAmount;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<SkoController>())
        {
            GameManager.instance.MoneyUpdate(coinAmount);
            GameEventsManager.instance.miscEvents.CoinCollected();
            GameEventsManager.instance.miscEvents.ThingObtained($"{coinAmount} MeowCoins");
            Destroy(gameObject);
        }
    }
}
