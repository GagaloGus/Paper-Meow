using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientMusicPlayer : MonoBehaviour
{
    public bool musicActive;
    public string currentZoneTag, previousZoneTag;

    public AudioSource musicSource; // AudioSource específico para la música ambiente

    public AmbientSongs[] ambientSongs;

    private void Start()
    {
        previousZoneTag = "asfdfs";
        musicSource = transform.Find("AmbientMusic").GetComponent<AudioSource>();
    }

    public void ChangeTagSound(string tag)
    {
        currentZoneTag = tag;
        if(previousZoneTag != currentZoneTag && currentZoneTag != "IgnoreTag" && musicActive && !GameManager.instance.isTutorial)
        {
            AmbientSongs ambient = Array.Find(ambientSongs, x => x.tag == tag);
            if(ambient != null)
            {
                Debug.Log($"Cancion encontrada: {tag}");
                //Funciona
                ChangeBackgroundMusic(ambient.song);
            }
            else
            {
                Debug.LogWarning($"No se encontro la musica de ambiente {tag}");
            }

            previousZoneTag = tag;
        }
        
    }

    // Método para reproducir la música ambiente
    public void ChangeBackgroundMusic(AudioClip clip)
    {
        print($"Musica cambiada a {clip.name}");
        StartCoroutine(FadeOutVolume(0.005f, clip));
    }

    IEnumerator FadeOutVolume(float ratio, AudioClip clip)
    {
        float maxVolume = musicSource.volume;
        for (float i = 1; i >= maxVolume; i -= ratio)
        {
            musicSource.volume = i;
            yield return new WaitForSeconds(0.005f);
        }

        musicSource.clip = clip;
        ResumeBackgroundMusic();
    }

    public void ResumeBackgroundMusic(float MaxVolume = 1f)
    {
        print($"Musica reproducida a {musicSource.clip.name}");
        StartCoroutine(FadeInVolume(0.005f, MaxVolume));
    }

    public void ResumeBackgroundMusic(AudioClip clip, float MaxVolume = 1)
    {
        musicSource.clip = clip;
        ResumeBackgroundMusic(MaxVolume);
    }


    IEnumerator FadeInVolume(float ratio, float MaxVolume = 1)
    {
        musicSource.Play();
        for (float i = 0; i <= MaxVolume; i += ratio)
        {
            musicSource.volume = i;
            yield return new WaitForSeconds(0.005f);
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

