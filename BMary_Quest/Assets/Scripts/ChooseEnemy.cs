﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChooseEnemy : MonoBehaviour
{
    private Sprite[] enemyList;
    private Sprite[] enemyNameList;
    private Sprite lucifer;
    private Sprite luciferName;
    private Sprite ghastella;
    private Sprite ghastellaName;
    private Sprite seniorBones;
    private Sprite seniorBonesName;
    private Sprite umbralina;
    private Sprite umbralinaName;
    private Sprite count;
    private Sprite countName;

    int index = DataAcrossScenes.ChosenEnemy;
    private Image currentEnemy;
    public Image currentEnemyName;
    public Text currentEnemyStaff;
    public Text currentEnemyStaffName;

    public string[] enemyStaffList;
    public string[] enemyStaffNameList;

    public GameObject selectWarning;
    public GameObject chooseEnemy;
    public GameObject chooseStaff;
    public GameObject padlockImage;
    private string _scene;

    void Start()
    {
        int index = DataAcrossScenes.ChosenEnemy;
        currentEnemy = GetComponent<Image>();
        
        lucifer = Resources.Load<Sprite>("Sprites/Characters/Enemies/Enemy_Lucifer_Portrait");
        ghastella = Resources.Load<Sprite>("Sprites/Characters/Enemies/Enemy_Ghastella_Portrait");
        seniorBones = Resources.Load<Sprite>("Sprites/Characters/Enemies/Enemy_SenorBones_Portrait");
        umbralina = Resources.Load<Sprite>("Sprites/Characters/Enemies/Enemy_Umbralina_Portrait");
        count = Resources.Load<Sprite>("Sprites/Characters/Enemies/Enemy_Count_Portrait");

        luciferName = Resources.Load<Sprite>("Sprites/GUI/GUI_ChooseOpponent/GUI_ChooseOpponent_LuciferName");
        ghastellaName = Resources.Load<Sprite>("Sprites/GUI/GUI_ChooseOpponent/GUI_ChooseOpponent_GhastellaName");
        seniorBonesName = Resources.Load<Sprite>("Sprites/GUI/GUI_ChooseOpponent/GUI_ChooseOpponent_SenorBonesName");
        umbralinaName = Resources.Load<Sprite>("Sprites/GUI/GUI_ChooseOpponent/GUI_ChooseOpponent_UmbralinaName");
        countName = Resources.Load<Sprite>("Sprites/GUI/GUI_ChooseOpponent/GUI_ChooseOpponent_CountName");

        enemyList = new Sprite[5];
        enemyNameList = new Sprite[5];
        enemyStaffList = new string[]{ "If the user deals damage, they regenerate 2 blood points at the end of the users turn. Using its active ability marks the opponent for that turn with the brimstone mark. Attacking while under the brimstone mark deals damage to yourself as well as your opponent",
            "Gain one extra mark move. Using its active ability freezes the opponents' staff ability for 1 turn, with a cooldown of 1 turn.", 
            "Using its active ability, a random owned marked square is marked with a flower - which doubles the attack power of attack moves. Lasts for 1 turn, cooldown of 2 turns.", 
            "Using its active ability turns all owned marks into the opponents marks and vice versa.", 
            "Half of the blood points drained from the opponents blood pool is added to the users' blood points. Using its active ability renders one random square inaccessible for 2 turns and has a cooldown of 1 turn."};
        enemyStaffNameList = new string[]{"Hell Staff", "Pumpkin Staff", "Skeleton Staff", "Moon Staff", "Dark Night Staff"};

        enemyList[0] = lucifer;
        enemyNameList[0] = luciferName;
        enemyList[1] = ghastella;
        enemyNameList[1] = ghastellaName;
        enemyList[2] = seniorBones;
        enemyNameList[2] = seniorBonesName;
        enemyList[3] = umbralina;
        enemyNameList[3] = umbralinaName;
        enemyList[4] = count;
        enemyNameList[4] = countName;

        currentEnemyStaff.text = enemyStaffList[index];
        currentEnemyStaffName.text = enemyStaffNameList[index];
        currentEnemy.sprite = enemyList[index];
        currentEnemyName.sprite = enemyNameList[index];
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

    public void RightArrowButton()
    {
        SoundManager.Instance.ArrowButtonSound();

        index++;
        if (index >= enemyList.Length)
        {
            index = 0;
        }

        UpdateEnemy();
    }

    public void LeftArrowButton()
    {
        SoundManager.Instance.ArrowButtonSound();

        index--;
        if (index < 0)
        {
            index = enemyList.Length - 1;
        }

        UpdateEnemy();
    }

    private void UpdateEnemy()
    {
        currentEnemyStaffName.text = enemyStaffNameList[index];
        currentEnemyStaff.text = enemyStaffList[index];
        currentEnemy.sprite = enemyList[index];
        currentEnemyName.sprite = enemyNameList[index];
    }

    public void SelectEnemyButton()
    {
        SoundManager.Instance.MenuButtonSound();
        if (!padlockImage.activeSelf)
        {
            if (index == 0)
            {
                chooseEnemy.SetActive(false);
                DataAcrossScenes.EnemyChosenStaff = (int)Chosen_Staff.hell;
                chooseStaff.SetActive(true);
            }

            if (index == (int)Chosen_Staff.pumpkin)
            {
                chooseEnemy.SetActive(false);
                DataAcrossScenes.EnemyChosenStaff = (int)Chosen_Staff.pumpkin;
                chooseStaff.SetActive(true);
            }
        
            if (index == (int)Chosen_Staff.skeleton)
            {
                chooseEnemy.SetActive(false);
                DataAcrossScenes.EnemyChosenStaff = (int)Chosen_Staff.skeleton;
                chooseStaff.SetActive(true);
            }
        
            if (index == (int)Chosen_Staff.moon)
            {
                chooseEnemy.SetActive(false);
                DataAcrossScenes.EnemyChosenStaff = (int)Chosen_Staff.moon;
                chooseStaff.SetActive(true);
            }

            if (index == (int)Chosen_Staff.night)
            {
                chooseEnemy.SetActive(false);
                DataAcrossScenes.EnemyChosenStaff = (int)Chosen_Staff.night;
                chooseStaff.SetActive(true);
            }
        }
        else
        {
            SoundManager.Instance.LockedWarningPopUpSound();
            selectWarning.SetActive(true);
        }
    }
    
    public void CancelWarningButton()
    {
        selectWarning.SetActive(false);
    }

    public void BackToMainMenuButton()
    {
        SoundManager.Instance.LockedWarningPopUpSound();
        SceneManager.LoadScene("MainMenu");
    }

    private void Update()
    {
        if(index == 0)
        {
            if (DataAcrossScenes.luciferUnlocked)
                padlockImage.SetActive(false);
            else
                padlockImage.SetActive(true);
        }
        else if(index == (int)Chosen_Staff.pumpkin)
        {
            if (DataAcrossScenes.ghastellaUnlocked)
                padlockImage.SetActive(false);
            else
                padlockImage.SetActive(true);
        }
        else if(index == (int)Chosen_Staff.skeleton)
        {
            if (DataAcrossScenes.seniorBonesUnlocked)
                padlockImage.SetActive(false);
            else
                padlockImage.SetActive(true);
        }
        else if(index == (int)Chosen_Staff.moon)
        {
            if (DataAcrossScenes.umbralinaUnlocked)
                padlockImage.SetActive(false);
            else
                padlockImage.SetActive(true);
        }
        else if(index == (int)Chosen_Staff.night)
        {
            if (DataAcrossScenes.countUnlocked)
                padlockImage.SetActive(false);
            else
                padlockImage.SetActive(true);
        }

        HotKeys();
    }

    private void HotKeys()
    {
        if (_scene == "ChooseEnemy")
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (selectWarning.activeSelf)
                {
                    selectWarning.SetActive(false);
                }
                else if (chooseEnemy.activeSelf)
                {
                    BackToMainMenuButton();
                }
            }
        }
    }
}