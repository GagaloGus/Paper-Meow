using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : ScriptableObject
{
    public enum UnlockType { SkillTree, Quest}

    public string skillName;
    [TextArea(3,1)] public string skillDescription;

    public int skillID;
    public Sprite skillTreeSprite;
    public bool isUnlocked;
    public bool canBeUnlocked;
    public Skill[] parentSkills;
    public Skill[] childSkills;
    public float cooldown;

    public UnlockType unlockType;

    [Tooltip("Solo util en Skill Tree")]
    public int moneyRequired;

    [Tooltip("Solo util en Quest")]
    public Item questItem;


    public virtual void StartSkill(GameObject owner)
    {

    }
    
    public abstract void Use(GameObject owner);

    //Si las skills padres estan desbloqueadas se puede desbloquear esta
    public void CheckIfUnlockable()
    {
        foreach(Skill parent in parentSkills)
        {
            if(!parent.isUnlocked)
            {
                canBeUnlocked = false;
                return;
            }
        }

        canBeUnlocked = true;
    }

    public void UnlockSkill()
    {
        isUnlocked = true;
    }
}

