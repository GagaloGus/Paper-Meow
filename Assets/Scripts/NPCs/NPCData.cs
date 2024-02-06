using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NPCData
{
    //nombre del npc
    public string name;

    //descripcion del npc
    public string description;

    [HideInInspector] //rotacion original del npc, se auto asigna en el DialogueTrigger
    public Quaternion originalRot;
}