using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientMusicPlayer : MonoBehaviour
{
    public string currentZoneTag, previousZoneTag;

    public AmbientSongs[] ambientSongs;


    private void OnEnable()
    {
        GameEventsManager.instance.playerEvents.onPlayerTouchGround += ChangeTagSound;
    }

    private void Start()
    {
        previousZoneTag = "Pueblo1";
    }

    void ChangeTagSound(string tag)
    {
        currentZoneTag = tag;
        if(previousZoneTag != currentZoneTag)
        {
            AmbientSongs ambient = Array.Find(ambientSongs, x => x.tag == tag);
            if(ambient != null)
            {
                AudioManager.instance.ChangeBackgroundMusic(ambient.song);
            }
            else
            {
                Debug.LogWarning($"No se encontro la musica de ambiente {tag}");
            }

            previousZoneTag = tag;
        }
        
    }
}

[System.Serializable]
public class AmbientSongs
{
    public string tag;
    public AudioClip song;
}

