using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Defeat Quest", menuName = "Quest/Defeat")]
public class DefeatQuest : Quest
{
    public int enemiesToDefeat;
    public int enemiesDefeated;
}
