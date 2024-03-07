using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEvents
{
    public event Action<int> onPlayerLevelChange;
    public void PlayerLevelChange(int level)
    {
        if (onPlayerLevelChange != null)
        {
            onPlayerLevelChange(level);
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

    public event Action<int> onDamageTaken;
    public void DamageTaken(int damageTaken)
    {
        if(onDamageTaken != null)
        {
            onDamageTaken(damageTaken);
            GameEventsManager.instance.playerEvents.HealthUpdate();
        }
    }

    public event System.Action onHealthUpdate;
    public void HealthUpdate()
    {
        Debug.Log("whatsapp");
        if(onHealthUpdate != null)
        {
            onHealthUpdate();
        }
    }

    public event System.Action onPlayerDeath;
    public void PlayerDeath()
    {
        if(onPlayerDeath != null)
        {
            onPlayerDeath();
        }
    }
}
