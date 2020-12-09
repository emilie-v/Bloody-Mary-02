using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Cursor = UnityEngine.Cursor;
using Image = UnityEngine.UI.Image;
using Random = UnityEngine.Random;
using DG.Tweening;

public class GameControl : MonoBehaviour
{
    public int marysTempPoints;
    private int Char;
    public int enemyTempPoints;
    public GameObject Spelplan;
    private Owner owner;
    private Boardpiece boardpiece;
    [SerializeField] private GUIManager guiManager;
    private LastMove lastMove;
    [SerializeField] private AIBehaviour aiBehaviour;
    
    [SerializeField] private GameObject gameOver;

    public int marysMaxHealth = 20;
    public int enemyMaxHealth = 20;
    public int marysHealth;
    public int enemyHealth;

    private int maxMarksToPlace;
    
    public KeyCode playerPlaceModeHotkey = KeyCode.Q;
    public KeyCode playerStaffHotkey = KeyCode.W;
    public KeyCode playerCashOutHotkey = KeyCode.E;
    public KeyCode playerEndTurnHotkey = KeyCode.R;

    public bool placeMode;
    public bool staffUsed;
    public bool pauseMode;
    public int playerTurn;
    public int playerMoves;
    public int playerMovesPerTurn;
    public int enemyMovesPerTurn;
    private bool canCashOut;
    
    //UI Text
    [SerializeField] private GameObject playerBloodPointsText;
    [SerializeField] private GameObject enemyBloodPointsText;
    
    //UI Fill Bars
    [SerializeField] private Image playerBloodPointsFilling;
    [SerializeField] private Image enemyBloodPointsFilling;
    
    //Mark Buttons
    [SerializeField] private GameObject playerMarkButton;
    [SerializeField] private GameObject enemyMarkButton;
    
    //CashOut Buttons
    [SerializeField] private Image playerCashOutButton;
    [SerializeField] private Image enemyCashOutButton;
    
    //Characters
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject enemy;
    

    private void Awake()
    {
        playerMovesPerTurn = 2;
        enemyMovesPerTurn = 2;
        maxMarksToPlace = 4;
    }


    void Start()
    {
        Spelplan = GameObject.FindGameObjectWithTag("Spelplan");
        lastMove = GameObject.Find("PController").GetComponent<LastMove>();

        playerTurn = (int)Random.Range(0, 2);
        
        marysHealth = marysMaxHealth;
        enemyHealth = enemyMaxHealth;
        
        TurnStart();

        lastMove.staffUsed = true;         
    }

    private void Update()
    {
        if (!pauseMode)
        {
            HotKeys();
        }
    }

    public void EndTurn()
    {
        if (playerTurn == (int)Player_Turn.mary) 
        {
            SoundManager.Instance.EndTurnButtonSound();
            playerTurn = (int)Player_Turn.enemy;
            TurnStart();
        }
        else
        {
            SoundManager.Instance.EndTurnButtonSound();
            playerTurn = (int)Player_Turn.mary;
            lastMove.staffUsed = false;
            TurnStart();
        }
    }
    
    public void TurnStart()
    {
        if (playerTurn == (int)Player_Turn.mary) 
        {
            playerMoves = playerMovesPerTurn;
        }
        else if (playerTurn == (int)Player_Turn.enemy)
        {
            lastMove.resetArray();
            playerMoves = enemyMovesPerTurn;
            if (aiBehaviour.AIMode)
            {
                aiBehaviour.Behaviour();
            }
        }
        
        canCashOut = true;
        staffUsed = false;
        ResetCanChange();
        CharacterScaling();
        CharacterDarkening();
        NoMoreMoves();
        Staff();
        CheckCanCashOut();
        UpdateBloodPoints();
        UpdateMarkIndicators();
    }

