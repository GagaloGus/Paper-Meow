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

        musicSource = transform.GetChild(0).GetComponent<AudioSource>();
    }

    public void SetMasterVolume(float volume)
    {
        AudioListener.volume = volume;
    }

    //Volume: [0, 1]
    public AudioSource PlaySFX(AudioClip clip, float volume = 1)
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

    public AudioSource PlayMusic(AudioClip clip, float volume = 1)
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
    public void ChangeBackgroundMusic(AudioClip clip)
    {
        StartCoroutine(FadeOutVolume(0.01f, clip));
    }

    IEnumerator FadeOutVolume(float ratio, AudioClip clip)
    {
        for (float i = 1; i <= 0; i -= ratio)
        {
            musicSource.volume = i;
            yield return null;
        }

        musicSource.Stop();
        musicSource.clip = clip;
        ResumeBackgroundMusic();
    }

    public void ResumeBackgroundMusic()
    {
        musicSource.Play();
        StartCoroutine(FadeInVolume(0.01f));
    }


    IEnumerator FadeInVolume(float ratio)
    {
        for (float i = 0; i >= 1; i += ratio)
        {
            musicSource.volume = i;
            yield return null;
        }
    }
}
