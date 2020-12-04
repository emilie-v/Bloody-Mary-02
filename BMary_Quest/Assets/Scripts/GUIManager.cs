using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GUIManager : MonoBehaviour
{
    public GameObject menuTab;
    public GameObject closeMenuTab;
    public GameObject restartTab;
    public GameObject exitGame;
    
    
    public GameControl gameControl;
    private Spelplan spelplan;
    public int cashOutStatus;

    private void Awake()
    {
        GetUIComponents();
    }

    private void Start()
    {
        cashOutStatus = gameControl.playerTurn;
    }

    private void GetUIComponents()
    {
        
    }

    public void CashOutPlayerButton()
    {
        gameControl.CashOut();
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
        gameControl.CashOut();
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

    public void BackToMainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ExitGameButton()
    {
        Application.Quit();
    }
}
