using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Parameters
{
    public Skill[] parentSkills;
}

public abstract class Skill
{
    public bool isUnlocked;
    public bool canBeUnlocked;
    public Parameters ParentSkills;

    
    public abstract void Use();

    public bool CheckIfUnlockable()
    {
        foreach(Skill parent in ParentSkills.parentSkills)
        {
            if(!parent.isUnlocked)
            {
                return false;
            }
        }

        return true;

    }
}
