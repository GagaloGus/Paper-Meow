using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : ScriptableObject
{
    public string skillName;

    public bool isUnlocked;
    public bool canBeUnlocked;

    public Skill[] parentSkills;

    public int skillID;
    
    public abstract void Use();

    public bool CheckIfUnlockable()
    {
        foreach(Skill parent in parentSkills)
        {
            if(!parent.isUnlocked)
            {
                return false;
            }
        }

        return true;
    }

    
}
