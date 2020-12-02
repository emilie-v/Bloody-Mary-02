using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour
{
    public GameObject menuTab;
    public GameObject closeMenuTab;
    public GameObject restartTab;
    public GameObject exitGame;

    public GameControl gameControl;
    private Spelplan spelplan;
    public int cashOutStatus;

    //UI Text
    private Text playerBloodPointsText;
    private Text enemyBloodPointsText;
    
    //UI Fill Bars
    private Image playerBloodPointsFilling;
    private Image enemyBloodPointsFilling;

    private void Awake()
    {
        GetUIComponents();
    }

    private void Start()
    {
        cashOutStatus = gameControl.playerTurn;
    }

    private void Update()
    {
        NoMoreMoves();
    }

    private void GetUIComponents()
    {
        playerBloodPointsText = GameObject.Find("Player_BloodPoint_Text").GetComponent<Text>();
        enemyBloodPointsText = GameObject.Find("Enemy_BloodPoint_Text").GetComponent<Text>();

        playerBloodPointsFilling = GameObject.Find("Player/Player_BloodPointsBar").transform.GetChild(0).GetComponent<Image>();
        enemyBloodPointsFilling = GameObject.Find("Enemy/Enemy_BloodPointsBar").transform.GetChild(0).GetComponent<Image>();
    }

    public void CashOutPlayerButton()
    {
        if (gameControl.playerTurn == (int)Player_Turn.mary)
        {
            gameControl.CashOut();
            UpdateBloodPoints();
        }
    }

    public void EndPlayerTurnButton()
    {
        if (gameControl.playerTurn == (int)Player_Turn.mary)
        {
            gameControl.EndTurn();
            Debug.Log(gameControl.playerTurn);
        }
    }
    
    public void PlaceMarkPlayerButton()
    {
        if (gameControl.playerTurn == (int)Player_Turn.mary)
        {
            gameControl.placeMode = true;
        }
    }
    
    public void CashOutEnemyButton()
    {
        if (gameControl.playerTurn == (int)Player_Turn.enemy)
        {
            gameControl.CashOut();
            UpdateBloodPoints();
        }
    }

    public void EndEnemyTurnButton()
    {
        if (gameControl.playerTurn == (int)Player_Turn.enemy)
        {
            gameControl.EndTurn();
            Debug.Log(gameControl.playerTurn);
        }
    }
    
    public void PlaceMarkEnemyButton()
    {
        if (gameControl.playerTurn == (int)Player_Turn.enemy)
        {
            gameControl.placeMode = true;
        }
    }

    public void OpenMenuTab()
    {
        menuTab.SetActive(true);
    }

    public void ContinueButton()
    {
        menuTab.SetActive(false);
    }

    public void RestartButton()
    {

        Owner[] owners = FindObjectsOfType<Owner>();

        foreach (Owner tile in owners)
        {
            tile.resetBoard();
            tile.resetMary();
            tile.resetEnemy();
        }
    }

    public void ExitGameButton()
    {
        Application.Quit();
    }

    private void UpdateBloodPoints()
    {
        playerBloodPointsText.text = gameControl.marysHealth.ToString();
        enemyBloodPointsText.text = gameControl.enemyHealth.ToString();

        playerBloodPointsFilling.fillAmount = (float) gameControl.marysHealth / (float) gameControl.marysMaxHealth;
        enemyBloodPointsFilling.fillAmount = (float) gameControl.enemyHealth / (float) gameControl.enemyMaxHealth;
    }

    public void NoMoreMoves()
    {
        if (gameControl.playerMoves <= 0)
        {
            if (gameControl.playerTurn == (int)Player_Turn.mary)
            {
                GameObject.Find("Buttons/PlayerButtons/MarkButton").GetComponent<Image>().color = new Color(0.2f, 0.2f, 0.2f);
            }
            else if (gameControl.playerTurn == (int)Player_Turn.enemy)
            {
                GameObject.Find("Buttons/EnemyButtons/MarkButton").GetComponent<Image>().color = new Color(0.2f, 0.2f, 0.2f);
            }
        }
        else if (gameControl.playerMoves > 0)
        {
            GameObject.Find("Buttons/PlayerButtons/MarkButton").GetComponent<Image>().color = Color.white;
            GameObject.Find("Buttons/EnemyButtons/MarkButton").GetComponent<Image>().color = Color.white;
        }
    }
}
