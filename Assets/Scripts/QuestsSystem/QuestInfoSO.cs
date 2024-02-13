using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestInfoSO", menuName = "Quests/QuestInfoSO", order = 1)]
public class QuestInfoSO : ScriptableObject
{
    [field:SerializeField] public string id { get; private set; }

    [Header("General")]
    public string displayName;

    [Header("Requirements")]
    public int levelRequirement;
    public QuestInfoSO[] questPrerequisties;

    [Header("Steps")]
    public GameObject[] questStepPrefabs;

    [Header("Rewards")]
    public int coinReward;
    public Item weaponReward;
    public int expReward;

    private void OnValidate()
    {
        //El id siempre sea el nombre del Scrip Object
        #if UNITY_EDITOR
        id = this.name;
        UnityEditor.EditorUtility.SetDirty(this);
        #endif
    }
}
