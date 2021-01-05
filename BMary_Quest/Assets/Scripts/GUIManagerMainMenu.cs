using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GUIManagerMainMenu : MonoBehaviour
{
    [SerializeField] public GameObject optionsPanel;
    [SerializeField] public Slider volumeSlider;
    [SerializeField] public Slider musicSlider;
    [SerializeField] public Slider sfxSlider;
    [SerializeField] public GameObject mainMuteButton;
    [SerializeField] public GameObject musicMuteButton;
    [SerializeField] public GameObject sfxMuteButton;
    [SerializeField] public GameObject fullscreenButton;
    
    public AudioSource mainMenuMusic;
    public AudioSource sfxSource;

    private Sprite highVolume;
    private Sprite lowVolume;
    private Sprite noVolume;
    
    private float sfxVolume = 1f;
    private float setSFXVolume = 1f;
    private bool sfxVolumeMute;
    
    private float setMainVolume = 1f;
    private bool mainVolumeMute;
    
    private float mainMenuMusicVolume = 1f;
    private float setMainMenuMusicVolume = 1f;
    private bool mainMenuMusicMute;

    private void Start()
    {
        GetComponents();
    }
    
    private void Update()
    {
        mainMenuMusic.volume = mainMenuMusicVolume;
        sfxSource.volume = sfxVolume;

        HotKeys();
    }

    void GetComponents()
    {
        optionsPanel.SetActive(true);

        mainMenuMusic = GameObject.Find("AudioSource").GetComponent<AudioSource>();
        sfxSource = GameObject.Find("SoundManager").GetComponent<AudioSource>();
            
        volumeSlider = GameObject.Find("MainVolume_Slider").GetComponent<Slider>();
        volumeSlider.onValueChanged.AddListener(UpdateMainVolume);
        volumeSlider.value = AudioListener.volume;
        
        musicSlider = GameObject.Find("MusicVolume_Slider").GetComponent<Slider>();
        musicSlider.onValueChanged.AddListener(UpdateMainMenuMusicVolume);
        musicSlider.value = Mathf.Sqrt(mainMenuMusic.volume);
        
        sfxSlider = GameObject.Find("SFXVolume_Slider").GetComponent<Slider>();
        sfxSlider.onValueChanged.AddListener(UpdateSFXVolume);
        sfxSlider.value = Mathf.Sqrt(sfxSource.volume);

        mainMuteButton = GameObject.Find("Options_Panel/Background/Audio/MainVolume/MainVolume_MuteButton");
        mainMuteButton.GetComponent<Button>().onClick.AddListener(MuteMainVolume);
        
        musicMuteButton = GameObject.Find("Options_Panel/Background/Audio/MusicVolume/MusicVolume_MuteButton");
        musicMuteButton.GetComponent<Button>().onClick.AddListener(MuteMainMenuMusicVolume);
        
        sfxMuteButton = GameObject.Find("Options_Panel/Background/Audio/SFXVolume/SFXVolume_MuteButton");
        sfxMuteButton.GetComponent<Button>().onClick.AddListener(MuteSFXVolume);

        fullscreenButton = GameObject.Find("Options_Panel/Background/Fullscreen/Fullscreen_Text/Fullscreen_Checkbox");
        fullscreenButton.GetComponent<Button>().onClick.AddListener(Fullscreen);
        
        highVolume = Resources.Load<Sprite>("Sprites/GUI/GUI_Options/GUI_Options_HighVolume");
        lowVolume = Resources.Load<Sprite>("Sprites/GUI/GUI_Options/GUI_Options_LowVolume");
        noVolume = Resources.Load<Sprite>("Sprites/GUI/GUI_Options/GUI_Options_NoVolume");

        optionsPanel.SetActive(false);
    }
    
    public void UpdateMainVolume(float volume)
    {
        setMainVolume = volume;
        AudioListener.volume = setMainVolume;

        UpdateSprite(AudioListener.volume, mainMuteButton.GetComponent<Image>());
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
        UpdateSprite(AudioListener.volume, mainMuteButton.GetComponent<Image>());
    }
    
    public void UpdateMainMenuMusicVolume(float volume)
    {
        setMainMenuMusicVolume = Mathf.Pow(volume, 2);
        mainMenuMusicVolume = setMainMenuMusicVolume;

        UpdateSprite(mainMenuMusicVolume, musicMuteButton.GetComponent<Image>());
    }

    public void MuteSFXVolume()
    {
        if (sfxVolumeMute)
        {
            sfxSource.volume = setSFXVolume;
            sfxVolumeMute = false;
        }
        else if (!sfxVolumeMute)
        {
            sfxSource.volume = 0;
            sfxVolumeMute = true;
        }
        
        UpdateSprite(sfxSource.volume, sfxMuteButton.GetComponent<Image>());
    }

    public void UpdateSFXVolume(float volume)
    {
        setSFXVolume = Mathf.Pow(volume, 2);
        sfxVolume = setSFXVolume;
        
        UpdateSprite(sfxSource.volume, sfxMuteButton.GetComponent<Image>());
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
        
        UpdateSprite(mainMenuMusicVolume, musicMuteButton.GetComponent<Image>());
    }

    public void OptionsPanel()
    {
        if (optionsPanel.activeSelf)
        {
            SoundManager.Instance.LockedWarningPopUpSound();
            optionsPanel.SetActive(false);
        }
        else if (!optionsPanel.activeSelf)
        {
            SoundManager.Instance.MenuButtonSound();
            optionsPanel.SetActive(true);
            UpdateSprite(AudioListener.volume, mainMuteButton.GetComponent<Image>());
            UpdateSprite(sfxSource.volume, sfxMuteButton.GetComponent<Image>());
            UpdateSprite(mainMenuMusicVolume, musicMuteButton.GetComponent<Image>());
        }
    }

    public void Fullscreen()
    {
        Screen.fullScreen = !Screen.fullScreen;

        if (Screen.fullScreen)
        {
            fullscreenButton.GetComponent<Image>().color = Color.black;
        }
        else if (!Screen.fullScreen)
        {
            fullscreenButton.GetComponent<Image>().color = Color.white;
        }
    }

    private void HotKeys()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (optionsPanel.activeSelf)
            {
                optionsPanel.SetActive(!optionsPanel.activeSelf);
            }
        }
    }
}
