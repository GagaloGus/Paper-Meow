using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BehaviourType { MoveAround, PatrolPoints, Stop, Interact }

public enum AnimationTypes { Idle, Talk, Talk_idle, Laugh }

[System.Serializable]
public class NPCData
{
    //nombre del npc
    public string name;

    //descripcion del npc
    public string description;

    [HideInInspector]
    public Quaternion originalRot;

    public BehaviourType Behaviour;
}