using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyTesting : MonoBehaviour
{
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
            animator.SetTrigger("hit");
            AudioManager.instance.PlaySFX2D(damageClip);
        }
    }
}
