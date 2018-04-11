using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource musicSource;
    public AudioSource sfxSource;

    public AudioClip musicEnvironment1;
    public AudioClip musicMenu;
    public AudioClip musicLevel1;
    public AudioClip musicContinue;
    public AudioClip sfxGO;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void PlayMusic(AudioClip audioClip)
    {
        musicSource.clip = audioClip;
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void PlayMusicEnvironment1()
    {
        PlayMusic(musicEnvironment1);
    }

    public void PlayMusicLevel1()
    {
        PlayMusic(musicLevel1);
    }

    public void PlayMusicContinue()
    {
        PlayMusic(musicContinue);
    }

    public void PlayMusicMenu()
    {
        PlayMusic(musicMenu);
    }

    public void PlaySfxGO()
    {
        PlaySoundFX(sfxGO);
    }


    public void PlaySoundFX(AudioClip audioClip)
    {
        if (sfxSource != null && audioClip != null)
        {
            sfxSource.clip = audioClip;
            sfxSource.Play();
        }
    }
}
