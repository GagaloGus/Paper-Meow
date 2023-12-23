using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spawn Bola", menuName = "Player/Skills/SpawnBola")]
public class SpawnBolaSkill : Skill
{
    public GameObject objectToSpawn;

    public override void StartSkill(GameObject owner)
    {
        base.StartSkill(owner);
    }

    public override void Use(GameObject owner)
    {
        Instantiate(objectToSpawn, owner.transform.position + -owner.transform.forward, Quaternion.identity);
    }
}
