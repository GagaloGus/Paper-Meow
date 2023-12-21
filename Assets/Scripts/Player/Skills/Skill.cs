using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : ScriptableObject
{
    public enum UnlockType { SkillTree, Quest}

    public string skillName;
    public UnlockType unlockType;

    public bool isUnlocked;

    [HideInInspector]
    public bool canBeUnlocked;

    public Skill[] parentSkills;

    public int skillID;

    [HideInInspector]
    public int moneyRequired;



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
