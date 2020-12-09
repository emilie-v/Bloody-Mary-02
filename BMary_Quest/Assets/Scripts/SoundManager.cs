using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource effectSource;
    public AudioClip[] audioClips;

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

    void GetAllComponents()
    {
        audioClips = new AudioClip[1];
        audioClips[0] = Resources.Load<AudioClip>("Audio/BackButton_Sound");
    }

    public void PlaySounds(AudioClip clip)
    {
        effectSource.clip = clip;
        effectSource.Play();
    }

    public void SelectButtonSound()
    {
        effectSource.clip = audioClips[(int)SoundEnum.BackButton];
        effectSource.Play();
    }

    public enum SoundEnum : int
    {
        BackButton
    }
}
