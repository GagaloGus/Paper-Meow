using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public float weaponDamageMult;
    protected GameObject player;

    protected virtual void Start()
    {
        player = GetComponentInParent<SkoController>().gameObject;
    }
}
