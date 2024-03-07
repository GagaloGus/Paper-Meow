using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkoStats : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float jumpForce;
    [Range(1, 5)]public float runSpeedMult;

    [Header("Stats")]
    public int MaxLevel;
    public int health;
    public float BaseATK;
    public float BaseSPD;
    public int BaseExpReq;
    public int EXP_incMult;
    public Stats currentStats;


    [Header("Weapons")]
    public AttackWeaponIDs weaponSelected;
    public enum AttackWeaponIDs { garra, cutter, hammer, spear }

    string unlockedSkills;

    private void OnEnable()
    {
        GameEventsManager.instance.miscEvents.onExperienceGained += GetEXP;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.miscEvents.onExperienceGained -= GetEXP;
    }

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
        if(currentStats.currentLevel < MaxLevel)
        {
            currentStats.EXP += xpIncrease;

            while(currentStats.EXP >= currentStats.EXP_RequiredNextLvl)
            {
                LevelUp();
            }
        }
        else
        {
            print("Nivel maximo alcanzado");
        }

    }

    public void LevelUp()
    {
        currentStats.currentLevel++;

        currentStats.ATK += Random.Range(currentStats.ATK_increaseRange.x, currentStats.ATK_increaseRange.y);
        currentStats.SPD += Random.Range(currentStats.SPD_increaseRange.x, currentStats.SPD_increaseRange.y);

        int remaining_EXP = currentStats.EXP - currentStats.EXP_RequiredNextLvl;
        currentStats.EXP = remaining_EXP;

        GameEventsManager.instance.playerEvents.PlayerLevelChange(currentStats.currentLevel);

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
