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

    [Header("Stats")]
    public int health;
    public float BaseATK;
    public float BaseSPD;
    public int BaseExpReq;
    public int EXP_incMult;
    public Stats currentStats;


    [Header("Weapons")]
    public AttackWeaponIDs weaponSelected;
    public enum AttackWeaponIDs { garra, cutter, hammer, spear }


    [Header("Stats")]
    public int money;
    string unlockedSkills;


    private void Start()
    {
        currentStats.EXP = 0;
        currentStats.currentLevel = 0;
        currentStats.ATK = BaseATK;
        currentStats.SPD = BaseSPD;

        ReCalculateEXP_Required();
    }
    public void GetEXP(int xpIncrease)
    {
        print($"{xpIncrease} experiencia obtenida");
        currentStats.EXP += xpIncrease;

        if(currentStats.EXP >= currentStats.EXP_RequiredNextLvl)
        {
            LevelUp();
        }
    }

    public void LevelUp()
    {
        currentStats.currentLevel++;

        currentStats.ATK += Random.Range(currentStats.ATK_increaseRange.x, currentStats.ATK_increaseRange.y);
        currentStats.SPD += Random.Range(currentStats.SPD_increaseRange.x, currentStats.SPD_increaseRange.y);

        int remaining_EXP = currentStats.EXP - currentStats.EXP_RequiredNextLvl;
        currentStats.EXP = remaining_EXP;

        ReCalculateEXP_Required();
    }

    void ReCalculateEXP_Required()
    {
        currentStats.EXP_RequiredNextLvl = BaseExpReq + currentStats.currentLevel * EXP_incMult;
    }

    //aca estara todo sobre el guardado y cargado de datos
}

[System.Serializable]
public class Stats
{
    public int EXP;
    public int EXP_RequiredNextLvl;
    public int currentLevel;
    public float ATK;
    public Vector2Int ATK_increaseRange;
    public float SPD;
    public Vector2Int SPD_increaseRange;
}
