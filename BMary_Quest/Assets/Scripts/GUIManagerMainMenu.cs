using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIManagerMainMenu : MonoBehaviour
{
    [SerializeField] private GameObject optionsPanel;
    private Slider musicSlider;
    private Button musicMuteButton;

    private void Start()
    {
        musicSlider = GameObject.Find("MusicVolume_Slider").GetComponent<Slider>();
        musicSlider.onValueChanged.AddListener(SoundManager.Instance.UpdateMainMenuMusicVolume);
        musicSlider.value = SoundManager.Instance.mainMenuMusic.volume;

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
