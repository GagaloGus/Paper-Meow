using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    static public AudioManager instance;
    public List<AudioSource> activeAudioSources;
    public GameObjPool audioPool;
    private bool isMuted = false;

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
        audioPool = transform.Find("GameObjPoolAudios").gameObject.GetComponent<GameObjPool>();
        AudioListener.volume = 1;
    }

    public void SetMasterVolume(float volume)
    {
        AudioListener.volume = volume;
    }

    public void SetAmbientMusic(string zone)
    {
        GetComponent<AmbientMusicPlayer>().ChangeTagSound(zone);
    }


    //Volume: [0, 1]
    void PlaySFX(AudioClip clip, Vector3 position, bool audio3d, float volume = 1)
    {
        GameObject sourceObj = audioPool.GetFirstInactiveGameObject();
        sourceObj.SetActive(true);
        sourceObj.transform.position = position;

        AudioSource source = sourceObj.GetComponent<AudioSource>();
        activeAudioSources.Add(source);

        source.spatialBlend = ( audio3d ? 1 : 0 );

        source.clip = clip;
        source.volume = volume * AudioListener.volume;
        source.Play();
        StartCoroutine(PlayAudio(source));
    }

    public void PlaySFX2D(AudioClip clip, float volume = 1)
    {
        PlaySFX(clip, Camera.main.transform.position, false ,volume);    
    }

    public void PlaySFX3D(AudioClip clip, Vector3 position, float volume = 1)
    {
        PlaySFX(clip, position, true,volume);    
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


    public void ToggleMute()
    {
        isMuted = !isMuted;

        if (isMuted)
        {
            SetMasterVolume(0);
        }
        else
        {
            SetMasterVolume(1);
        }
    }

    public void ToggleMusicMute()
    {
        AudioSource musicSource = GetComponent<AmbientMusicPlayer>().musicSource;

        if (musicSource.isPlaying)
        {
            musicSource.Pause();
        }
        else
        {
            musicSource.UnPause();
        }
    }
}
