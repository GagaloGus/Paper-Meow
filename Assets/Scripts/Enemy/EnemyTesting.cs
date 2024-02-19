using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

<<<<<<<< HEAD:Assets/Scripts/Enemy/Enemy.cs
public class Enemy : MonoBehaviour
========
public class EnemyTesting : MonoBehaviour
>>>>>>>> d33cfd722825fc1327e6852d3c22dd999d44e5e0:Assets/Scripts/Enemy/EnemyTesting.cs
{
    [SerializeField] bool died = false;
    SkoStats player;
<<<<<<<< HEAD:Assets/Scripts/Enemy/Enemy.cs
    public int exp;
    public int coinAmount;
========
    Material material;
>>>>>>>> d33cfd722825fc1327e6852d3c22dd999d44e5e0:Assets/Scripts/Enemy/EnemyTesting.cs
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<SkoStats>();
    }

    // Update is called once per frame
    void Update()
    {
<<<<<<<< HEAD:Assets/Scripts/Enemy/Enemy.cs
        if(health <= 0 && !died)
        {
            died = true;
            GameManager.instance.MoneyUpdate(coinAmount);
            GameEventsManager.instance.miscEvents.CoinCollected();
            GameEventsManager.instance.miscEvents.ThingObtained($"{coinAmount} MeowCoins");
            player.GetEXP(exp);
            Destroy(gameObject);

        }
========

>>>>>>>> d33cfd722825fc1327e6852d3c22dd999d44e5e0:Assets/Scripts/Enemy/EnemyTesting.cs
    }

    public void TakeDamage(float damage)
    {
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
