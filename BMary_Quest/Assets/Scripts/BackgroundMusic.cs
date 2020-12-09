using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundMusic : MonoBehaviour
{
    public AudioClip menuMusic;
    public AudioClip gameMusic;

    AudioSource audioSource;

    private static BackgroundMusic instance = null;

    public static BackgroundMusic Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            audioSource = GetComponent<AudioSource>();
            SceneManager.sceneLoaded += OnSceneLoaded;
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainMenu" && audioSource.clip != menuMusic)
        { 
            audioSource.Stop();
            audioSource.clip = menuMusic;
            audioSource.Play();
        }

        if (scene.name == "GameBoard")
        {
            audioSource.Stop();
            audioSource.clip = gameMusic;
            audioSource.Play();
        }
    }
}
