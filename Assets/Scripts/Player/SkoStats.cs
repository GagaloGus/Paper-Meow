using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkoStats : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float maxSpeed;
    public float jumpForce;
    [Range(1, 5)]public float runSpeedMult;

    [Header("Combat")]
    public int health;
    public float attackPower;

    [Header("Weapons")]
    public AttackWeaponIDs weaponSelected;
    public enum AttackWeaponIDs { garra, cutter, hammer, spear }


    [Header("Stats")]
    public int money;
    string unlockedSkills;


    //aca estara todo sobre el guardado y cargado de datos
}
