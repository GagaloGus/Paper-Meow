using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BehaviourType { MoveAround, PatrolPoints, Stop, Interact }

public enum AnimationTypes { Idle, IdleTalk, Talk, Laugh }

[System.Serializable]
public class Dialogue
{
    //Texto del dialogo
    [TextArea(3, 5)] public string text;

    //animacion del dialogo
    public AnimationTypes currentAnimation;

    public Sprite npcSpriteFace;

    //si el player habla (aun no implementado)
    public bool playerTalksNext;
    public string[] playerText;

    //el quest que PUEDE devolver
    public Quest newQuest;
}

[System.Serializable]
public class NPCData
{
    //nombre del npc
    public string name;

    //descripcion del npc
    public string description;

    public Sprite icon;

    //id del npc (no se, puede servir)
    public int id;
    public BehaviourType Behaviour;

    //array con todos los dialogos que tiene
    public Dialogue[] dialogues;
}