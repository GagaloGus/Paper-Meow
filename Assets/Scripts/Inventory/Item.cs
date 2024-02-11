using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item")]
public class Item : ScriptableObject
{
    [Header("Info")]
    public string itemName;
    [TextArea(3,5)]public string description;


    [Header("Gameplay")]
    public Type itemType;

    [Header("UI")]
    public Sprite sprite;
    public bool stackable = true;
}
public enum Type { 
    Fruit, 
    Weapon, 
    QuestItem, 
    Money
}
