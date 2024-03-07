using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NPCData
{
    //nombre del npc
    public string ID_name;
    public string displayName;
    //public NPCName type;

    //descripcion del npc
    public string description;

    public float typingSpeedMult = 1;

    [Header("Sound Effects")]
    [Range(0,1)] public float typingVolume;
    public AudioClip[] typingSfxs;

    [HideInInspector] //rotacion original del npc, se auto asigna
    public Quaternion originalRot;

    public Animator iconAnimator;
}

public enum NPCName { Pirate, Beeko }
