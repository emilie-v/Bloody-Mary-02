using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public AudioListener audioListener;
    public AudioSource effectSource;
    public AudioSource mainMenuMusic;
    public AudioClip[] audioClips;
    public Image musicMuteButton;
    public Image mainMuteButton;

    private Sprite highVolume;
    private Sprite lowVolume;
    private Sprite noVolume;
    
    private float mainVolume = 1f;
    private float setMainVolume = 1f;
    private bool mainVolumeMute;
    
    private float mainMenuMusicVolume = 1f;
    private float setMainMenuMusicVolume = 1f;
    private bool mainMenuMusicMute;

    public float lowPitchRange = 0.95f;
    public float highPitchRange = 1.05f;

    public static SoundManager Instance = null;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        GetAllComponents();
    }
    
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainMenu")
        {
            GetAllComponents();
        }
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Update()
    {
        mainMenuMusic.volume = mainMenuMusicVolume;
    }
    
    void GetAllComponents()
    {
        audioListener = GameObject.Find("Main Camera").GetComponent<AudioListener>();
        
        audioClips = new AudioClip[12];
        audioClips[0] = Resources.Load<AudioClip>("Audios/Audios_Select_Sound");
        audioClips[1] = Resources.Load<AudioClip>("Audios/Audios_Arrows_Sound");
        audioClips[2] = Resources.Load<AudioClip>("Audios/Audios_Locked_Sound");
        audioClips[3] = Resources.Load<AudioClip>("Audios/Audios_BackExit_Sound");
        audioClips[4] = Resources.Load<AudioClip>("Audios/Audios_ActivateStaff_Sound");
        audioClips[5] = Resources.Load<AudioClip>("Audios/Audios_MaryMark_Sound");
        audioClips[6] = Resources.Load<AudioClip>("Audios/Audios_EnemyMark_Sound");
        audioClips[7] = Resources.Load<AudioClip>("Audios/Audios_MenuButtons_Sound");
        audioClips[8] = Resources.Load<AudioClip>("Audios/Audios_CashOut_Sound");
        audioClips[9] = Resources.Load<AudioClip>("Audios/Audios_EndTurn_Sound");
        audioClips[10] = Resources.Load<AudioClip>("Audios/Audios_WinState_Sound");
        audioClips[11] = Resources.Load<AudioClip>("Audios/Audios_LoseState_Sound");

        highVolume = Resources.Load<Sprite>("Sprites/GUI/GUI_Options/GUI_Options_HighVolume");
        lowVolume = Resources.Load<Sprite>("Sprites/GUI/GUI_Options/GUI_Options_LowVolume");
        noVolume = Resources.Load<Sprite>("Sprites/GUI/GUI_Options/GUI_Options_NoVolume");

        mainMuteButton = GameObject.Find("Options_Panel/Background/Audio/MainVolume/MainVolume_MuteButton").GetComponent<Image>();
        musicMuteButton = GameObject.Find("Options_Panel/Background/Audio/MusicVolume/MusicVolume_MuteButton").GetComponent<Image>();
    }

    public void UpdateMainVolume(float volume)
    {
        setMainVolume = volume;
        AudioListener.volume = setMainVolume;

        UpdateSprite(AudioListener.volume, mainMuteButton);
    }

    public void MuteMainVolume()
    {
        if (mainVolumeMute)
        {
            AudioListener.volume = setMainVolume;
            mainVolumeMute = false;
        }
        else if (!mainVolumeMute)
        {
            AudioListener.volume = 0;
            mainVolumeMute = true;
        }
        
        UpdateSprite(AudioListener.volume, mainMuteButton);
    }
    
    public void UpdateMainMenuMusicVolume(float volume)
    {
        setMainMenuMusicVolume = Mathf.Pow(volume, 2);
        mainMenuMusicVolume = setMainMenuMusicVolume;

        UpdateSprite(mainMenuMusicVolume, musicMuteButton);
    }

    private void UpdateSprite(float volume, Image button)
    {
        if (volume > 0.5f)
        {
            button.sprite = highVolume;
        }
        else if (volume > 0)
        {
            button.sprite = lowVolume;
        }
        else if (volume <= 0)
        {
            button.sprite = noVolume;
        }
    }

    public void MuteMainMenuMusicVolume()
    {
        if (mainMenuMusicMute)
        {
            mainMenuMusicVolume = setMainMenuMusicVolume;
            mainMenuMusicMute = false;
        }
        else if (!mainMenuMusicMute)
        {
            mainMenuMusicVolume = 0;
            mainMenuMusicMute = true;
        }
        
        UpdateSprite(mainMenuMusicVolume, musicMuteButton);
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

    public void ArrowButtonSound()
    {
        effectSource.clip = audioClips[(int)SoundEnum.ArrowButton];
        effectSource.Play();
    }

    public void LockedWarningPopUpSound()
    {
        effectSource.clip = audioClips[(int)SoundEnum.LockedWarning];
        effectSource.Play();
    }

    public void BackExitButtonSound()
    {
        effectSource.clip = audioClips[(int)SoundEnum.BackExitButton];
        effectSource.Play();
    }

    public void ActivateStaffButtonSound()
    {
        effectSource.clip = audioClips[(int)SoundEnum.ActivateStaffButton];
        effectSource.Play();
    }

    public void MaryMarkPlacedSound()
    {
        effectSource.clip = audioClips[(int)SoundEnum.MaryMarkPlaced];
        effectSource.Play();
    }
    public void EnemyMarkPlacedSound()
    {
        effectSource.clip = audioClips[(int)SoundEnum.EnemyMarkPlaced];
        effectSource.Play();
    }

    public void MenuButtonSound()
    {
        effectSource.clip = audioClips[(int)SoundEnum.MenuButton];
        effectSource.Play();
    }

    public void CashOutButtonSound()
    {
        effectSource.clip = audioClips[(int)SoundEnum.CashOutButton];
        effectSource.Play();
    }

    public void EndTurnButtonSound()
    {
        effectSource.clip = audioClips[(int)SoundEnum.EndTurnButton];
        effectSource.Play();
    }

    public void WinStateSound()
    {
        effectSource.clip = audioClips[(int)SoundEnum.WinState];
        effectSource.Play();
    }

    public void LoseStateSound()
    {
        effectSource.clip = audioClips[(int)SoundEnum.LoseState];
        effectSource.Play();
    }

    public enum SoundEnum : int
    {
        SelectButton,
        ArrowButton,
        LockedWarning,
        BackExitButton,
        ActivateStaffButton,
        MaryMarkPlaced,
        EnemyMarkPlaced,
        MenuButton,
        CashOutButton,
        EndTurnButton,
        WinState,
        LoseState
    }
}
