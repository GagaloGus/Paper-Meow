using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    static public AudioManager instance;
    public List<AudioSource> activeAudioSources;
    public AudioSource musicSource; // AudioSource específico para la música ambiente
    public GameObjPool audioPool;

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

        musicSource = transform.Find("AmbientMusic").GetComponent<AudioSource>();
        audioPool = transform.Find("GameObjPoolAudios").gameObject.GetComponent<GameObjPool>();
        AudioListener.volume = 1;
    }

    public void SetMasterVolume(float volume)
    {
        AudioListener.volume = volume;
    }

    //Volume: [0, 1]
    public void PlaySFX(AudioClip clip, Vector3 position, float volume = 1)
    {
        GameObject sourceObj = audioPool.GetFirstInactiveGameObject();
        sourceObj.SetActive(true);
        sourceObj.transform.position = position;

        AudioSource source = sourceObj.GetComponent<AudioSource>();
        activeAudioSources.Add(source);

        source.clip = clip;
        source.volume = volume * AudioListener.volume;
        source.Play();
        StartCoroutine(PlayAudio(source));
    }

    public void PlayMusic(AudioClip clip, Vector3 position , float volume = 1)
    {
        GameObject sourceObj = audioPool.GetFirstInactiveGameObject();
        sourceObj.SetActive(true);
        sourceObj.transform.position = position;

        AudioSource source = sourceObj.GetComponent<AudioSource>();
        activeAudioSources.Add(source);

        source.clip = clip;
        source.volume = volume * AudioListener.volume;
        source.loop = true;
        source.Play();
        StartCoroutine(PlayAudio(source));
    }

    public void ClearAudioList()
    {
        foreach (AudioSource source in activeAudioSources)
        {
            source.Stop();
            source.gameObject.SetActive(false);
        }
        activeAudioSources.Clear();
    }

    IEnumerator PlayAudio(AudioSource source)
    {
        while (source && source.isPlaying)
        {
            yield return new WaitForSeconds(0.05f);
        }
        if (source)
        {
            source.gameObject.SetActive(false);
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

    public void ResumeBackgroundMusic(float MaxVolume = 1)
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
