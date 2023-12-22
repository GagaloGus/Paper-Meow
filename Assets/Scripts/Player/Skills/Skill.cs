using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : ScriptableObject
{
    public enum UnlockType { SkillTree, Quest}

    public string skillName;
    public int skillID;
    public bool isUnlocked;
    public Skill[] parentSkills;

    public UnlockType unlockType;

    [Tooltip("Solo util en Skill Tree")]
    public bool canBeUnlocked;
    [Tooltip("Solo util en Skill Tree")]
    public int moneyRequired;

    [Tooltip("Solo util en Quest")]
    public Quest questItem;


    public abstract void Use();

    //Si las skills padres estan desbloqueadas y es una
    //skill tipo SkillTree se puede desbloquear esta
    public void CheckIfUnlockable()
    {
        foreach(Skill parent in parentSkills)
        {
            if(!parent.isUnlocked || unlockType != UnlockType.SkillTree)
            {
                canBeUnlocked = false;
            }
        }

        canBeUnlocked = true;
    }

    public void UnlockSkill()
    {
        isUnlocked = true;
    }
}

