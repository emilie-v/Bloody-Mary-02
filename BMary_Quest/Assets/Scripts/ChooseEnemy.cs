using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChooseEnemy : MonoBehaviour
{
    Sprite[] enemyList;
    Sprite lucifer;
    Sprite ghastella;
    Sprite padlock;

    int index = 0;
    Image currentEnemy;
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
        padlock = Resources.Load<Sprite>("Sprites/GUI/GUI_padlock");

        enemyList = new Sprite[2];
        enemyList[0] = lucifer;
        enemyList[1] = ghastella;

        currentEnemy.sprite = enemyList[index];
    }

    public void RightArrowButton()
    {
        index++;
        //index = (index + enemyList.Length) % enemyList.Length;

        if (index >= enemyList.Length)
        {
            index = 0;
        }

        currentEnemy.sprite = enemyList[index];
    }

    public void LeftArrowButton()
    {
        index--;

        if (index < 0)
        {
            index = enemyList.Length - 1;
        }

        currentEnemy.sprite = enemyList[index];
    }

    public void SelectEnemyButton()
    {
        if (index == 0)
        {
            chooseEnemy.SetActive(false);
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
