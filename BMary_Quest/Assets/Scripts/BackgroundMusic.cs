using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public bool menuMusicPlaying;

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
        menuMusicPlaying = true;

        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
