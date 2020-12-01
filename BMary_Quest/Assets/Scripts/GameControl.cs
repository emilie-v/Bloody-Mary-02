using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;
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

    public bool placeMode;
    public int playerTurn;
    public int playerMoves;

    void Start()
    {
        Spelplan = GameObject.FindGameObjectWithTag("Spelplan");

        playerTurn = 0; //(int)Random.Range(0, 2)
        TurnStart();
        
        marysHealth = marysMaxHealth;
        enemyHealth = enemyMaxHealth;
    }

    public void EndTurn()
    {
        if (playerTurn == (int)Player_Turn.mary) 
        {
            playerTurn = (int)Player_Turn.enemy;
            TurnStart();
        }
        else
        {
            playerTurn = (int)Player_Turn.mary;
            TurnStart();
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

    private void TurnStart()
    {
        playerMoves = 1;
        for (int i = 0; i < Spelplan.GetComponent<Spelplan>().gridArray.GetLength(0); i++)
        {
            for (int j = 0; j < Spelplan.GetComponent<Spelplan>().gridArray.GetLength(1); j++)
            {
                Spelplan.GetComponent<Spelplan>().gridArray[i, j].GetComponent<Owner>().canChange = false;
            }
        }

        if (playerTurn == (int)Player_Turn.mary)
        {
            //Set enemy to smaller size
            GameObject.Find("IngameGUI_Canvas/MaskEnemy/Enemy").GetComponent<RectTransform>().localScale = new Vector3(0.7f, 0.7f, 1);
            
            //Set mary back to right size
            GameObject.Find("IngameGUI_Canvas/MaskPlayer/Player").GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        } 
        else if (playerTurn == (int)Player_Turn.enemy)
        {
            //Set enemy to smaller size
            GameObject.Find("IngameGUI_Canvas/MaskPlayer/Player").GetComponent<RectTransform>().localScale = new Vector3(0.7f, 0.7f, 1);
            
            //Set enemy back to right size
            GameObject.Find("IngameGUI_Canvas/MaskEnemy/Enemy").GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        }
    }
}

public enum Player_Turn : int
{
    mary,
    enemy
}