    public void CashOut() 
    {
        if (canCashOut)
        {
            if (playerTurn == (int)Player_Turn.mary)
            {
                enemyBloodPointsText.transform.DOShakePosition(0.4f + marysTempPoints * 0.1f, 1 + marysTempPoints, 25, 10);
                SoundManager.Instance.CashOutButtonSound();
                //Reset placed pieces and set temp blood points
                for (int i = 0; i < Spelplan.GetComponent<Spelplan>().gridArray.GetLength(0); i++)
                {
                    for (int j = 0; j < Spelplan.GetComponent<Spelplan>().gridArray.GetLength(1); j++)
                    {
                        if (Spelplan.GetComponent<Spelplan>().gridArray[i, j].GetComponent<Owner>().owned == (int)Tile_State.player1 && Spelplan.GetComponent<Spelplan>().gridArray[i, j].GetComponent<Owner>().specialState == 0)
                        {
                            marysTempPoints ++;
                            Spelplan.GetComponent<Spelplan>().gridArray[i, j].GetComponent<Owner>().resetMary();
                        }
                    }
                }
                enemyHealth -= marysTempPoints + 1;
                marysTempPoints = 0;
                
                

                GameOver();
            }
            else if (playerTurn == (int)Player_Turn.enemy)
            {
                SoundManager.Instance.CashOutButtonSound();
                playerBloodPointsText.transform.DOShakePosition(0.4f + enemyTempPoints * 0.1f, enemyTempPoints, 25, 10);
                
                //Reset placed pieces and set temp blood points
                for (int i = 0; i < Spelplan.GetComponent<Spelplan>().gridArray.GetLength(0); i++) //Null-pointer exeption?
                {
                    for (int j = 0; j < Spelplan.GetComponent<Spelplan>().gridArray.GetLength(1); j++)
                    {
                        if (Spelplan.GetComponent<Spelplan>().gridArray[i, j].GetComponent<Owner>().owned == (int)Tile_State.player2 && Spelplan.GetComponent<Spelplan>().gridArray[i, j].GetComponent<Owner>().specialState == 0)
                        {
                            enemyTempPoints ++;
                            Spelplan.GetComponent<Spelplan>().gridArray[i, j].GetComponent<Owner>().resetEnemy();
                        }
                    }
                }
                marysHealth -= enemyTempPoints + 1;
                enemyTempPoints = 0;

                GameOver();
            }
            else
            {
                Debug.Log("Cashout error! This should not happen!");
            }
            UpdateBloodPoints();
            canCashOut = false;
            CheckCanCashOut();
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
                SoundManager.Instance.LoseStateSound();
                GameObject.Find("IngameGUI_Canvas/GameOver/Text").GetComponent<Text>().text = "Lucifer Wins";
            } 
            else if (enemyHealth <= 0)
            {
                SoundManager.Instance.WinStateSound();
                GameObject.Find("IngameGUI_Canvas/GameOver/Text").GetComponent<Text>().text = "Mary Wins";
            }
            pauseMode = true;
        }
    }
    
    public void UpdateBloodPoints()
    {
        playerBloodPointsText.GetComponent<Text>().text = marysHealth.ToString();
        enemyBloodPointsText.GetComponent<Text>().text = enemyHealth.ToString();

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
            enemy.transform.DOScale(new Vector3(0.8f, 0.8f, 1), 0.3f);
            
            //Set mary back to right size
            player.transform.DOScale(new Vector3(1, 1, 1), 0.3f);
        } 
        else if (playerTurn == (int)Player_Turn.enemy)
        {
            //Set mary to smaller size
            player.transform.DOScale(new Vector3(0.8f, 0.8f, 1), 0.3f);
            
            //Set enemy back to right size
            enemy.transform.DOScale(new Vector3(1, 1, 1), 0.3f);
        }
    }

    private void CharacterDarkening()
    {
        if (playerTurn == (int)Player_Turn.mary)
        {
            //Set enemy to smaller size
            enemy.GetComponent<Image>().DOColor(new Color(0.5f, 0.5f, 0.5f), 0.3f);
            
            //Set mary back to right size
            player.GetComponent<Image>().DOColor(new Color(1, 1, 1), 0.3f);
        } 
        else if (playerTurn == (int)Player_Turn.enemy)
        {
            //Set enemy to smaller size
            player.GetComponent<Image>().DOColor(new Color(0.5f, 0.5f, 0.5f), 0.3f);
            
            //Set enemy back to right size
            enemy.GetComponent<Image>().DOColor(new Color(1, 1, 1), 0.3f);
        }
    }
    
    public void NoMoreMoves()
    {
        if (playerMoves <= 0)
        {
            if (playerTurn == (int)Player_Turn.mary)
            {
                playerMarkButton.GetComponent<Image>().color = new Color(0.2f, 0.2f, 0.2f);
                guiManager.ButtonGlow();
            }
            else if (playerTurn == (int)Player_Turn.enemy)
            {
                enemyMarkButton.GetComponent<Image>().color = new Color(0.2f, 0.2f, 0.2f);
            }
        }
        else if (playerMoves > 0)
        {
            playerMarkButton.GetComponent<Image>().color = Color.white;
            enemyMarkButton.GetComponent<Image>().color = new Color(1f, 0.1921569f, 0.1921569f);
        }
    }

    public void Staff()
    {
        if (staffUsed)
        {
            GameObject.Find("PlayerButtons/StaffButton").GetComponent<Image>().color = new Color(0.2f, 0.2f, 0.2f);
        }
        else if (!staffUsed)
        {
            GameObject.Find("PlayerButtons/StaffButton").GetComponent<Image>().color = Color.white;
        }
    }
    
    public void CheckCanCashOut()
    {
        if (canCashOut)
        {
            playerCashOutButton.color = playerMarkButton.GetComponent<Image>().color = Color.white;
            enemyCashOutButton.color = enemyMarkButton.GetComponent<Image>().color = new Color(1f, 0.1921569f, 0.1921569f);
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
    
    public void UpdateMarkIndicators()
    {
        GameObject yourMarks = playerMarkButton;
        GameObject enemyMarks = enemyMarkButton;
        if (playerTurn == (int)Player_Turn.mary)
        {
            yourMarks = playerMarkButton;
            enemyMarks = enemyMarkButton;
        }
        else if (playerTurn == (int)Player_Turn.enemy)
        {
            yourMarks = enemyMarkButton;
            enemyMarks = playerMarkButton;
        }

        for (int i = 0; i < maxMarksToPlace; i++)
        {
            yourMarks.transform.GetChild(i).GetComponent<Image>().color = Color.black;
            enemyMarks.transform.GetChild(i).GetComponent<Image>().color = Color.black;
        }

        for (int i = 0; i < playerMoves; i++)
        {
            yourMarks.transform.GetChild(i).GetComponent<Image>().color = Color.white;
        }
    }

    private void HotKeys()
    {
        if (playerTurn == (int)Player_Turn.mary)
        {
            if (Input.GetKeyDown(playerPlaceModeHotkey))
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

            if (Input.GetKeyDown(playerStaffHotkey))
            {
                //Staffs
            }
        
            if (Input.GetKeyDown(playerCashOutHotkey))
            {
                CashOut();
            }
        
            if (Input.GetKeyDown(playerEndTurnHotkey))
            {
                EndTurn();
            }
        }
        else if (playerTurn == (int)Player_Turn.enemy && !aiBehaviour.AIMode)
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

        if (Input.GetKeyDown(KeyCode.A))
        {
            if (!aiBehaviour.AIMode)
            {
                aiBehaviour.AIMode = true;
            }
            else if (aiBehaviour.AIMode)
            {
                aiBehaviour.AIMode = false;
            }
        }
    }
}

public enum Player_Turn : int
{
    mary,
    enemy
}