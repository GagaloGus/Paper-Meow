using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EnemyBarrera : MonoBehaviour
{
    public List<GameObject> enemigos = new List<GameObject>();

    [Header("Stats")]
    [SerializeField] int health;
    [SerializeField] int defense;
    [SerializeField] bool canBeAttacked;

    [Header("SFX")]
    public AudioClip noAttackSFX;
    public AudioClip hitAttackSFX;

    [Header("Canvas")]
    Slider healthSlider;

    public UnityEvent brokenBarrierEvent;

    private void Start()
    {
        //Si la cantidad de enemigos es mayor que 0, no lo puedes atacar
        canBeAttacked = enemigos.Count == 0;

        healthSlider = GetComponentInChildren<Slider>();
        healthSlider.maxValue = health;
        healthSlider.value = health;
        healthSlider.gameObject.SetActive(false);
    }

    public void EnemyDied(GameObject enemy)
    {
        enemigos.Remove(enemy);
        canBeAttacked = enemigos.Count == 0;
    }

    private void OnTriggerEnter(Collider trigger)
    {
        Weapon weapon = trigger.gameObject.GetComponentInParent<Weapon>();
        if (weapon && canBeAttacked)
        {
            TakeDamage(FindObjectOfType<SkoStats>().currentStats.ATK * weapon.weaponData.damageMultiplier * weapon.weaponData.itemData.damageMultiplier);
            AudioManager.instance.PlaySFX2D(hitAttackSFX);
        }

        if(!canBeAttacked)
        {
            AudioManager.instance.PlaySFX2D(noAttackSFX);
        }
    }

    public void TakeDamage(float damage)
    {
        healthSlider.gameObject.SetActive(true);
        int damageInt = Mathf.CeilToInt(damage/defense);
        print($"au {damageInt}");
        health -= damageInt;
        StartCoroutine(DecreaseHealthSlider(damageInt));

        if (health <= 0)
        {
            brokenBarrierEvent?.Invoke();
            print("barrera caida");
            Destroy(gameObject);
        }
    }

    IEnumerator DecreaseHealthSlider(int damage)
    {
        float ogHealth = healthSlider.value;
        while(healthSlider.value > ogHealth-damage)
        {
            healthSlider.value -= 0.3f;
            yield return null;
        }
    }
}
