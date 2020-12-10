using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{

    public void BackToMainMenuButton()
    {
        SoundManager.Instance.LockedWarningPopUpSound();
        SceneManager.LoadScene("MainMenu");
    }


    void Update()
    {
       
    }

}
