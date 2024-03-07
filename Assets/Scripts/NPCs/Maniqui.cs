using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maniqui : MonoBehaviour
{
    public Quest quest;
    public int hitAddCount;
    Animator animator;
    public AudioClip damageClip;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        transform.forward = CoolFunctions.FlattenVector3(-Camera.main.transform.forward);
    }

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
        animator.SetTrigger("hit");
        AudioManager.instance.PlaySFX2D(damageClip);

        if(quest != null)
        {
            QuestManager.instance.AddQuestItem(quest, hitAddCount);
        }
    }
}
