using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    static public AudioManager instance;
    public List<AudioSource> activeAudioSources;
    public AudioSource musicSource; // AudioSource específico para la música ambiente

    void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);

        }
        else
        {
            instance = this;
            activeAudioSources = new List<AudioSource>();
            DontDestroyOnLoad(gameObject);
        }
    }

    public void SetMasterVolume(float volume)
    {
        AudioListener.volume = volume;
    }

    //Volume: [0, 1]
    public AudioSource PlayAudio(AudioClip clip, float volume = 1)
    {
        GameObject sourceObj = new GameObject(clip.name);
        activeAudioSources.Add(sourceObj.AddComponent<AudioSource>());
        AudioSource source = sourceObj.GetComponent<AudioSource>();
        source.clip = clip;
        source.volume = volume * AudioListener.volume;
        source.Play();
        StartCoroutine(PlayAudio(source));
        return source;
    }

    public AudioSource PlayAudioOnLoop(AudioClip clip, float volume = 1)
    {
        GameObject sourceObj = new GameObject(clip.name);
        activeAudioSources.Add(sourceObj.AddComponent<AudioSource>());
        AudioSource source = sourceObj.GetComponent<AudioSource>();
        source.clip = clip;
        source.volume = volume * AudioListener.volume;
        source.loop = true;
        source.Play();
        return source;
    }

    public void ClearAudioList()
    {
        foreach (AudioSource source in activeAudioSources)
        {
            Destroy(source.gameObject);
        }
        activeAudioSources.Clear();
    }

    IEnumerator PlayAudio(AudioSource source)
    {
        while (source && source.isPlaying)
        {
            yield return null;
        }
        if (source)
        {
            activeAudioSources.Remove(source);
            Destroy(source.gameObject);
        }
    }
    // Método para reproducir la música ambiente
    public void PlayBackgroundMusic(AudioClip clip, float volume = 1)
    {
        if (musicSource != null)
        {
            musicSource.Stop();
            Destroy(musicSource.gameObject);
        }

        GameObject sourceObj = new GameObject(clip.name);
        musicSource = sourceObj.AddComponent<AudioSource>();
        musicSource.clip = clip;
        musicSource.volume = volume * AudioListener.volume;
        musicSource.loop = true;
        musicSource.Play();
        activeAudioSources.Add(musicSource);
    }
    public void StopBackgroundMusic()
    {
        if (musicSource != null)
        {
            musicSource.Stop();
            activeAudioSources.Remove(musicSource);
        }
    }
}
