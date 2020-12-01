using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameControl : MonoBehaviour
{
    public int marysTempPoints;
    private int Char;
    public int enemyTempPoints;
    public GameObject Spelplan;
    private Owner owner;
    private Boardpiece boardpiece;

    public int marysMaxHealth = 20;
    public int enemyMaxHealth = 20;
    public int marysHealth;
    public int enemyHealth;

    public int playerTurn;
    public int playerMoves;

    void Start()
    {
        Spelplan = GameObject.FindGameObjectWithTag("Spelplan");

        playerTurn = 0; //(int)Random.Range(0, 2)
        playerMoves = 1;
        
        marysHealth = marysMaxHealth;
        enemyHealth = enemyMaxHealth;
    }

    public void EndTurn()
    {
        if (playerTurn == (int)Player_Turn.mary) 
        {
            playerTurn = (int)Player_Turn.enemy;
            TurnChange();
        }
        else
        {
            playerTurn = (int)Player_Turn.mary;
            TurnChange();
        }
    }

    public void CashOut() 
    {
        if (playerTurn == (int)Player_Turn.mary)
        {
            enemyHealth -= marysTempPoints + 1;
            marysTempPoints = 0;

            //Reset placed pieces
            for (int i = 0; i < Spelplan.GetComponent<Spelplan>().gridArray.GetLength(0); i++)
            {
                for (int j = 0; j < Spelplan.GetComponent<Spelplan>().gridArray.GetLength(1); j++)
                {
                    if (Spelplan.GetComponent<Spelplan>().gridArray[i, j].GetComponent<Owner>().owned == (int)Tile_State.player1 && Spelplan.GetComponent<Spelplan>().gridArray[i, j].GetComponent<Owner>().specialState == 0)
                    {
                        Spelplan.GetComponent<Spelplan>().gridArray[i, j].GetComponent<Owner>().resetMary();
                    }
                }
            }
        }
        else if (playerTurn == (int)Player_Turn.enemy)
        {
            marysHealth -= enemyTempPoints + 1;
            enemyTempPoints = 0;
            for (int i = 0; i < Spelplan.GetComponent<Spelplan>().gridArray.GetLength(0); i++) //Null-pointer exeption?
            {
                for (int j = 0; j < Spelplan.GetComponent<Spelplan>().gridArray.GetLength(1); j++)
                {
                    if (Spelplan.GetComponent<Spelplan>().gridArray[i, j].GetComponent<Owner>().owned == (int)Tile_State.player2 && Spelplan.GetComponent<Spelplan>().gridArray[i, j].GetComponent<Owner>().specialState == 0)
                    {
                        Spelplan.GetComponent<Spelplan>().gridArray[i, j].GetComponent<Owner>().resetEnemy();
                    }
                }
            }
        }
        else
        {
            Debug.Log("Cashout error! This should not happen!");
        }
    }

    private void TurnChange()
    {
        playerMoves = 1;
        for (int i = 0; i < Spelplan.GetComponent<Spelplan>().gridArray.GetLength(0); i++)
        {
            for (int j = 0; j < Spelplan.GetComponent<Spelplan>().gridArray.GetLength(1); j++)
            {
                Spelplan.GetComponent<Spelplan>().gridArray[i, j].GetComponent<Owner>().canChange = false;
            }
        }
    }
}

public enum Player_Turn : int
{
    mary,
    enemy
}