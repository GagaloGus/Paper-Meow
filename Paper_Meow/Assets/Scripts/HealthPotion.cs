using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : Potion
{
    public int HealAmount;
    public HealthPotion(string name, int value, int HealAmount) : base(name, value)
    {
        this.HealAmount = HealAmount;
    }
    public override void UsePotion(player player)
    {
       player.Healing(HealAmount);
    }
}
