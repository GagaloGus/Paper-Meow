using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyTesting : MonoBehaviour
{
    [SerializeField] bool died = false;
    SkoStats player;

    public AudioClip damageClip;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<SkoStats>();
    }


    public void TakeDamage(float damage)
    {
        print($"au {damage}");
        AudioManager.instance.PlaySFX2D(damageClip);
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
