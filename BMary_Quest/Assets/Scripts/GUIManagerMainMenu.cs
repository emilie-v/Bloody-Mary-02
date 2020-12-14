using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIManagerMainMenu : MonoBehaviour
{
    [SerializeField] private GameObject optionsPanel;
    private Slider volumeSlider;
    private Slider musicSlider;
    private Button mainMuteButton;
    private Button musicMuteButton;

    private void Start()
    {
        volumeSlider = GameObject.Find("MainVolume_Slider").GetComponent<Slider>();
        volumeSlider.onValueChanged.AddListener(SoundManager.Instance.UpdateMainVolume);
        volumeSlider.value = AudioListener.volume;
        
        musicSlider = GameObject.Find("MusicVolume_Slider").GetComponent<Slider>();
        musicSlider.onValueChanged.AddListener(SoundManager.Instance.UpdateMainMenuMusicVolume);
        musicSlider.value = SoundManager.Instance.mainMenuMusic.volume;

        mainMuteButton = GameObject.Find("MainVolume_MuteButton").GetComponent<Button>();
        mainMuteButton.onClick.AddListener(SoundManager.Instance.MuteMainVolume);
        
        musicMuteButton = GameObject.Find("MusicVolume_MuteButton").GetComponent<Button>();
        musicMuteButton.onClick.AddListener(SoundManager.Instance.MuteMainMenuMusicVolume);

        optionsPanel.SetActive(false);
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
        }
    }
}
