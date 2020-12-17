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

    private Sprite luciferSprite;
    private Sprite ghastellaSprite;
    private Sprite theCountSprite;
    
    
    [SerializeField] private AIBehaviour aiBehaviour;
    
    public GameControl gameControl;
    private Spelplan spelplan;
    private StaffManager staffmanager;

    private void Start()
    {
        GameObject.Find("Button_Info").transform.GetChild(0).transform.GetChild(1).GetComponent<Text>().text = "Hotkey: " + gameControl.playerPlaceModeHotkey;
        GameObject.Find("Button_Info").transform.GetChild(1).transform.GetChild(1).GetComponent<Text>().text = "Hotkey: " + gameControl.playerStaffHotkey;
        GameObject.Find("Button_Info").transform.GetChild(2).transform.GetChild(1).GetComponent<Text>().text = "Hotkey: " + gameControl.playerCashOutHotkey;
        GameObject.Find("Button_Info").transform.GetChild(3).transform.GetChild(1).GetComponent<Text>().text = "Hotkey: " + gameControl.playerEndTurnHotkey;

        luciferSprite = Resources.Load<Sprite>("Sprites/Characters/Enemies/Enemy_Lucifer");
        ghastellaSprite = Resources.Load<Sprite>("Sprites/Characters/Enemies/Enemy_Ghastella");
        theCountSprite = Resources.Load<Sprite>("Sprites/Characters/Enemies/Enemy_Count");
        
        EnemySpriteChange();
    }

    private void Update()
    {
        HotKeys();
    }

    public void CashOutPlayerButton()
    {
        if (gameControl.playerTurn == (int)Player_Turn.mary && !gameControl.pauseMode)
        {
            gameControl.CashOut();
        }
    }

    public void EndPlayerTurnButton()
    {
        if (gameControl.playerTurn == (int)Player_Turn.mary && !gameControl.pauseMode)
        {
            gameControl.EndTurn();
        }
    }

    public void PlaceMarkPlayerButton()
    {
        if (gameControl.playerTurn == (int)Player_Turn.mary && gameControl.playerMoves > 0 && !gameControl.pauseMode)
        {
            if (!gameControl.placeMode)
            {
                gameControl.placeMode = true;
                ButtonGlow();
            }
            else if (gameControl.placeMode)
            {
                gameControl.placeMode = false;
                ButtonGlow();
            }
        }
    }

    public void ButtonGlow()
    {
        if (gameControl.placeMode)
        {
            GameObject.Find("MarkButton_Glow").GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }
        else if (!gameControl.placeMode)
        {
            GameObject.Find("MarkButton_Glow").GetComponent<Image>().color = new Color(1, 1, 1, 0);
        }
    }

    public void CashOutEnemyButton()
    {
        if (gameControl.playerTurn == (int)Player_Turn.enemy && !aiBehaviour.AIMode && !gameControl.pauseMode)
        {
            gameControl.CashOut();
        }
    }

    public void EndEnemyTurnButton()
    {
        if (gameControl.playerTurn == (int)Player_Turn.enemy && !aiBehaviour.AIMode && !gameControl.pauseMode)
        {
            gameControl.EndTurn();
        }
    }
    
    public void PlaceMarkEnemyButton()
    {
        if (gameControl.playerTurn == (int)Player_Turn.enemy && gameControl.playerMoves > 0 && !aiBehaviour.AIMode && !gameControl.pauseMode)
        {
            if (!gameControl.placeMode)
            {
                gameControl.placeMode = true;
            }
            else if (gameControl.placeMode)
            {
                gameControl.placeMode = false;
            }
        }
    }

    public void OpenMenuTab()
    {
        SoundManager.Instance.MenuButtonSound();

        if (menuTab.activeSelf)
        {
            ContinueButton();
        }
        else if (!menuTab.activeSelf)
        {
            menuTab.SetActive(true);
            gameControl.pauseMode = true;          
        }
    }

    public void ContinueButton()
    {
        SoundManager.Instance.MenuButtonSound();

        menuTab.SetActive(false);
        gameControl.pauseMode = false;
    }

    public void RestartButton()
    {
        SoundManager.Instance.MenuButtonSound();

        Owner[] owners = FindObjectsOfType<Owner>();

        foreach (Owner tile in owners)
        {
            tile.resetBoard();
            tile.resetMary();
            tile.resetEnemy();
        }

        gameControl.marysHealth = gameControl.marysMaxHealth;
        gameControl.enemyHealth = gameControl.enemyMaxHealth;
        gameControl.UpdateBloodPoints();
        gameControl.GameOver();
        
        gameControl.Start();
        gameControl.gameOver.SetActive(false);
        gameControl.pauseMode = false;
    }

    public void BackToMainMenuButton()
    {
        SoundManager.Instance.LockedWarningPopUpSound();
        SceneManager.LoadScene("MainMenu");
    }

    public void ExitGameButton()
    {
        SoundManager.Instance.LockedWarningPopUpSound();
        Application.Quit();
    }
    
    private void HotKeys()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OpenMenuTab();
        }
    }

    private void EnemySpriteChange()
    {
        var enemySprite = GameObject.Find("MaskEnemy/Enemy").GetComponent<Image>();
        if (DataAcrossScenes.EnemyChosenStaff == (int)Chosen_Staff.pumpkin)
        {
            enemySprite.sprite = ghastellaSprite;
        }
        if (DataAcrossScenes.EnemyChosenStaff == (int)Chosen_Staff.skeleton)
        {
            enemySprite.sprite = ghastellaSprite; //change to senior bones sprite
        }
        if (DataAcrossScenes.EnemyChosenStaff == (int)Chosen_Staff.moon)
        {
            enemySprite.sprite = ghastellaSprite; //change to umbralina sprite
        }
        if (DataAcrossScenes.EnemyChosenStaff == (int)Chosen_Staff.night)
        {
            enemySprite.sprite = theCountSprite;
        }
        if (DataAcrossScenes.EnemyChosenStaff == (int)Chosen_Staff.hell)
        {
            enemySprite.sprite = luciferSprite;
        }
    }
}
