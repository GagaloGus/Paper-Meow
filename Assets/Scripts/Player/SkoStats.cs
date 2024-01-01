using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkoStats : MonoBehaviour
{
    public int health;
    public float moveSpeed, maxSpeed, jumpForce;
    [Range(1, 5)]public float runSpeedMult;
    public float attackPower;

    string unlockedSkills;
    public int money;

    public enum attackWeaponIDs { garra, cutter }
    public attackWeaponIDs weaponSelected;
    
    //aca estara todo sobre el guardado y cargado de datos
}
