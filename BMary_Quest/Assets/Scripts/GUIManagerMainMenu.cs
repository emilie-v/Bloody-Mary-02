﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIManagerMainMenu : MonoBehaviour
{
    [SerializeField] private GameObject optionsPanel;
    private Slider slider;

    private void Start()
    {
        slider = GameObject.Find("MusicVolume_Slider").GetComponent<Slider>();
        slider.onValueChanged.AddListener(SoundManager.Instance.UpdateMainMenuMusicVolume);
        slider.value = SoundManager.Instance.mainMenuMusic.volume;
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
