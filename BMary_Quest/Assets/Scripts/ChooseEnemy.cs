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

    int index = 0;
    Image currentEnemy;
    public GameObject selectWarning;
    public GameObject cancelWarning;

    void Start()
    {
        currentEnemy = GetComponent<Image>();

        lucifer = Resources.Load<Sprite>("Sprites/Enemy_Lucifer");
        ghastella = Resources.Load<Sprite>("Sprites/Enemy_Ghastella");

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
            SceneManager.LoadScene("GameBoard");
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
}
