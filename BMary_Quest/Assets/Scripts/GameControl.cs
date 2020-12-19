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
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    private Owner owner;
    public DialogueManager dialogueManager;
    private HellStaff hellStaff;
    private DarkNightStaff darknightStaff;
    private PumpkinStaff pumpkinStaff;
    private SkeletonStaff skeletonStaff;
    private MoonStaff moonStaff;
    private MirrorStaff mirrorStaff;
    private Boardpiece boardpiece;
    public RewardScreen rewardScreen;
    public PoseAnimation poseAnimation;
    public CharacterAnimations characterAnimations;
    [SerializeField] private GUIManager guiManager;
    private LastMove lastMove;
    [SerializeField] private AIBehaviour aiBehaviour;

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
    public bool pauseMode;
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
    
    //UI Fill Bars
    [SerializeField] private Image playerBloodPointsFilling;
    [SerializeField] private Image enemyBloodPointsFilling;
    
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
        rewardScreen = GameObject.Find("PController").GetComponent<RewardScreen>();
        lastMove = GameObject.Find("PController").GetComponent<LastMove>();
        hellStaff = GameObject.Find("PController").GetComponent<HellStaff>();
        darknightStaff = GameObject.Find("PController").GetComponent<DarkNightStaff>();
        moonStaff = GameObject.Find("PController").GetComponent<MoonStaff>();
        skeletonStaff = GameObject.Find("PController").GetComponent<SkeletonStaff>();
        pumpkinStaff = GameObject.Find("PController").GetComponent<PumpkinStaff>();
        mirrorStaff = GameObject.Find("PController").GetComponent<MirrorStaff>();
        playerTurn = (int)Random.Range(0, 2);
        
        marysHealth = marysMaxHealth;
        enemyHealth = enemyMaxHealth;
        
        firstTurn = true;
        
        Marks(playerMarks, enemyMarks);
        
        pumpkinStaff.PumpkinStaffPassiveAbility();
        
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
            dialogueManager.maryIndex = 0;
            dialogueManager.currentMaryDialogue.text = dialogueManager.dialogueMaryList[dialogueManager.maryIndex];
            SoundManager.Instance.EndTurnButtonSound();
            playerTurn = (int)Player_Turn.enemy;
            lastMove.enemyCashedOutThisTurn = false;
            lastMove.enemyHellStaffActivePower = false;
            lastMove.enemyDarkNightStaffActivePower = false;
            if (DataAcrossScenes.PlayerChosenStaff == (int)Chosen_Staff.hell)
            {
                hellStaff.hellStaffPassiveAbility();
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
            dialogueManager.enemyIndex = 0;
            dialogueManager.currentEnemyDialogue.text = dialogueManager.dialogueLuciferList[dialogueManager.enemyIndex];
            SoundManager.Instance.EndTurnButtonSound();
            playerTurn = (int)Player_Turn.mary;
            lastMove.staffUsed = false;
            lastMove.maryCashedOutThisTurn = false;
            lastMove.playerHellStaffActivePower = false;
            lastMove.playerDarkNightStaffActivePower = false;
            if (DataAcrossScenes.EnemyChosenStaff == (int)Chosen_Staff.hell)
            {
                hellStaff.hellStaffPassiveAbility();
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
                            characterAnimations.animator.SetTrigger("Smiling");
                            dialogueManager.maryIndex = Random.Range(2, 3);
                            dialogueManager.currentMaryDialogue.text = dialogueManager.dialogueMaryList[dialogueManager.maryIndex];
                            marysTempPoints ++;
                            spelplan.GetComponent<Spelplan>().gridArray[i, j].GetComponent<Owner>().resetMary();
                        }
                    }
                }


                enemyBloodPointsText.transform.DOShakePosition(0.4f + marysTempPoints * 0.5f, 3 + marysTempPoints, 25, 10);

                if(marysTempPoints > 0)
                {
                    dialogueManager.enemyIndex = Random.Range(4,5);
                    dialogueManager.currentEnemyDialogue.text = dialogueManager.dialogueLuciferList[dialogueManager.enemyIndex];
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
                marysTempPoints = 0;
                lastMove.maryCashedOutThisTurn=true;
                
                GameOver();
            }
            else if (playerTurn == (int)Player_Turn.enemy)
            {
                SoundManager.Instance.CashOutButtonSound();
                
                //Reset placed pieces and set temp blood points
                for (int i = 0; i < spelplan.GetComponent<Spelplan>().gridArray.GetLength(0); i++) //Null-pointer exeption?
                {
                    for (int j = 0; j < spelplan.GetComponent<Spelplan>().gridArray.GetLength(1); j++)
                    {
                        if (spelplan.GetComponent<Spelplan>().gridArray[i, j].GetComponent<Owner>().owned == (int)Tile_State.player2 && spelplan.GetComponent<Spelplan>().gridArray[i, j].GetComponent<Owner>().specialState == 0)
                        {
                            dialogueManager.enemyIndex = Random.Range(2,3);
                            dialogueManager.currentEnemyDialogue.text = dialogueManager.dialogueLuciferList[dialogueManager.enemyIndex];
                            enemyTempPoints ++;
                            spelplan.GetComponent<Spelplan>().gridArray[i, j].GetComponent<Owner>().resetEnemy();
                        }
                    }
                }


                playerBloodPointsText.transform.DOShakePosition(0.4f + enemyTempPoints * 0.5f, 3 + enemyTempPoints, 25, 10);

                if (enemyTempPoints > 0)
                {
                    characterAnimations.animator.SetTrigger("Damaged");
                    dialogueManager.maryIndex = Random.Range(4, 5);
                    dialogueManager.currentMaryDialogue.text = dialogueManager.dialogueMaryList[dialogueManager.maryIndex];
                }


                if (lastMove.playerHellStaffActivePower == true && DataAcrossScenes.EnemyChosenStaff == (int)Chosen_Staff.hell)
                {
                    enemyHealth -= enemyTempPoints + 1;
                    enemyBloodPointsText.transform.DOShakePosition(0.4f + enemyTempPoints * 0.5f, 3 + enemyTempPoints, 25, 10);
                }
                if (DataAcrossScenes.EnemyChosenStaff == (int)Chosen_Staff.night)
                {
                    darknightStaff.DarkNightStaffPassiveAbility();
                }

                marysHealth -= (enemyTempPoints + 1) * enemyDamageMultiplier;
                enemyTempPoints = 0;
                lastMove.enemyCashedOutThisTurn=true;

                GameOver();
            }
            else
            {
                Debug.Log("Cashout error! This should not happen!");
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
                dialogueManager.maryIndex = 8;
                dialogueManager.currentMaryDialogue.text = dialogueManager.dialogueMaryList[dialogueManager.maryIndex];
                dialogueManager.enemyIndex = 8;
                dialogueManager.currentEnemyDialogue.text = dialogueManager.dialogueLuciferList[dialogueManager.enemyIndex];

                SoundManager.Instance.LoseStateSound();
                GameObject.Find("IngameGUI_Canvas/GameOver/Text").GetComponent<Text>().text = "Everyone Lose!";
            }
            else if (marysHealth <= 0)
            {
                characterAnimations.animator.SetTrigger("Angry");
                dialogueManager.maryIndex = 6;
                dialogueManager.currentMaryDialogue.text = dialogueManager.dialogueMaryList[dialogueManager.maryIndex];
                dialogueManager.enemyIndex = 7;
                dialogueManager.currentEnemyDialogue.text = dialogueManager.dialogueLuciferList[dialogueManager.enemyIndex];

                SoundManager.Instance.LoseStateSound();
                GameObject.Find("IngameGUI_Canvas/GameOver/Text").GetComponent<Text>().text = "Mary Lost!";
                rewardScreen.newReward(6);
            } 
            else if (enemyHealth <= 0)
            {
                characterAnimations.animator.SetBool("WinState", true);
                dialogueManager.maryIndex = 7;
                dialogueManager.currentMaryDialogue.text = dialogueManager.dialogueMaryList[dialogueManager.maryIndex];
                dialogueManager.enemyIndex = 6;
                dialogueManager.currentEnemyDialogue.text = dialogueManager.dialogueLuciferList[dialogueManager.enemyIndex];
                DataAcrossScenes.ChosenEnemy += 1;
                SoundManager.Instance.WinStateSound();
                GameObject.Find("IngameGUI_Canvas/GameOver/Text").GetComponent<Text>().text = "Mary Wins!";
                //wait x seconds, win-screen? Story goes on?
                if (DataAcrossScenes.EnemyChosenStaff == (int)Chosen_Staff.pumpkin)
                {
                    if(DataAcrossScenes.pumpkinStaffUnlocked == false)
                    {
                        rewardScreen.newReward(0);
                        DataAcrossScenes.pumpkinStaffUnlocked = true;
                        DataAcrossScenes.seniorBonesUnlocked = true;
                    }
                    else if (DataAcrossScenes.pumpkinStaffUnlocked == true)
                    {
                        rewardScreen.newReward(5);
                    }
                }
                if (DataAcrossScenes.EnemyChosenStaff == (int)Chosen_Staff.skeleton)
                {
                    if(DataAcrossScenes.skeletonStaffUnlocked == false)
                    {
                        rewardScreen.newReward(1);
                        DataAcrossScenes.skeletonStaffUnlocked = true;
                        DataAcrossScenes.umbralinaUnlocked = true;
                    }
                    else if (DataAcrossScenes.skeletonStaffUnlocked == true)
                    {
                        rewardScreen.newReward(5);
                    }
                }
                if (DataAcrossScenes.EnemyChosenStaff == (int)Chosen_Staff.moon)
                {
                    if(DataAcrossScenes.moonStaffUnlocked == false)
                    {
                        rewardScreen.newReward(2);
                        DataAcrossScenes.moonStaffUnlocked = true;
                        DataAcrossScenes.countUnlocked = true;
                    }
                    else if (DataAcrossScenes.moonStaffUnlocked == true)
                    {
                        rewardScreen.newReward(5);
                    }
                }
                if (DataAcrossScenes.EnemyChosenStaff == (int)Chosen_Staff.night)
                {
                    if (DataAcrossScenes.darkNightStaffUnlocked == false)
                    {
                        rewardScreen.newReward(3);
                        DataAcrossScenes.darkNightStaffUnlocked = true;
                        DataAcrossScenes.luciferUnlocked = true;
                    }
                    else if (DataAcrossScenes.darkNightStaffUnlocked == true)
                    {
                        rewardScreen.newReward(5);
                    }
                }
                if (DataAcrossScenes.EnemyChosenStaff == (int)Chosen_Staff.hell)
                {
                    if (DataAcrossScenes.hellStaffUnlocked == false)
                    {
                        DataAcrossScenes.ChosenEnemy = 1;
                        rewardScreen.newReward(4);
                        DataAcrossScenes.hellStaffUnlocked = true;
                    }
                    else if(DataAcrossScenes.hellStaffUnlocked == true)
                    {
                        rewardScreen.newReward(5);
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

    private void CharacterScaling()
    {
        if (playerTurn == (int)Player_Turn.mary)
        {
            //Set enemy to smaller size
            enemy.transform.DOScale(new Vector3(0.8f, 0.8f, 1), 0.3f);
            
            //Set mary back to right size
            player.transform.parent.transform.DOScale(new Vector3(1, 1, 1), 0.3f);
        }
        else if (playerTurn == (int)Player_Turn.enemy)
        {
            //Set mary to smaller size
            player.transform.parent.transform.DOScale(new Vector3(0.8f, 0.8f, 1), 0.3f);
            
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
            player.transform.parent.GetChild(0).GetComponent<Image>().DOColor(new Color(1, 1, 1), 0.3f);
        } 
        else if (playerTurn == (int)Player_Turn.enemy)
        {
            //Set enemy to smaller size
            player.GetComponent<Image>().DOColor(new Color(0.5f, 0.5f, 0.5f), 0.3f);
            player.transform.parent.GetChild(0).GetComponent<Image>().DOColor(new Color(0.5f, 0.5f, 0.5f), 0.3f);
            
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
            characterAnimations.animator.SetTrigger("Scared");
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

    private void StaffCooldown()
    {
        //mirrorStaff
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
        
        //pumpkinStaff
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
        
        //skeletonStaff
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
        
        //moonStaff
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
        
        //darknightStaff
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
        
        //hellStaff
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

    private void UpdateMarkedPiece()
    {
        //Hell Staff
        if (lastMove.enemyHellStaffActivePower)
        {
            spelplan.transform.GetChild(2).transform.GetChild(0).transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = hellStaffMark;
        }
        else if (!lastMove.enemyHellStaffActivePower)
        {
            spelplan.transform.GetChild(2).transform.GetChild(0).transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = null;
        }

        if (lastMove.playerHellStaffActivePower)
        {
            spelplan.transform.GetChild(22).transform.GetChild(0).transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = hellStaffMark;
        }
        else if (!lastMove.playerHellStaffActivePower)
        {
            spelplan.transform.GetChild(22).transform.GetChild(0).transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = null;
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