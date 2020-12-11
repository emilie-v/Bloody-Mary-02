using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    public GameObject gameBoardTutorial;
    public GameObject bloodPointsTutorial;
    public GameObject marksTutorial;
    public GameObject attackMovesTutorial;
    public GameObject staffsTutorial;
    public GameObject endTurnTutorial;
    public GameObject backTutorial;

    public void BackToMainMenuButton()
    {
        SetAllToFalse();
        SoundManager.Instance.LockedWarningPopUpSound();
        SceneManager.LoadScene("MainMenu");
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
