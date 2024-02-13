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
            GameEventsManager.instance.itemsEvents.MoneyChange(coinAmount);
            Destroy(gameObject);
        }
    }
}
