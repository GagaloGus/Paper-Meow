using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Air Current", menuName = "Player/Skills/AirCurrent")]
public class AirCurrentSkill : Skill
{
    public float distance;
    public override void StartSkill()
    {
        base.StartSkill();
    }

    public override void Use()
    {
        player.GetComponent<Rigidbody>().velocity = Vector3.up * distance;
    }
}
