using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spawn Bola", menuName = "Player/Skills/SpawnBola")]
public class SpawnBolaSkill : Skill
{
    public GameObject objectToSpawn;

    public override void StartSkill()
    {
        base.StartSkill();
    }

    public override void Use()
    {
        Instantiate(objectToSpawn, player.transform.position + -player.transform.forward, Quaternion.identity);
    }
}
