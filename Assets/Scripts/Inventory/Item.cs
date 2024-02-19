using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu]
public class Item : ScriptableObject
{
    [Header("Info")]
    public string itemName;
    [TextArea(3,5)]public string description;


    [Header("Gameplay")]
    public Type itemType;
    [HideInInspector] public WeaponType weaponType;
    [HideInInspector] public GameObject itemPrefab;

    [Header("UI")]
    public Sprite sprite;
    public bool stackable = true;
}
public enum Type { 
    Object,
    QuestItem, 
    Fruit,
    Weapon, 
    Money
}

public enum WeaponType
{
    Garra,
    Sword,
    Hammer,
    Spear
}

#if UNITY_EDITOR_WIN
[CustomEditor(typeof(Item))]
class ItemEditor : Editor
{
    SerializedProperty itemTypes;
    SerializedProperty weaponTypes, gameobjPrefab;

    private void OnEnable()
    {
        itemTypes = serializedObject.FindProperty("itemType");    
        weaponTypes = serializedObject.FindProperty("weaponType");    
        gameobjPrefab = serializedObject.FindProperty("itemPrefab");    
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        serializedObject.Update();

        if((Type)itemTypes.enumValueIndex == Type.Weapon)
        {
            EditorGUILayout.LabelField("Weapon Settings", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(weaponTypes);
            EditorGUILayout.PropertyField(gameobjPrefab);
            
        }

        serializedObject.ApplyModifiedProperties();
    }
}
#endif
