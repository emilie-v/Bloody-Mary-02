using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChooseStaff : MonoBehaviour
{
    Sprite[] staffList;
    Sprite mirror;
    Sprite hell;
    Sprite padlock;

    public GameObject chooseEnemy;
    public GameObject chooseStaff;

    public GameObject selectWarning;
    public GameObject cancelWarning;

    bool isUnlocked = false;
    BackgroundMusic backgroundMusic;

    public GameObject padlockImage;

    int index = 0;
    Image currentStaff;

    void Start()
    {
        currentStaff = GetComponent<Image>();

        mirror = Resources.Load<Sprite>("Sprites/Staffs/Staff_Mirror");
        hell = Resources.Load<Sprite>("Sprites/Staffs/Staff_Hell");
        padlock = Resources.Load<Sprite>("Sprites/GUI/GUI_padlock");
        
        staffList = new Sprite[2];
        staffList[0] = mirror;
        staffList[1] = hell;

        //Unlock the first staff
        PlayerPrefs.SetInt("Staff" + index, 1);


        // //Example, look staff 1 (hellstaff)
        // PlayerPrefs.SetInt("Staff" + 1, 0);

        // //Example, unlook staff 1 (hellstaff)
        // PlayerPrefs.SetInt("Staff" + 1, 1);

        UpdateCurrentStaff();
    }

#if UNITY_EDITOR
    void Update()
    {
        //Debug code for testing staff unlock

        if(Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Reset all save data! Please restart!");
            PlayerPrefs.DeleteAll();
        }
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("Debug Unlocked staff 1");
            PlayerPrefs.SetInt("Staff" + 1, 1);
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("Debug Unlocked staff 2");
            PlayerPrefs.SetInt("Staff" + 2, 1);
        }
    }
#endif

    public void RightStaffButton()
    {
        index++;

        if (index >= staffList.Length)
        {
            index = 0;
        }
        UpdateCurrentStaff();
    }

    public void LeftStaffButton()
    {
        index--;

        if (index < 0)
        {
            index = staffList.Length - 1;
        }

        UpdateCurrentStaff();
    }

    void UpdateCurrentStaff()
    {
        int unlocked = PlayerPrefs.GetInt("Staff" + index);

        if(unlocked == 1)
        {
            padlockImage.SetActive(false);
            isUnlocked = true;
        }
        else
        {
            padlockImage.SetActive(true);
            isUnlocked = false;
        }
    
        currentStaff.sprite = staffList[index];
    }

    public void CancelStaffWarningButton()
    {
        selectWarning.SetActive(false);
    }

    public void SelectStaffButton()
    {
        if (isUnlocked == true)
        {
            backgroundMusic.menuMusicPlaying = false;
            StaffManager.playerSelectedStaff = index;
            SceneManager.LoadScene("GameBoard");
        }
        else
        {
            selectWarning.SetActive(true);
        }
    }

    public void BackToEnemyButton()
    {
        chooseStaff.SetActive(false);
        chooseEnemy.SetActive(true);
    }
}
