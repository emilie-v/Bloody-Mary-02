using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChooseStaff : MonoBehaviour
{
    private Sprite[] staffList;
    private Sprite mirror;
    private Sprite pumpkin;
    private Sprite skeleton;
    private Sprite moon;
    private Sprite count;
    private Sprite hell;

    public GameObject chooseEnemy;
    public GameObject chooseStaff;

    public GameObject selectWarning;

    private bool isUnlocked;
    public LevelLoaderTransition levelLoaderTransition;
    public ChooseEnemy chooseEnemyClass;

    public GameObject padlockImage;

    public string[] staffInformationList;
    public string[] staffNameList;
    public Text currentStaffInformation;
    public Text currentStaffName;

    private int index = 0;
    private Image currentStaff;
    private string _scene;

    void Start()
    {
        currentStaff = GetComponent<Image>();

        mirror = Resources.Load<Sprite>("Sprites/Staffs/Staff_Mirror_Portrait");
        pumpkin = Resources.Load<Sprite>("Sprites/Staffs/Staff_Pumpkin_Portrait");
        skeleton = Resources.Load<Sprite>("Sprites/Staffs/Staff_Skeleton_Portrait");
        moon = Resources.Load<Sprite>("Sprites/Staffs/Staff_Moon_Portrait");
        count = Resources.Load<Sprite>("Sprites/Staffs/Staff_Darkest_Night_Portrait");
        hell = Resources.Load<Sprite>("Sprites/Staffs/Staff_Hell_Portrait");
        
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

        UpdateCurrentStaff();
    }

    private void Update()
    {
        HotKeys();
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

    private void UpdateCurrentStaff()
    {
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
    
    private void HotKeys()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (selectWarning.activeSelf)
            {
                CancelStaffWarningButton();
            }
            else if (chooseStaff.activeSelf)
            {
                BackToEnemyButton();
            }
        }
    }
}
