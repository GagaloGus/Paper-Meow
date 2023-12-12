using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BehaviourType { MoveAround, PatrolPoints, Stop, Interact }

public enum AnimationTypes { Idle, IdleTalk, Talk, Laugh }

[System.Serializable]
public class Dialogue
{
    public string text;
    public AnimationTypes currentAnimation;
    public bool playerTalksNext;
    public string[] playerText;
    public Quest newQuest;
}

[System.Serializable]
public class NPCData
{
    public string name;
    public string description;
    public int id;
    public Vector3 directionFacing;
    public BehaviourType Behaviour;

    public Dialogue[] dialogues;
}
