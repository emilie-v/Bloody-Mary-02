﻿using System;
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
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    private Owner owner;
    public DialogueTrigger dialogueTrigger;
    private HellStaff hellStaff;
    private DarkNightStaff darknightStaff;
    private PumpkinStaff pumpkinStaff;
    private SkeletonStaff skeletonStaff;
    private MoonStaff moonStaff;
    private MirrorStaff mirrorStaff;
    private Boardpiece boardpiece;
    public RewardScreen rewardScreen;
    public LevelLoaderTransition levelLoaderTransition;
    public CharacterAnimations characterAnimations;
    public EnemyAnimations enemyAnimations;
    [SerializeField] private GUIManager guiManager;
    [SerializeField] private AIBehaviour aiBehaviour;
    [SerializeField] private TurnStartText turnStartText;
    private LastMove lastMove;

    public GameObject spelplan;
    public GameObject gameOver;

    public int marysMaxHealth = 20;
    public int enemyMaxHealth = 20;
    public int marysHealth;
    public int enemyHealth;

    public int maryDamageMultiplier;
    public int enemyDamageMultiplier;

    public Button abilityButton;

    private int maxMarksToPlace;

    public int playerStaffCooldown;
    public int enemyStaffCooldown;
    
    public KeyCode playerPlaceModeHotkey = KeyCode.Q;
    public KeyCode playerStaffHotkey = KeyCode.W;
    public KeyCode playerCashOutHotkey = KeyCode.E;
    public KeyCode playerEndTurnHotkey = KeyCode.R;

    public bool placeMode;
    public bool staffUsed;
    public bool pauseMode = true;
    public bool firstTurn;
    public int playerTurn;
    public int playerMoves;
    public int playerMovesPerTurn;
    public int enemyMovesPerTurn;
    public int marysTempPoints;
    public int enemyTempPoints;
    private int Char;
    private bool canCashOut;
    
    //UI Text
    [SerializeField] private GameObject playerBloodPointsText;
    [SerializeField] private GameObject enemyBloodPointsText;
    
    //Mark Buttons
    [SerializeField] private GameObject playerMarkButton;
    [SerializeField] private GameObject enemyMarkButton;
    
    //Marks
    private Sprite playerMarks;
    private Sprite enemyMarks;
    
    //CashOut Buttons
    [SerializeField] private Image playerCashOutButton;
    [SerializeField] private Image enemyCashOutButton;
    
    //Characters
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject enemy;

    public Sprite hellStaffMark;
    
    private void Awake()
    {
        playerMovesPerTurn = 2;
        enemyMovesPerTurn = 2;
        maxMarksToPlace = 4;
        marysMaxHealth = 20;
        enemyMaxHealth = 20;
        playerMarks = Resources.Load<Sprite>("Sprites/Marks/Mark_BloodyMary");
        enemyMarks = Resources.Load<Sprite>("Sprites/Marks/Mark_Lucifer");
 
    }

    public void Start()
    {
        spelplan = GameObject.Find("Spelplan");
        characterAnimations = GameObject.Find("PlayerMary").GetComponent<CharacterAnimations>();
        enemyAnimations = GameObject.Find("PlayerEnemy").GetComponent<EnemyAnimations>();
        rewardScreen = GameObject.Find("PController").GetComponent<RewardScreen>();
        lastMove = GameObject.Find("PController").GetComponent<LastMove>();
        hellStaff = GameObject.Find("PController").GetComponent<HellStaff>();
        darknightStaff = GameObject.Find("PController").GetComponent<DarkNightStaff>();
        moonStaff = GameObject.Find("PController").GetComponent<MoonStaff>();
        skeletonStaff = GameObject.Find("PController").GetComponent<SkeletonStaff>();
        pumpkinStaff = GameObject.Find("PController").GetComponent<PumpkinStaff>();
        mirrorStaff = GameObject.Find("PController").GetComponent<MirrorStaff>();
        playerTurn = 0;
        
        marysHealth = marysMaxHealth;
        enemyHealth = enemyMaxHealth;
        
        firstTurn = true;
        
        Marks(playerMarks, enemyMarks);
        
        pumpkinStaff.PumpkinStaffPassiveAbility();

        lastMove.staffUsed = true;

    }

    private void Update()
    {
        if (!pauseMode)
        {
            HotKeys();
        }
    }

    private void Marks(Sprite playerMarker, Sprite enemyMarker)
    {
        for (int i = 0; i < 4; i++)
        {
            playerMarkButton.transform.GetChild(i).GetComponent<Image>().sprite = playerMarker;
            enemyMarkButton.transform.GetChild(i).GetComponent<Image>().sprite = enemyMarker;
        }
    }

    public void EndTurn()
    {
        if (firstTurn)
        {
            firstTurn = false;

        }
        if (playerTurn == (int)Player_Turn.mary) 
        {
            SoundManager.Instance.EndTurnButtonSound();
            playerTurn = (int)Player_Turn.enemy;
            lastMove.enemyCashedOutThisTurn = false;
            lastMove.enemyHellStaffActivePower = false;
            if (DataAcrossScenes.PlayerChosenStaff == (int)Chosen_Staff.hell)
            {
                hellStaff.HellStaffPassiveAbility();
            }
            if (DataAcrossScenes.EnemyChosenStaff == (int)Chosen_Staff.night)
            {
                darknightStaff.bricksToLockLeft = darknightStaff.bricksToLock;
            }
            
            foreach (Transform child in GameObject.Find("Spelplan").transform)
            {
                child.GetComponent<Owner>().locked--;
            }
            
            if (enemyStaffCooldown > 0)
            {
                enemyStaffCooldown--;
            }
            TurnStart();
        }
        else
        {
            characterAnimations.animator.SetTrigger("Idle");
            SoundManager.Instance.EndTurnButtonSound();
            playerTurn = (int)Player_Turn.mary;
            lastMove.staffUsed = false;
            lastMove.maryCashedOutThisTurn = false;
            lastMove.playerHellStaffActivePower = false;
            if (DataAcrossScenes.EnemyChosenStaff == (int)Chosen_Staff.hell)
            {
                hellStaff.HellStaffPassiveAbility();
            }
            if (DataAcrossScenes.PlayerChosenStaff == (int)Chosen_Staff.night)
            {
                darknightStaff.bricksToLockLeft = darknightStaff.bricksToLock;
            }
            
            foreach (Transform child in GameObject.Find("Spelplan").transform)
            {
                child.GetComponent<Owner>().locked--;
            }
            
            if (playerStaffCooldown > 0)
            {
                playerStaffCooldown--;
            }
            TurnStart();
        }
    }
    
    public void TurnStart()
    {
        turnStartText.StartCoroutine(turnStartText.TurnStart());
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

        foreach (Transform child in GameObject.Find("Spelplan").transform)
        {
            child.GetComponent<Owner>().skeletonMark = false;
        }
        maryDamageMultiplier = 1;
        enemyDamageMultiplier = 1;
        skeletonStaff.piecesToMarkLeft = skeletonStaff.piecesToMark;
        
        placeMode = false;
        canCashOut = true;
        if (firstTurn)
        {
            staffUsed = true;
        }
        else if (!firstTurn)
        {
            staffUsed = false;
        }
        ResetCanChange();
        CharacterScaling();
        CharacterDarkening();
        NoMoreMoves();
        Staff();
        CheckCanCashOut();
        UpdateBloodPoints();
        UpdateMarkIndicators();
        UpdateMarkedPiece();
        guiManager.ButtonGlow();
    }

    public void CashOut() 
    {
        if (canCashOut)
        {
            if (playerTurn == (int)Player_Turn.mary)
            {
                SoundManager.Instance.CashOutButtonSound();
                //Reset placed pieces and set temp blood points
                for (int i = 0; i < spelplan.GetComponent<Spelplan>().gridArray.GetLength(0); i++)
                {
                    for (int j = 0; j < spelplan.GetComponent<Spelplan>().gridArray.GetLength(1); j++)
                    {
                        if (spelplan.GetComponent<Spelplan>().gridArray[i, j].GetComponent<Owner>().owned == (int)Tile_State.player1 && spelplan.GetComponent<Spelplan>().gridArray[i, j].GetComponent<Owner>().specialState == 0)
                        {
                            marysTempPoints++;
                            spelplan.GetComponent<Spelplan>().gridArray[i, j].GetComponent<Owner>().resetMary();
                        }
                    }
                }

                enemyBloodPointsText.transform.DOShakePosition(0.4f + marysTempPoints * 0.5f, 3 + marysTempPoints, 25, 10);

                if(marysTempPoints > 0)
                {
                    enemyAnimations.animator.SetTrigger("Damaged");
                }

                if (lastMove.enemyHellStaffActivePower == true && DataAcrossScenes.EnemyChosenStaff == (int)Chosen_Staff.hell)
                {
                    marysHealth -= marysTempPoints + 1;
                    playerBloodPointsText.transform.DOShakePosition(0.4f + marysTempPoints * 0.5f, 3 + marysTempPoints, 25, 10);
                }

                if(DataAcrossScenes.PlayerChosenStaff == (int)Chosen_Staff.night)
                {
                    darknightStaff.DarkNightStaffPassiveAbility();
                }

                enemyHealth -= (marysTempPoints + 1) * maryDamageMultiplier;
                dialogueTrigger.EnemyHurts();
                dialogueTrigger.MaryCashOut();
                marysTempPoints = 0;
                lastMove.maryCashedOutThisTurn=true;
                
                GameOver();
            }
            else if (playerTurn == (int)Player_Turn.enemy)
            {
                SoundManager.Instance.CashOutButtonSound();
                
                for (int i = 0; i < spelplan.GetComponent<Spelplan>().gridArray.GetLength(0); i++)
                {
                    for (int j = 0; j < spelplan.GetComponent<Spelplan>().gridArray.GetLength(1); j++)
                    {
                        if (spelplan.GetComponent<Spelplan>().gridArray[i, j].GetComponent<Owner>().owned == (int)Tile_State.player2 
                            && spelplan.GetComponent<Spelplan>().gridArray[i, j].GetComponent<Owner>().specialState == 0)
                        {
                            enemyTempPoints++;
                            spelplan.GetComponent<Spelplan>().gridArray[i, j].GetComponent<Owner>().resetEnemy();
                        }
                    }
                }

                playerBloodPointsText.transform.DOShakePosition(0.4f + enemyTempPoints * 0.5f, 3 + enemyTempPoints, 25, 10);

                if (enemyTempPoints > 0)
                {
                    characterAnimations.animator.SetTrigger("Damaged");
                }

                if (lastMove.playerHellStaffActivePower == true)
                {
                    enemyHealth -= enemyTempPoints + 1;
                    enemyBloodPointsText.transform.DOShakePosition(0.4f + enemyTempPoints * 0.5f, 3 + enemyTempPoints, 25, 10);
                }
                
                if (DataAcrossScenes.EnemyChosenStaff == (int)Chosen_Staff.night)
                {
                    darknightStaff.DarkNightStaffPassiveAbility();
                }

                marysHealth -= (enemyTempPoints + 1) * enemyDamageMultiplier;
                dialogueTrigger.EnemyAttack();
                dialogueTrigger.MaryTakingDamage();
                enemyTempPoints = 0;
                lastMove.enemyCashedOutThisTurn=true;

                GameOver();
            }
            
            foreach (Transform child in GameObject.Find("Spelplan").transform)
            {
                child.GetComponent<Owner>().skeletonMark = false;
            }
            
            UpdateBloodPoints();
            canCashOut = false;
            CheckCanCashOut();
        }
    }
    
    public void GameOver()
    {
        if (marysHealth <= 0 || enemyHealth <= 0)
        {
            gameOver.SetActive(true);
            if (marysHealth <= 0 && enemyHealth <= 0)
            {
                SoundManager.Instance.LoseStateSound();
                GameObject.Find("IngameGUI_Canvas/GameOver/Text").GetComponent<Text>().text = "Everyone Lose!";
                rewardScreen.NewReward(6);
                dialogueTrigger.EnemyStalemate();
                dialogueTrigger.MaryStalemate();
            }
            else if (marysHealth <= 0)
            {
                dialogueTrigger.EnemyWins();
                dialogueTrigger.MaryLoss();
                enemyAnimations.animator.SetBool("WinState", true);
                SoundManager.Instance.LoseStateSound();
                GameObject.Find("IngameGUI_Canvas/GameOver/Text").GetComponent<Text>().text = "Mary Lost!";
                rewardScreen.NewReward(6);
            } 
            else if (enemyHealth <= 0)
            {
                dialogueTrigger.EnemyLoses();
                dialogueTrigger.MaryWin();
                characterAnimations.animator.SetBool("WinState", true);
                SoundManager.Instance.WinStateSound();
                GameObject.Find("IngameGUI_Canvas/GameOver/Text").GetComponent<Text>().text = "Mary Wins!";
                
                if (DataAcrossScenes.EnemyChosenStaff == (int)Chosen_Staff.pumpkin)
                {
                    if(DataAcrossScenes.pumpkinStaffUnlocked == false)
                    {
                        rewardScreen.NewReward(0);
                        DataAcrossScenes.pumpkinStaffUnlocked = true;
                        DataAcrossScenes.seniorBonesUnlocked = true;
                        DataAcrossScenes.ChosenEnemy += 1;
                    }
                    else if (DataAcrossScenes.pumpkinStaffUnlocked == true)
                    {
                        rewardScreen.NewReward(5);
                    }
                }
                
                if (DataAcrossScenes.EnemyChosenStaff == (int)Chosen_Staff.skeleton)
                {
                    if(DataAcrossScenes.skeletonStaffUnlocked == false)
                    {
                        rewardScreen.NewReward(1);
                        DataAcrossScenes.skeletonStaffUnlocked = true;
                        DataAcrossScenes.umbralinaUnlocked = true;
                        DataAcrossScenes.ChosenEnemy += 1;
                    }
                    else if (DataAcrossScenes.skeletonStaffUnlocked == true)
                    {
                        rewardScreen.NewReward(5);
                    }
                }
                
                if (DataAcrossScenes.EnemyChosenStaff == (int)Chosen_Staff.moon)
                {
                    if(DataAcrossScenes.moonStaffUnlocked == false)
                    {
                        rewardScreen.NewReward(2);
                        DataAcrossScenes.moonStaffUnlocked = true;
                        DataAcrossScenes.countUnlocked = true;
                        DataAcrossScenes.ChosenEnemy += 1;
                    }
                    else if (DataAcrossScenes.moonStaffUnlocked == true)
                    {
                        rewardScreen.NewReward(5);
                    }
                }
                
                if (DataAcrossScenes.EnemyChosenStaff == (int)Chosen_Staff.night)
                {
                    if (DataAcrossScenes.darkNightStaffUnlocked == false)
                    {
                        DataAcrossScenes.ChosenEnemy = 0;
                        rewardScreen.NewReward(3);
                        DataAcrossScenes.darkNightStaffUnlocked = true;
                        DataAcrossScenes.luciferUnlocked = true;
                    }
                    else if (DataAcrossScenes.darkNightStaffUnlocked == true)
                    {
                        rewardScreen.NewReward(5);
                    }
                }
                
                if (DataAcrossScenes.EnemyChosenStaff == (int)Chosen_Staff.hell)
                {
                    if (DataAcrossScenes.hellStaffUnlocked == false)
                    {
                        DataAcrossScenes.ChosenEnemy = 1;
                        rewardScreen.NewReward(7);
                        DataAcrossScenes.hellStaffUnlocked = true;                    
                    }
                    else if(DataAcrossScenes.hellStaffUnlocked == true)
                    {
                        rewardScreen.NewReward(5);
                    }
                }
            }
            pauseMode = true;
        }
    }

    public void UpdateBloodPoints()
    {
        playerBloodPointsText.GetComponent<Text>().text = marysHealth.ToString();
        enemyBloodPointsText.GetComponent<Text>().text = enemyHealth.ToString();

        for (int i = 0; i < 5; i++)
        {
            GameObject.Find("Player_BloodPointsBar").transform.GetChild(i).GetComponent<Image>().fillAmount 
                = ((float) marysHealth - (i * (float) marysMaxHealth)) / (float) marysMaxHealth;
            GameObject.Find("Enemy_BloodPointsBar").transform.GetChild(i).GetComponent<Image>().fillAmount 
                = ((float) enemyHealth - (i * (float) enemyMaxHealth)) / (float) enemyMaxHealth;
        }
    }

    private void ResetCanChange()
    {
        for (int i = 0; i < spelplan.GetComponent<Spelplan>().gridArray.GetLength(0); i++)
        {
            for (int j = 0; j < spelplan.GetComponent<Spelplan>().gridArray.GetLength(1); j++)
            {
                spelplan.GetComponent<Spelplan>().gridArray[i, j].GetComponent<Owner>().canChange = false;
            }
        }
    }

    public void CharacterScaling()
    {
        if (playerTurn == (int)Player_Turn.mary)
        {
            enemy.transform.DOScale(new Vector3(0.8f, 0.8f, 1), 0.3f);
            
            player.transform.parent.transform.DOScale(new Vector3(1, 1, 1), 0.3f);
        }
        else if (playerTurn == (int)Player_Turn.enemy)
        {
            player.transform.parent.transform.DOScale(new Vector3(0.8f, 0.8f, 1), 0.3f);
            
            enemy.transform.DOScale(new Vector3(1, 1, 1), 0.3f);
        }
    }

    public void CharacterDarkening()
    {
        if (playerTurn == (int)Player_Turn.mary)
        {
            enemy.GetComponent<Image>().DOColor(new Color(0.5f, 0.5f, 0.5f), 0.3f);
            
            player.GetComponent<Image>().DOColor(new Color(1, 1, 1), 0.3f);
            player.transform.parent.GetChild(0).GetComponent<Image>().DOColor(new Color(1, 1, 1), 0.3f);
        } 
        else if (playerTurn == (int)Player_Turn.enemy)
        {
            player.GetComponent<Image>().DOColor(new Color(0.5f, 0.5f, 0.5f), 0.3f);
            player.transform.parent.GetChild(0).GetComponent<Image>().DOColor(new Color(0.5f, 0.5f, 0.5f), 0.3f);
            
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
        if (staffUsed && playerTurn == (int)Player_Turn.mary || playerStaffCooldown > 0)
        {
            GameObject.Find("PlayerButtons/StaffButton").GetComponent<Image>().color = new Color(0.2f, 0.2f, 0.2f);
        }
        else if (!staffUsed && playerTurn == (int)Player_Turn.mary)
        {
            GameObject.Find("PlayerButtons/StaffButton").GetComponent<Image>().color = Color.white;
        }

        if (staffUsed && playerTurn == (int)Player_Turn.enemy || enemyStaffCooldown > 0)
        {
            GameObject.Find("EnemyButtons/StaffButton").GetComponent<Image>().color = new Color(0.2f, 0.2f, 0.2f);
        }
        else if (!staffUsed && playerTurn == (int)Player_Turn.enemy)
        {
            GameObject.Find("EnemyButtons/StaffButton").GetComponent<Image>().color = new Color(1f, 0.1921569f, 0.1921569f);
        }
       
        StaffCooldown();

        GameObject.Find("PlayerButtons/StaffButton").transform.GetChild(1).GetComponent<Text>().text =
            "" + playerStaffCooldown;
        GameObject.Find("EnemyButtons/StaffButton").transform.GetChild(1).GetComponent<Text>().text =
            "" + enemyStaffCooldown;

        if (playerStaffCooldown <= 0)
        {
            GameObject.Find("PlayerButtons/StaffButton").transform.GetChild(1).GetComponent<Text>().text = null;
        }
        if (enemyStaffCooldown <= 0)
        {
            GameObject.Find("EnemyButtons/StaffButton").transform.GetChild(1).GetComponent<Text>().text = null;       
        }
        
        UpdateMarkedPiece();
    }

    public void StaffCooldown()
    {
        if (DataAcrossScenes.PlayerChosenStaff == (int)Chosen_Staff.mirror)
        {
            GameObject.Find("Buttons/PlayerButtons/StaffButton").transform.GetChild(0).GetComponent<Image>().
                DOFillAmount((float) playerStaffCooldown / (float) mirrorStaff.staffCooldown, 0.5f);
        }
        if (DataAcrossScenes.EnemyChosenStaff == (int)Chosen_Staff.mirror)
        {
            GameObject.Find("Buttons/EnemyButtons/StaffButton").transform.GetChild(0).GetComponent<Image>().
                DOFillAmount((float) enemyStaffCooldown / (float) mirrorStaff.staffCooldown, 0.5f);
        }
        
        if (DataAcrossScenes.PlayerChosenStaff == (int)Chosen_Staff.pumpkin)
        {
            GameObject.Find("PlayerButtons/StaffButton").transform.GetChild(0).GetComponent<Image>().
                DOFillAmount((float) playerStaffCooldown / (float) pumpkinStaff.staffCooldown, 0.5f);
        }
        if (DataAcrossScenes.EnemyChosenStaff == (int)Chosen_Staff.pumpkin)
        {
            GameObject.Find("EnemyButtons/StaffButton").transform.GetChild(0).GetComponent<Image>().
                DOFillAmount((float) enemyStaffCooldown / (float) pumpkinStaff.staffCooldown, 0.5f);
        }
        
        if (DataAcrossScenes.PlayerChosenStaff == (int)Chosen_Staff.skeleton)
        {
            GameObject.Find("PlayerButtons/StaffButton").transform.GetChild(0).GetComponent<Image>().
                DOFillAmount((float) playerStaffCooldown / (float) skeletonStaff.staffCooldown, 0.5f);
        }
        if (DataAcrossScenes.EnemyChosenStaff == (int)Chosen_Staff.skeleton)
        {
            GameObject.Find("EnemyButtons/StaffButton").transform.GetChild(0).GetComponent<Image>().
                DOFillAmount((float) enemyStaffCooldown / (float) skeletonStaff.staffCooldown, 0.5f);
        }
        
        if (DataAcrossScenes.PlayerChosenStaff == (int)Chosen_Staff.moon)
        {
            GameObject.Find("PlayerButtons/StaffButton").transform.GetChild(0).GetComponent<Image>().
                DOFillAmount((float) playerStaffCooldown / (float) moonStaff.staffCooldown, 0.5f);
        }
        if (DataAcrossScenes.EnemyChosenStaff == (int)Chosen_Staff.moon)
        {
            GameObject.Find("EnemyButtons/StaffButton").transform.GetChild(0).GetComponent<Image>().
                DOFillAmount((float) enemyStaffCooldown / (float) moonStaff.staffCooldown, 0.5f);
        }
        
        if (DataAcrossScenes.PlayerChosenStaff == (int)Chosen_Staff.night)
        {
            GameObject.Find("PlayerButtons/StaffButton").transform.GetChild(0).GetComponent<Image>().
                DOFillAmount((float) playerStaffCooldown / (float) darknightStaff.staffCooldown, 0.5f);
        }
        if (DataAcrossScenes.EnemyChosenStaff == (int)Chosen_Staff.night)
        {
            GameObject.Find("EnemyButtons/StaffButton").transform.GetChild(0).GetComponent<Image>().
                DOFillAmount((float) enemyStaffCooldown / (float) darknightStaff.staffCooldown, 0.5f);
        }
        
        if (DataAcrossScenes.PlayerChosenStaff == (int)Chosen_Staff.hell)
        {
            GameObject.Find("PlayerButtons/StaffButton").transform.GetChild(0).GetComponent<Image>().
                DOFillAmount((float) playerStaffCooldown / (float) hellStaff.staffCooldown, 0.5f);
        }
        if (DataAcrossScenes.EnemyChosenStaff == (int)Chosen_Staff.hell)
        {
            GameObject.Find("EnemyButtons/StaffButton").transform.GetChild(0).GetComponent<Image>().
                DOFillAmount((float) enemyStaffCooldown / (float) hellStaff.staffCooldown, 0.5f);
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

    public void UpdateMarkedPiece()
    {
        if (lastMove.enemyHellStaffActivePower)
        {
            spelplan.transform.GetChild(2).transform.GetChild(0).transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = hellStaffMark;
            GameObject.Find("PlayerButtons/OutCashButton/LuciferMark").SetActive(true);
        }
        else if (!lastMove.enemyHellStaffActivePower)
        {
            spelplan.transform.GetChild(2).transform.GetChild(0).transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = null;
            GameObject.Find("PlayerButtons/OutCashButton/LuciferMark").SetActive(false);
        }

        if (lastMove.playerHellStaffActivePower)
        {
            spelplan.transform.GetChild(22).transform.GetChild(0).transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = hellStaffMark;
            GameObject.Find("EnemyButtons/OutCashButton/LuciferMark").SetActive(true);
        }
        else if (!lastMove.playerHellStaffActivePower)
        {
            spelplan.transform.GetChild(22).transform.GetChild(0).transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = null;
            GameObject.Find("EnemyButtons/OutCashButton/LuciferMark").SetActive(false);
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
                abilityButton = GameObject.Find("Buttons/PlayerButtons/StaffButton").GetComponent<Button>();
                abilityButton.onClick.Invoke();
                Staff();
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
    
    private void OnDestroy()
    {
        DOTween.Kill(transform);
        DOTween.Kill(gameObject);
        foreach (Transform child in transform)
        {
            DOTween.Kill(child);
        }
    }
    
    public void NextOpponentButton()
    {
        SceneManager.LoadScene("ChooseEnemy");
    }

    public void BackToMainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}

public enum Player_Turn : int
{
    mary,
    enemy
}