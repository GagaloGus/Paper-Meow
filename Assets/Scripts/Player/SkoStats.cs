using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkoStats : MonoBehaviour
{
    public int health;
    public float moveSpeed, jumpForce;
    [Range(1, 5)]public float runSpeedMult;
    public float attackPower;

    string unlockedSkills;

    public enum attackWeaponIDs { garra, cutter }
    public attackWeaponIDs weaponSelected;

    // Start is called before the first frame update
    void Start()
    {
        //unlockedSkills = SkillManager.instance.get_UnlockIDs;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
