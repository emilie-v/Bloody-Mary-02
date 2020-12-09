using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource effectSource;
    public AudioSource mainMenuMusic;
    public AudioClip[] audioClips;

    private float mainMenuMusicVolume = 1f;

    public float lowPitchRange = 0.95f;
    public float highPitchRange = 1.05f;

    public static SoundManager Instance = null;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else if( Instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        GetAllComponents();
    }

    private void Update()
    {
        mainMenuMusic.volume = mainMenuMusicVolume;
    }

    void GetAllComponents()
    {
        audioClips = new AudioClip[1];
        audioClips[0] = Resources.Load<AudioClip>("Audios/Audios_Select_Sound");
    }

    public void PlaySounds(AudioClip clip)
    {
        effectSource.clip = clip;
        effectSource.Play();
    }

    public void SelectButtonSound()
    {
        effectSource.clip = audioClips[(int)SoundEnum.SelectButton];
        effectSource.Play();
    }

    public void updateMainMenuMusicVolume(float volume)
    {
        mainMenuMusicVolume = volume;
    }

    public enum SoundEnum : int
    {
        SelectButton
    }
}
