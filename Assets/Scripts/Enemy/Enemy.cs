using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    GameObject player;
    Quaternion originalRot;
    public float detectionRange;

    [Header("Stats")]
    public float health;
    public float defense;

    [Header("Recompensas")]
    public int exp;
    public int coinAmount;

    [Header("Eventos")]
    public UnityEvent DestroyEvent;

    public AudioClip hitClip;
    public AudioClip deathClip;

    private void Start()
    {
        player = FindObjectOfType<SkoController>().gameObject;
        originalRot = transform.rotation;
    }

    private void Update()
    {
        if(Vector3.Distance(player.transform.position, transform.position) < Mathf.Abs(detectionRange))
        {
            transform.LookAt(CoolFunctions.FlattenVector3(player.transform.position, transform.position.y));
        }
        else
        {
            transform.rotation = originalRot;
        }
    }


    private void OnTriggerEnter(Collider trigger)
    {
        Weapon weapon = trigger.gameObject.GetComponentInParent<Weapon>();
        if (weapon)
        {
            AudioManager.instance.PlaySFX2D(hitClip);
            TakeDamage((player.GetComponent<SkoStats>().currentStats.ATK * weapon.weaponData.damageMultiplier * weapon.weaponData.itemData.damageMultiplier)/defense);
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
