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
    
    [SerializeField] private AIBehaviour aiBehaviour;
    
    public GameControl gameControl;
    private Spelplan spelplan;

    private void Update()
    {
        HotKeys();
    }

    public void CashOutPlayerButton()
    {
        if (gameControl.playerTurn == (int)Player_Turn.mary)
        {
            gameControl.CashOut();
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
        if (gameControl.playerTurn == (int)Player_Turn.mary && gameControl.playerMoves > 0)
        {
            gameControl.placeMode = true;
        }
    }
    
    public void CashOutEnemyButton()
    {
        if (gameControl.playerTurn == (int)Player_Turn.enemy && !aiBehaviour.AIMode)
        {
            gameControl.CashOut();
        }
    }

    public void EndEnemyTurnButton()
    {
        if (gameControl.playerTurn == (int)Player_Turn.enemy && !aiBehaviour.AIMode)
        {
            gameControl.EndTurn();
            Debug.Log(gameControl.playerTurn);
        }
    }
    
    public void PlaceMarkEnemyButton()
    {
        if (gameControl.playerTurn == (int)Player_Turn.enemy && gameControl.playerMoves > 0 && !aiBehaviour.AIMode)
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

    

    private void HotKeys()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (menuTab.activeSelf)
            {
                ContinueButton();
            }
            else if (!menuTab.activeSelf)
            {
                OpenMenuTab();
            }
        }
    }
}
