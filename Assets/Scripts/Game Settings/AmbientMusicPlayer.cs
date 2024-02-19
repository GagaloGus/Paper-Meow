using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientMusicPlayer : MonoBehaviour
{
    public bool musicActive;
    public string currentZoneTag, previousZoneTag;

    public AmbientSongs[] ambientSongs;


    private void OnEnable()
    {
        GameEventsManager.instance.playerEvents.onPlayerTouchGround += ChangeTagSound;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.playerEvents.onPlayerTouchGround -= ChangeTagSound;
    }

    private void Start()
    {
        previousZoneTag = "asfdfs";
    }

    void ChangeTagSound(string tag)
    {
        currentZoneTag = tag;
        if(previousZoneTag != currentZoneTag && musicActive)
        {
            AmbientSongs ambient = Array.Find(ambientSongs, x => x.tag == tag);
            if(ambient != null)
            {
                Debug.Log($"Cancion encontrada: {tag}");
                //Funciona
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
    public int maxVolume;
    public AudioClip song;
}

