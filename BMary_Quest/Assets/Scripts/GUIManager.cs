using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIManager : MonoBehaviour
{
    public GameObject menuTab;
    public GameObject closeMenuTab;
    public GameObject restartTab;
    public GameObject exitGame;

    public Owner owner;

    public GameControl gameControl;
    public int cashOutStatus;

    private void Start()
    {
        cashOutStatus = gameControl.playerTurn;
    }

    public void CashOutButton()
    {
        gameControl.CashOut(cashOutStatus);
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
      //  owner.resetBoard();
       // owner.resetMary();
       // owner.resetEnemy();
    }

    public void ExitGameButton()
    {
        Application.Quit();
    }
}
