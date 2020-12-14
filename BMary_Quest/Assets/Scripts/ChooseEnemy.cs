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

    Sprite padlock;

    int index = 0;
    Image currentEnemy;
    public Image currentEnemyName;
    public Text currentEnemyStaff;

    public string[] enemyStaffList = 
        { 
        "HellStaff", 
        "GhastellaStaff" 
        };

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

        luciferName = Resources.Load<Sprite>("Sprites/GUI/GUI_ChooseOpponent/GUI_ChooseOpponent_LuciferName");
        ghastellaName = Resources.Load<Sprite>("Sprites/GUI/GUI_ChooseOpponent/GUI_ChooseOpponent_CountName");

        padlock = Resources.Load<Sprite>("Sprites/GUI/GUI_padlock");

        enemyList = new Sprite[2];
        enemyNameList = new Sprite[2];

        enemyList[0] = lucifer;
        enemyNameList[0] = luciferName;
        enemyList[1] = ghastella;
        enemyNameList[1] = ghastellaName;
        
        currentEnemy.sprite = enemyList[index];
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
            DataAcrossScenes.EnemyChosenStaff = 1; //temp, sets the value to reflect that the chosen staff is the hellstaff
            chooseStaff.SetActive(true);
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
            padlockImage.SetActive(false);
        }
        else if(index == 1)
        {
            padlockImage.SetActive(true);
        }
    }
}
