using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIManager : MonoBehaviour
{
    public GameObject menuTab;
    public GameObject closeMenuTab;
    public GameObject restartTab;
    public GameObject exitGame;

    public GameControl gameControl;
    public int cashOutStatus;

    private void Start()
    {
        cashOutStatus = gameControl.playerTurn;
    }

    public void CashOutButton()
    {
        gameControl.CashOut(gameControl.playerTurn);
    }

    public void endTurnButton()
    {
        gameControl.EndTurn();
        Debug.Log(gameControl.playerTurn);
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
}
