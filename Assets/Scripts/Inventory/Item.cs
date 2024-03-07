using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu]
public class Item : ScriptableObject
{
    [Header("Info")]
    public string displayName;
    public string ID_name;
    [TextArea(3,5)]public string description;
    public Rarity itemRarity;

    [Header("Gameplay")]
    public Type itemType;
    //Weapon
    [HideInInspector] public float damageMultiplier;
    [HideInInspector] public WeaponType weaponType;
    [HideInInspector] public GameObject itemPrefab;
    //Fruit
    [HideInInspector] public uint healAmount;


    [Header("UI")]
    public Sprite sprite;
    public bool stackable = true;

    private void OnValidate()
    {
        ID_name = this.name;
    }
}
public enum Type { 
    Object,
    QuestItem, 
    Fruit,
    Weapon, 
    PuebloPegatina
}

public enum WeaponType
{
    Garra,
    Sword,
    Hammer,
    Spear
}

public enum Rarity
{
    Common,
    Uncommon,
    Rare,
    Epic,
    Legendary
}

#if UNITY_EDITOR_WIN
[CustomEditor(typeof(Item))]
class ItemEditor : Editor
{
    SerializedProperty itemTypes;
    SerializedProperty weaponTypes, gameobjPrefab, damageMult, heal;

    private void OnEnable()
    {
        damageMult = serializedObject.FindProperty("damageMultiplier");
        itemTypes = serializedObject.FindProperty("itemType");    
        weaponTypes = serializedObject.FindProperty("weaponType");    
        gameobjPrefab = serializedObject.FindProperty("itemPrefab");    
        heal = serializedObject.FindProperty("healAmount");    
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        serializedObject.Update();

        if((Type)itemTypes.enumValueIndex == Type.Weapon)
        {
            EditorGUILayout.LabelField("Weapon Settings", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(weaponTypes);
            EditorGUILayout.PropertyField(damageMult);
            EditorGUILayout.PropertyField(gameobjPrefab);
        }
        else if((Type)itemTypes.enumValueIndex == Type.Fruit)
        {
            EditorGUILayout.LabelField("Fruit Settings", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(heal);
        }

        serializedObject.ApplyModifiedProperties();
    }
}
#endif
