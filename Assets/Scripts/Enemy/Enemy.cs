using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    [Header("Stats")]
    public float health;

    [Header("Recompensas")]
    public int exp;
    public int coinAmount;

    [Header("Eventos")]
    public UnityEvent DestroyEvent;

    public AudioClip deathClip;


    private void OnTriggerEnter(Collider trigger)
    {
        Weapon weapon = trigger.gameObject.GetComponentInParent<Weapon>();
        if (weapon)
        {

            TakeDamage(FindObjectOfType<SkoStats>().currentStats.ATK * weapon.weaponData.damageMultiplier * weapon.weaponData.itemData.damageMultiplier);
        }
    }


    public void TakeDamage(float damage)
    {
        print($"au {damage}");
        health -= damage;

        if (health <= 0)
        {
            GameEventsManager.instance.miscEvents.CoinCollected(coinAmount);
            GameEventsManager.instance.miscEvents.ExperienceGained(exp);
            AudioManager.instance.PlaySFX2D(deathClip);
            DestroyEvent?.Invoke();
            Destroy(gameObject);
        }
    }
}
