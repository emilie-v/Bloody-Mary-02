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

    private void GetUIComponents()
    {
        playerBloodPointsText = GameObject.Find("Player_BloodPoint_Text").GetComponent<Text>();
        enemyBloodPointsText = GameObject.Find("Enemy_BloodPoint_Text").GetComponent<Text>();

        playerBloodPointsFilling = GameObject.Find("Player_BloodPointsBar").transform.GetChild(0).GetComponent<Image>();
        enemyBloodPointsFilling = GameObject.Find("Enemy_BloodPointsBar").transform.GetChild(0).GetComponent<Image>();
    }

    public void CashOutButton()
    {
        gameControl.CashOut(gameControl.playerTurn);
        UpdateBloodPoints();
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

    public void UpdateBloodPoints()
    {
        playerBloodPointsText.text = gameControl.marysHealth.ToString();
        enemyBloodPointsText.text = gameControl.enemyHealth.ToString();

        playerBloodPointsFilling.fillAmount = (float) gameControl.marysHealth / (float) gameControl.marysMaxHealth;
        enemyBloodPointsFilling.fillAmount = (float) gameControl.enemyHealth / (float) gameControl.enemyMaxHealth;
    }
}
