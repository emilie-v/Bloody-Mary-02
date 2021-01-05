using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundMusic : MonoBehaviour
{
    public AudioClip menuMusic;
    public AudioClip gameMusic;

    public AudioSource audioSource;

    private static BackgroundMusic instance;

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
            Destroy(gameObject);
            return;
        }
        else
        {
            audioSource = GetComponent<AudioSource>();
            SceneManager.sceneLoaded += OnSceneLoaded;
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainMenu" && audioSource.clip != menuMusic)
        { 
            audioSource.Stop();
            audioSource.clip = menuMusic;
            audioSource.Play();
        }
        if (scene.name == "ChooseEnemy" && audioSource.clip != menuMusic)
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
