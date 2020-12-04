using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
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
    private GUIManager guiManager;
    
    [SerializeField]
    private GameObject gameOver;

    public int marysMaxHealth = 20;
    public int enemyMaxHealth = 20;
    public int marysHealth;
    public int enemyHealth;

    public bool placeMode;
    public int playerTurn;
    public int playerMoves;
    private int playerMovesPerTurn;
    private int enemyMovesPerTurn;
    private bool canCashOut;
    
    //UI Text
    [SerializeField] private Text playerBloodPointsText;
    [SerializeField] private Text enemyBloodPointsText;
    
    //UI Fill Bars
    [SerializeField] private Image playerBloodPointsFilling;
    [SerializeField] private Image enemyBloodPointsFilling;
    
    //Mark Buttons
    [SerializeField] private Image playerMarkButton;
    [SerializeField] private Image enemyMarkButton;
    
    //CashOut Buttons
    [SerializeField] private Image playerCashOutButton;
    [SerializeField] private Image enemyCashOutButton;


    void Start()
    {
        Spelplan = GameObject.FindGameObjectWithTag("Spelplan");

        playerTurn = 0; //(int)Random.Range(0, 2)
        playerMovesPerTurn = 2;
        enemyMovesPerTurn = 2;
        TurnStart(2);
        
        marysHealth = marysMaxHealth;
        enemyHealth = enemyMaxHealth;
    }

    private void Update()
    {
        HotKeys();
    }

    public void EndTurn()
    {
        if (playerTurn == (int)Player_Turn.mary) 
        {
            playerTurn = (int)Player_Turn.enemy;
            TurnStart(enemyMovesPerTurn);
        }
        else
        {
            playerTurn = (int)Player_Turn.mary;
            TurnStart(playerMovesPerTurn);
        }
    }
    
    private void TurnStart(int MovesPerTurn)
    {
        playerMoves = MovesPerTurn;
        canCashOut = true;
        ResetCanChange();
        CharacterScaling();
        ButtonFade();
        NoMoreMoves();
        checkCanCashOut();
    }

    public void CashOut() 
    {
        if (canCashOut)
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

                GameOver();
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
            
                GameOver();
            }
            else
            {
                Debug.Log("Cashout error! This should not happen!");
            }
            UpdateBloodPoints();
            canCashOut = false;
            checkCanCashOut();
        }
    }
    
    public void GameOver()
    {
        //TODO Make it change dynamically depending on who is vs who
        if (marysHealth <= 0 || enemyHealth <= 0)
        {
            gameOver.SetActive(true);
            
            if (marysHealth <= 0)
            {
                GameObject.Find("IngameGUI_Canvas/GameOver/Text").GetComponent<Text>().text = "Lucifer Wins";
            } 
            else if (enemyHealth <= 0)
            {
                GameObject.Find("IngameGUI_Canvas/GameOver/Text").GetComponent<Text>().text = "Mary Wins";
            }
        }
    }
    
    public void UpdateBloodPoints()
    {
        playerBloodPointsText.text = marysHealth.ToString();
        enemyBloodPointsText.text = enemyHealth.ToString();

        playerBloodPointsFilling.fillAmount = (float) marysHealth / (float) marysMaxHealth;
        enemyBloodPointsFilling.fillAmount = (float) enemyHealth / (float) enemyMaxHealth;
    }

    private void ResetCanChange()
    {
        for (int i = 0; i < Spelplan.GetComponent<Spelplan>().gridArray.GetLength(0); i++)
        {
            for (int j = 0; j < Spelplan.GetComponent<Spelplan>().gridArray.GetLength(1); j++)
            {
                Spelplan.GetComponent<Spelplan>().gridArray[i, j].GetComponent<Owner>().canChange = false;
            }
        }
    }

    private void CharacterScaling()
    {
        if (playerTurn == (int)Player_Turn.mary)
        {
            //Set enemy to smaller size
            GameObject.Find("IngameGUI_Canvas/Enemy/MaskEnemy/Enemy").GetComponent<RectTransform>().localScale = new Vector3(0.7f, 0.7f, 1);
            
            //Set mary back to right size
            GameObject.Find("IngameGUI_Canvas/Player/MaskPlayer/Player").GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        } 
        else if (playerTurn == (int)Player_Turn.enemy)
        {
            //Set enemy to smaller size
            GameObject.Find("IngameGUI_Canvas/Player/MaskPlayer/Player").GetComponent<RectTransform>().localScale = new Vector3(0.7f, 0.7f, 1);
            
            //Set enemy back to right size
            GameObject.Find("IngameGUI_Canvas/Enemy/MaskEnemy/Enemy").GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        }
    }

    private void ButtonFade()
    {
        //Player Color Block
        ColorBlock colorBlockPlayerButtons = GameObject.Find("IngameGUI_Canvas/Buttons/PlayerButtons/EndTurnButton").GetComponent<Button>().colors;
        
        //Enemy Color Block
        ColorBlock colorBlockEnemyButtons = GameObject.Find("IngameGUI_Canvas/Buttons/EnemyButtons/EndTurnButton").GetComponent<Button>().colors;
        
        if (playerTurn == (int)Player_Turn.mary)
        {
            colorBlockPlayerButtons.highlightedColor = new Color(1f, 1f, 1f, 1f);
            colorBlockPlayerButtons.pressedColor = new Color(1f, 1f, 1f, 1f);
            
            colorBlockEnemyButtons.highlightedColor = new Color(1f, 1f, 1f, 0.2f);
            colorBlockEnemyButtons.pressedColor = new Color(0.6603774f, 0.4074404f, 0.4074404f, 1f);
        }
        else if (playerTurn == (int)Player_Turn.enemy)
        {
            colorBlockPlayerButtons.highlightedColor = new Color(1f, 1f, 1f, 0.2f);
            colorBlockPlayerButtons.pressedColor = new Color(0.6603774f, 0.4074404f, 0.4074404f, 1f);
            
            colorBlockEnemyButtons.highlightedColor = new Color(1f, 1f, 1f, 1f);
            colorBlockEnemyButtons.pressedColor = new Color(1f, 1f, 1f, 1f);
        }
        
        //Sets the colors of the Player buttons
        GameObject.Find("IngameGUI_Canvas/Buttons/PlayerButtons/EndTurnButton").GetComponent<Button>().colors = colorBlockEnemyButtons;
        GameObject.Find("IngameGUI_Canvas/Buttons/PlayerButtons/OutCashButton").GetComponent<Button>().colors = colorBlockEnemyButtons;
        GameObject.Find("IngameGUI_Canvas/Buttons/PlayerButtons/StaffButton").GetComponent<Button>().colors = colorBlockEnemyButtons;
        GameObject.Find("IngameGUI_Canvas/Buttons/PlayerButtons/MarkButton").GetComponent<Button>().colors = colorBlockEnemyButtons;
        
        //Sets the colors of the Enemy buttons
        GameObject.Find("IngameGUI_Canvas/Buttons/EnemyButtons/EndTurnButton").GetComponent<Button>().colors = colorBlockPlayerButtons;
        GameObject.Find("IngameGUI_Canvas/Buttons/EnemyButtons/OutCashButton").GetComponent<Button>().colors = colorBlockPlayerButtons;
        GameObject.Find("IngameGUI_Canvas/Buttons/EnemyButtons/StaffButton").GetComponent<Button>().colors = colorBlockPlayerButtons;
        GameObject.Find("IngameGUI_Canvas/Buttons/EnemyButtons/MarkButton").GetComponent<Button>().colors = colorBlockPlayerButtons;
    }
    
    public void NoMoreMoves()
    {
        if (playerMoves <= 0)
        {
            if (playerTurn == (int)Player_Turn.mary)
            {
                playerMarkButton.color = new Color(0.2f, 0.2f, 0.2f);
            }
            else if (playerTurn == (int)Player_Turn.enemy)
            {
                enemyMarkButton.color = new Color(0.2f, 0.2f, 0.2f);
            }
        }
        else if (playerMoves > 0)
        {
            playerMarkButton.color = Color.white;
            enemyMarkButton.color = new Color(1f, 0.1921569f, 0.1921569f);
        }
    }
    
    public void checkCanCashOut()
    {
        if (canCashOut)
        {
            playerCashOutButton.color = playerMarkButton.color = Color.white;
            enemyCashOutButton.color = playerMarkButton.color = new Color(1f, 0.1921569f, 0.1921569f);
        }
        else if (canCashOut == false)
        {
            if (playerTurn == (int)Player_Turn.mary)
            {
                playerCashOutButton.color = new Color(0.2f, 0.2f, 0.2f);
            }
            else if (playerTurn == (int)Player_Turn.enemy)
            {
                enemyCashOutButton.color = new Color(0.2f, 0.2f, 0.2f);
            }
        }
    }

    private void HotKeys()
    {
        if (playerTurn == (int)Player_Turn.mary)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (placeMode == false)
                {
                    placeMode = true;
                }
                else if (placeMode)
                {
                    placeMode = false;
                }
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                //Staffs
            }
        
            if (Input.GetKeyDown(KeyCode.E))
            {
                CashOut();
            }
        
            if (Input.GetKeyDown(KeyCode.R))
            {
                EndTurn();
            }
        }
        else if (playerTurn == (int)Player_Turn.enemy)
        {
            if (Input.GetKeyDown(KeyCode.U))
            {
                EndTurn();
            }
            
            if (Input.GetKeyDown(KeyCode.I))
            {
                CashOut();
            }
            
            if (Input.GetKeyDown(KeyCode.O))
            {
                //Staffs
            }
        
            if (Input.GetKeyDown(KeyCode.P))
            {
                if (placeMode == false)
                {
                    placeMode = true;
                }
                else if (placeMode)
                {
                    placeMode = false;
                }
            }
        }
    }
}

public enum Player_Turn : int
{
    mary,
    enemy
}