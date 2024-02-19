using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEvents
{
    public event Action<int> onExperienceGained;
    public void ExperienceGained(int experience)
    {
        if (onExperienceGained != null)
        {
            onExperienceGained(experience);
        }
    }

    public event Action<int> onPlayerLevelChange;
    public void PlayerLevelChange(int level)
    {
        if (onPlayerLevelChange != null)
        {
            onPlayerLevelChange(level);
        }
    }

    public event Action<int> onPlayerExperienceChange;
    public void PlayerExperienceChange(int experience)
    {
        if (onPlayerExperienceChange != null)
        {
            onPlayerExperienceChange(experience);
        }
    }

    public event Action<string> onPlayerTouchGround;
    public void PlayerSendGroundTag(string tag)
    {
        if(onPlayerTouchGround != null)
        {
            onPlayerTouchGround(tag);
        }
    }

    public event Action<bool> onHouseEnterChange;
    public void HouseEnterChange(bool isInside)
    {
        if(onHouseEnterChange != null)
        {
            onHouseEnterChange(isInside);
        }
    }

    public event Action<bool> onPlayerTouchTutTrigger;
    public void PlayerTouchedWaitTrigger()
    {
        if(onPlayerTouchTutTrigger != null) 
        {
            onPlayerTouchTutTrigger(true);
        }
    }

    public event Action<Skill> onSkillSwapped;
    public void SkillSwapped(Skill skill)
    {
        if (onSkillSwapped != null)
        {
            onSkillSwapped(skill);
        }
    }

    public event Action<Skill> onSkillUsed;
    public void SkillUsed(Skill skill)
    {
        if (onSkillUsed != null)
        {
            onSkillUsed(skill);
        }
    }
}
