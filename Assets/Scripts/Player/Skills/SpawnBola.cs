using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spawn Bola", menuName = "Player/Skills/SpawnBola")]
public class SpawnBola : Skill
{
    public GameObject objectToSpawn;
    public override void Use()
    {
        Instantiate(objectToSpawn);
    }
}
