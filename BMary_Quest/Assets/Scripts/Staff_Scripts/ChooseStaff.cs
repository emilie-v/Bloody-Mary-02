using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChooseStaff : MonoBehaviour
{
    Sprite[] staffList;
    Sprite mirror;
    Sprite pumpkin;
    Sprite skeleton;
    Sprite moon;
    Sprite count;
    Sprite hell;
    Sprite padlock;

    public GameObject chooseEnemy;
    public GameObject chooseStaff;

    public GameObject selectWarning;
    public GameObject cancelWarning;

    bool isUnlocked = false;
    BackgroundMusic backgroundMusic;
    public SoundManager soundManager;
    public LevelLoaderTransition levelLoaderTransition;

    public GameObject padlockImage;

    public string[] staffInformationList;
    public string[] staffNameList;
    public Text currentStaffInformation;
    public Text currentStaffName;

    int index = 0;
    Image currentStaff;

    void Start()
    {
        currentStaff = GetComponent<Image>();

        backgroundMusic = GameObject.Find("AudioSource").GetComponent<BackgroundMusic>();
        mirror = Resources.Load<Sprite>("Sprites/Staffs/Staff_Mirror_Portrait");
        pumpkin = Resources.Load<Sprite>("Sprites/Staffs/Staff_Pumpkin_Portrait");
        skeleton = Resources.Load<Sprite>("Sprites/Staffs/Staff_Pumpkin");
        moon = Resources.Load<Sprite>("Sprites/Staffs/Staff_Pumpkin");
        count = Resources.Load<Sprite>("Sprites/Staffs/Staff_Darkest_Night_Portrait");
        hell = Resources.Load<Sprite>("Sprites/Staffs/Staff_Hell_Portrait");
        padlock = Resources.Load<Sprite>("Sprites/GUI/GUI_padlock");
        
        staffList = new Sprite[6];
        staffList[0] = mirror;
        staffList[1] = pumpkin;
        staffList[2] = skeleton;
        staffList[3] = moon;
        staffList[4] = count;
        staffList[5] = hell;

        staffInformationList = new string[] { "Mirror staff information", "Pumpkin staff information", "Skeleton staff information", "Moon staff information", "Dark Night staff information", "Hell staff information" };
        staffNameList = new string[] { "Mirror Staff", "Pumpkin Staff", "Skeleton Staff", "Moon Staff", "Dark Night Staff", "Hell Staff" };
        //Unlock the first staff
        PlayerPrefs.SetInt("Staff" + index, 1);

        currentStaffName.text = staffNameList[index];
        currentStaffInformation.text = staffInformationList[index];

        // //Example, look staff 1 (hellstaff)
        // PlayerPrefs.SetInt("Staff" + 1, 0);

        // //Example, unlook staff 1 (hellstaff)
        // PlayerPrefs.SetInt("Staff" + 1, 1);

        UpdateCurrentStaff();
    }

#if UNITY_EDITOR
    void Update()
    {
        /*Debug code for testing staff unlock

        if(Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Reset all save data! Please restart!");
            PlayerPrefs.DeleteAll();
            PlayerPrefs.SetInt("Staff" + 0, 1);
            UpdateCurrentStaff();
        }
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("Debug Unlocked staff 1");
            PlayerPrefs.SetInt("Staff" + 1, 1);
            UpdateCurrentStaff();
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("Debug Unlocked staff 2");
            PlayerPrefs.SetInt("Staff" + 2, 1);
            UpdateCurrentStaff();
        }
        */
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            DataAcrossScenes.pumpkinStaffUnlocked = true;
            DataAcrossScenes.darkNightStaffUnlocked = true;
            DataAcrossScenes.hellStaffUnlocked = true;

            UpdateCurrentStaff();
            
            Debug.Log("Three staffs unlocked");
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            DataAcrossScenes.pumpkinStaffUnlocked = false;
            DataAcrossScenes.darkNightStaffUnlocked = false;
            DataAcrossScenes.hellStaffUnlocked = false;
            
            UpdateCurrentStaff();
            
            Debug.Log("Three staffs locked");
        }
    }
#endif

    public void RightStaffButton()
    {
        SoundManager.Instance.ArrowButtonSound();

        index++;
        if (index >= staffList.Length)
        {
            index = 0;
        }
        currentStaffName.text = staffNameList[index];
        currentStaffInformation.text = staffInformationList[index];
        UpdateCurrentStaff();
    }

    public void LeftStaffButton()
    {
        SoundManager.Instance.ArrowButtonSound();

        index--;
        if (index < 0)
        {
            index = staffList.Length - 1;
        }
        currentStaffName.text = staffNameList[index];
        currentStaffInformation.text = staffInformationList[index];
        UpdateCurrentStaff();
    }

    void UpdateCurrentStaff()
    {
        //int unlocked = PlayerPrefs.GetInt("Staff" + index);

        if (index == 0 && DataAcrossScenes.mirrorStaffUnlocked == true)
        {
            padlockImage.SetActive(false);
            isUnlocked = true;
        }
        else if (index == 0 && DataAcrossScenes.mirrorStaffUnlocked == false)
        {
            padlockImage.SetActive(true);
            isUnlocked = false;
        }

        //pumpkin staff == 1
        if (index == 1 && DataAcrossScenes.pumpkinStaffUnlocked == true)
        {
            padlockImage.SetActive(false);
            isUnlocked = true;
        }
        else if(index == 1 && DataAcrossScenes.pumpkinStaffUnlocked == false)
        {
            padlockImage.SetActive(true);
            isUnlocked = false;
        }
        
        //count ==2
        if (index == 2 && DataAcrossScenes.darkNightStaffUnlocked == true)
        {
            padlockImage.SetActive(false);
            isUnlocked = true;
        }
        else if (index == 2 && DataAcrossScenes.darkNightStaffUnlocked == false)
        {
            padlockImage.SetActive(true);
            isUnlocked = false;
        }

        //lucifer ==3
        if (index == 3 && DataAcrossScenes.hellStaffUnlocked == true)
        {
            padlockImage.SetActive(false);
            isUnlocked = true;
        }
        else if (index == 3 && DataAcrossScenes.hellStaffUnlocked == false)
        {
            padlockImage.SetActive(true);
            isUnlocked = false;
        }



        currentStaff.sprite = staffList[index];
    }

    public void CancelStaffWarningButton()
    {
        SoundManager.Instance.MenuButtonSound();
        selectWarning.SetActive(false);
    }

    public void SelectStaffButton()
    {
        if (isUnlocked)
        {
            SoundManager.Instance.SelectButtonSound();
            StaffManager.playerSelectedStaff = index;
            levelLoaderTransition.LoadGameScene();
        }
        else
        {
            SoundManager.Instance.LockedWarningPopUpSound();
            selectWarning.SetActive(true);
        }
    }

    public void BackToEnemyButton()
    {
        SoundManager.Instance.LockedWarningPopUpSound();
        chooseStaff.SetActive(false);
        chooseEnemy.SetActive(true);
    }
}
