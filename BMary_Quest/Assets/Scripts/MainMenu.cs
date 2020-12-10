using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void PlayGame()
    {
        SoundManager.Instance.MenuButtonSound();
        SceneManager.LoadScene("ChooseEnemy");
    }
    public void Tutorial()
    {
        SoundManager.Instance.MenuButtonSound();
        SceneManager.LoadScene("Tutorial");
    }
    public void Credits()
    {
        SoundManager.Instance.MenuButtonSound();
        SceneManager.LoadScene("Credits");
    }

    public void QuitGame()
    {
        SoundManager.Instance.LockedWarningPopUpSound();
        Application.Quit();
    }
}
