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
    public ChooseEnemy chooseEnemyClass;

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
        skeleton = Resources.Load<Sprite>("Sprites/Staffs/Staff_Skeleton_Portrait");
        moon = Resources.Load<Sprite>("Sprites/Staffs/Staff_Moon_Portrait");
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

        staffInformationList = new string[] { "Using its active ability mirrors the opponents last move on the x-axis, and flips it on the y-axis", 
            "Gain one extra mark move. Using its active ability freezes the opponents' staff ability for 1 turn, with a cooldown of 1 turn.", 
            "Using its active ability, a random owned marked square is marked with a flower which doubles the attack power of attack moves. Lasts for 1 turn, cooldown of 2 turns.", 
            "Using its active ability turns all owned marks into the opponents marks and vice versa. Also makes the user bark like a dog.", 
            "Half of the blood point drained from the opponents blood pool is added to the users' blood points. Using its active ability renders one random square on the board inaccessible for 2 turns and has a cooldown of 1 turn.", 
            "At the end of the users turn, the user regenerates 2 blood points if the user dealt damage this turn. Using its active ability marks the opponent for that turn with the brimstone mark. Attacking while under the brimstone mark deals damage to yourself as well as your opponent" };
        
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
            DataAcrossScenes.skeletonStaffUnlocked = true;
            DataAcrossScenes.moonStaffUnlocked = true;
            DataAcrossScenes.darkNightStaffUnlocked = true;
            DataAcrossScenes.hellStaffUnlocked = true;

            UpdateCurrentStaff();
            
            Debug.Log("All staffs unlocked");
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            DataAcrossScenes.pumpkinStaffUnlocked = false;
            DataAcrossScenes.skeletonStaffUnlocked = false;
            DataAcrossScenes.moonStaffUnlocked = false;
            DataAcrossScenes.darkNightStaffUnlocked = false;
            DataAcrossScenes.hellStaffUnlocked = false;
            
            UpdateCurrentStaff();
            
            Debug.Log("All staffs locked");
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

        if (index == 0 && DataAcrossScenes.mirrorStaffUnlocked)
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
        if (index == 1 && DataAcrossScenes.pumpkinStaffUnlocked)
        {
            padlockImage.SetActive(false);
            isUnlocked = true;
        }
        else if(index == 1 && DataAcrossScenes.pumpkinStaffUnlocked == false)
        {
            padlockImage.SetActive(true);
            isUnlocked = false;
        }
        
        if (index == 2 && DataAcrossScenes.skeletonStaffUnlocked)
        {
            padlockImage.SetActive(false);
            isUnlocked = true;
        }
        else if(index == 2 && DataAcrossScenes.skeletonStaffUnlocked == false)
        {
            padlockImage.SetActive(true);
            isUnlocked = false;
        }
        
        if (index == 3 && DataAcrossScenes.moonStaffUnlocked)
        {
            padlockImage.SetActive(false);
            isUnlocked = true;
        }
        else if(index == 3 && DataAcrossScenes.moonStaffUnlocked == false)
        {
            padlockImage.SetActive(true);
            isUnlocked = false;
        }
        
        //count ==2
        if (index == 4 && DataAcrossScenes.darkNightStaffUnlocked)
        {
            padlockImage.SetActive(false);
            isUnlocked = true;
        }
        else if (index == 4 && DataAcrossScenes.darkNightStaffUnlocked == false)
        {
            padlockImage.SetActive(true);
            isUnlocked = false;
        }

        //lucifer ==3
        if (index == 5 && DataAcrossScenes.hellStaffUnlocked)
        {
            padlockImage.SetActive(false);
            isUnlocked = true;
        }
        else if (index == 5 && DataAcrossScenes.hellStaffUnlocked == false)
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
        chooseEnemyClass.selectWarning.SetActive(false);
        SoundManager.Instance.LockedWarningPopUpSound();
        chooseStaff.SetActive(false);
        chooseEnemy.SetActive(true);
    }
}
