using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    public GameObject tutorial;
    public GameObject gameBoardTutorial;
    public GameObject bloodPointsTutorial;
    public GameObject marksTutorial;
    public GameObject attackMovesTutorial;
    public GameObject staffsTutorial;
    public GameObject endTurnTutorial;
    private string _scene;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && _scene == "GameBoard")
        {
            SetAllToFalse();
            tutorial.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && _scene == "Tutorial")
        {
            BackToMainMenuButton();
        }
    }
    
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        _scene = scene.name;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void BackToMainMenuButton()
    {
        SoundManager.Instance.LockedWarningPopUpSound();
        SetAllToFalse();
        SceneManager.LoadScene("MainMenu");
    }

    public void BackToGameButton()
    {
        SoundManager.Instance.MenuButtonSound();
        SetAllToFalse();
        tutorial.SetActive(false);
    }

    public void OpenTutorialButton()
    {
        SoundManager.Instance.MenuButtonSound();
        SetAllToFalse();
        tutorial.SetActive(true);
    }

    public void GameBoardTutorialButton()
    {
        SoundManager.Instance.MenuButtonSound();
        SetAllToFalse();
        gameBoardTutorial.SetActive(true);
    }

    public void BloodPointsTutorialButton()
    {
        SoundManager.Instance.MenuButtonSound();
        SetAllToFalse();
        bloodPointsTutorial.SetActive(true);
    }

    public void MarksTutorialButton()
    {
        SoundManager.Instance.MenuButtonSound();
        SetAllToFalse();
        marksTutorial.SetActive(true);
    }

    public void AttackMovesTutorialButton()
    {
        SoundManager.Instance.MenuButtonSound();
        SetAllToFalse();
        attackMovesTutorial.SetActive(true);
    }

    public void StaffsTutorialButton()
    {
        SoundManager.Instance.MenuButtonSound();
        SetAllToFalse();
        staffsTutorial.SetActive(true);
    }    

    public void EndTurnTutorialButton()
    {
        SoundManager.Instance.MenuButtonSound();
        SetAllToFalse();
        endTurnTutorial.SetActive(true);
    }

    private void SetAllToFalse()
    {
        gameBoardTutorial.SetActive(false);
        bloodPointsTutorial.SetActive(false);
        marksTutorial.SetActive(false);
        attackMovesTutorial.SetActive(false);
        staffsTutorial.SetActive(false);
        endTurnTutorial.SetActive(false);
    }
}
