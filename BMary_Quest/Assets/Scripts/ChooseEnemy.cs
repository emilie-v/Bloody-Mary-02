using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChooseEnemy : MonoBehaviour
{
    Sprite[] enemyList;
    Sprite[] enemyNameList;
    Sprite lucifer;
    Sprite luciferName;
    Sprite ghastella;
    Sprite ghastellaName;
    Sprite seniorBones;
    Sprite seniorBonesName;
    Sprite umbralina;
    Sprite umbralinaName;
    Sprite count;
    Sprite countName;

    Sprite padlock;

    int index = 1;
    Image currentEnemy;
    public Image currentEnemyName;
    public Text currentEnemyStaff;

    public string[] enemyStaffList;

    public GameObject selectWarning;
    public GameObject cancelWarning;
    public GameObject chooseEnemy;
    public GameObject chooseStaff;
    public GameObject padlockImage;

    void Start()
    {
        currentEnemy = GetComponent<Image>();


        lucifer = Resources.Load<Sprite>("Sprites/Characters/Enemies/Enemy_Lucifer_Portrait");
        ghastella = Resources.Load<Sprite>("Sprites/Characters/Enemies/Enemy_Ghastella_Portrait");
        seniorBones = Resources.Load<Sprite>("Sprites/Characters/Enemies/Enemy_Ghastella_Portrait");
        umbralina = Resources.Load<Sprite>("Sprites/Characters/Enemies/Enemy_Ghastella_Portrait");
        count = Resources.Load<Sprite>("Sprites/Characters/Enemies/Enemy_Count");

        luciferName = Resources.Load<Sprite>("Sprites/GUI/GUI_ChooseOpponent/GUI_ChooseOpponent_LuciferName");
        ghastellaName = Resources.Load<Sprite>("Sprites/GUI/GUI_ChooseOpponent/GUI_ChooseOpponent_GhastellaName");
        seniorBonesName = Resources.Load<Sprite>("Sprites/GUI/GUI_ChooseOpponent/GUI_ChooseOpponent_GhastellaName");
        umbralinaName = Resources.Load<Sprite>("Sprites/GUI/GUI_ChooseOpponent/GUI_ChooseOpponent_GhastellaName");
        countName = Resources.Load<Sprite>("Sprites/GUI/GUI_ChooseOpponent/GUI_ChooseOpponent_CountName");

        padlock = Resources.Load<Sprite>("Sprites/GUI/GUI_padlock");

        enemyList = new Sprite[5];
        enemyNameList = new Sprite[5];
        enemyStaffList = new string[]{ "HellStaff", "GhastellaStaff", "SkeletonStaff", "MoonStaff", "CountStaff"};
       
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
        currentEnemy.sprite = enemyList[index];
        currentEnemyName.sprite = enemyNameList[index];

        Debug.Log(enemyStaffList.Length);
    }

    public void RightArrowButton()
    {
        SoundManager.Instance.ArrowButtonSound();

        index++;
        //index = (index + enemyList.Length) % enemyList.Length;
        if (index >= enemyList.Length)
        {
            index = 0;
        }

        currentEnemyStaff.text = enemyStaffList[index];
        currentEnemy.sprite = enemyList[index];
        currentEnemyName.sprite = enemyNameList[index];
    }

    public void LeftArrowButton()
    {
        SoundManager.Instance.ArrowButtonSound();

        index--;
        if (index < 0)
        {
            index = enemyList.Length - 1;
        }

        currentEnemyStaff.text = enemyStaffList[index];
        currentEnemy.sprite = enemyList[index];
        currentEnemyName.sprite = enemyNameList[index];
    }

    public void SelectEnemyButton()
    {
        SoundManager.Instance.MenuButtonSound();

        if (index == 0)
        {
            Debug.Log("Lucifer is chosen");
            chooseEnemy.SetActive(false);
            DataAcrossScenes.EnemyChosenStaff = (int)Chosen_Staff.hell; //temp, sets the value to reflect that the chosen staff is the hellstaff
            chooseStaff.SetActive(true);
        }

        if (index == (int)Chosen_Staff.pumpkin)
        {
            Debug.Log("Ghastella is chosen");
            chooseEnemy.SetActive(false);
            DataAcrossScenes.EnemyChosenStaff = (int)Chosen_Staff.pumpkin; //temp, sets the value to reflect that the chosen staff is the pumpkinstaff
            chooseStaff.SetActive(true);
        }
        
        if (index == (int)Chosen_Staff.skeleton)
        {
            Debug.Log("Ghastella is chosen");
            chooseEnemy.SetActive(false);
            DataAcrossScenes.EnemyChosenStaff = (int)Chosen_Staff.skeleton; //temp, sets the value to reflect that the chosen staff is the pumpkinstaff
            chooseStaff.SetActive(true);
        }
        
        if (index == (int)Chosen_Staff.moon)
        {
            Debug.Log("Ghastella is chosen");
            chooseEnemy.SetActive(false);
            DataAcrossScenes.EnemyChosenStaff = (int)Chosen_Staff.moon; //temp, sets the value to reflect that the chosen staff is the pumpkinstaff
            chooseStaff.SetActive(true);
        }

        if (index == (int)Chosen_Staff.night)
        {
            Debug.Log("The Count is chosen");
            chooseEnemy.SetActive(false);
            DataAcrossScenes.EnemyChosenStaff = (int)Chosen_Staff.night; //temp, sets the value to reflect that the chosen staff is the darknightstaff
            chooseStaff.SetActive(true);
        }

        else
        {
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

    }
}
