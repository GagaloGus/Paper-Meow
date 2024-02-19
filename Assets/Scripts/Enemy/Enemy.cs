using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health;
    [SerializeField] bool died = false;
    SkoStats player;
    public int exp;
    public int coinAmount;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<SkoStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0 && !died)
        {
            died = true;
            GameManager.instance.MoneyUpdate(coinAmount);
            GameEventsManager.instance.miscEvents.CoinCollected();
            GameEventsManager.instance.miscEvents.ThingObtained($"{coinAmount} MeowCoins");
            player.GetEXP(exp);
            Destroy(gameObject);

        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        print($"au {damage}");
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
