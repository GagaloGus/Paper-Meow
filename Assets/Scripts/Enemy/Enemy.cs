using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] bool died = false;
    SkoStats player;

    public float health;
    public int exp;
    public int coinAmount;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<SkoStats>();
    }


    public void TakeDamage(float damage)
    {
        print($"au {damage}");
        health -= damage;

        if (health <= 0)
        {
            GameManager.instance.MoneyUpdate(coinAmount);
            GameEventsManager.instance.miscEvents.CoinCollected();
            GameEventsManager.instance.miscEvents.ThingObtained($"{coinAmount} MeowCoins");
            player.GetEXP(exp);
            Destroy(gameObject);

        }
    }


    private void OnTriggerEnter(Collider trigger)
    {
        Weapon weapon = trigger.gameObject.GetComponentInParent<Weapon>();
        if (weapon && !died)
        {

            TakeDamage(player.currentStats.ATK * weapon.weaponData.damageMultiplier);
        }
    }
}
