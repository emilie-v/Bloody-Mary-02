﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{//TODO döp om denna class till end turn/cashout
    public int marysTempPoints;
    private int Char;
    public int enemyTempPoints;
    public GameObject Spelplan;

    public int marysHealth = 20; //kanske ska ha standardvärde + eventuell staff-modifier? Eller kommer hälsan med staven så att säga?
    public int enemyHealth = 20;

    public int playerTurn;

    void Start()
    {
        Spelplan = GameObject.FindGameObjectWithTag("Spelplan");
    }

    void Update()
    {
        //Avsluta din runda. Gör dock inget nu. 0 = Mary 1 = enemy
        if (Input.GetKeyDown(KeyCode.E)) //end-turn utan effekt.
        {
            if (playerTurn == 0)
            {
                playerTurn = 1;
            }
            else
            {
                playerTurn = 0;
            }
        }
        //Cash-Out
        if (Input.GetKeyDown(KeyCode.G)) 
        {
            CashOut(playerTurn);
        }
    }

    //Cash-Out funktion
    void CashOut(int playerTurn) 
    {
        //Marys runda om man trycker cash out  = då tar enemy damage 
        if (playerTurn == 0)
        {
            enemyHealth -= marysTempPoints;
            marysTempPoints = 0;

            //resettar de brickor som man cashat ut
            for (int i = 0; i < Spelplan.GetComponent<Spelplan>().gridArray.GetLength(0); i++) //Null-pointer exeption?
            {
                for (int j = 0; j < Spelplan.GetComponent<Spelplan>().gridArray.GetLength(1); j++)
                {
                    if (Spelplan.GetComponent<Spelplan>().gridArray[i, j].GetComponent<Owner>().OwnedByMary == true)
                    {
                        Spelplan.GetComponent<Spelplan>().gridArray[i, j].GetComponent<Owner>().resetMary();
                    }
                }
            }
        }

        //Enemys rund om man trycker cash out = då tar enemy damage.
        else if (playerTurn == 1)
        {
            marysHealth -= enemyTempPoints;
            enemyTempPoints = 0;
            for (int i = 0; i < Spelplan.GetComponent<Spelplan>().gridArray.GetLength(0); i++) //Null-pointer exeption?
            {
                for (int j = 0; j < Spelplan.GetComponent<Spelplan>().gridArray.GetLength(1); j++)
                {
                    if (Spelplan.GetComponent<Spelplan>().gridArray[i, j].GetComponent<Owner>().OwnedByEnemy == true)
                    {
                        Spelplan.GetComponent<Spelplan>().gridArray[i, j].GetComponent<Owner>().resetEnemy();
                    }
                }
            }
        }
        else
        {
            Debug.Log("This should not happen!");
        }
    }
}
