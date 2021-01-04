using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class AIBehaviour : MonoBehaviour
{
    public bool AIMode = true;
    [SerializeField] private int canChangeAmount;
    [SerializeField] private int cashOutVariable;
    [SerializeField] private int cashOutThreshhold;
    [SerializeField] private int ownedPieces;
    private bool placeBricksDone;
    private bool useStaffDone;
    private bool cashOutDone;
    private bool readyToEndTurn;

    [SerializeField] private DialogueTrigger dialogueTrigger;
    [SerializeField] private GameObject spelplan;
    [SerializeField] private GameControl gameControl;
    [SerializeField] private HellStaff hellstaff;
    [SerializeField] private DarkNightStaff darkNightStaff;
    [SerializeField] private MoonStaff moonStaff;
    [SerializeField] private SkeletonStaff skeletonStaff;
    [SerializeField] private PumpkinStaff pumpkinStaff;
    [SerializeField] private StartRandomizer startRandomizer;
    

    private void Start()
    {
        cashOutThreshhold = (int)Random.Range(1, 7);
        cashOutVariable = cashOutThreshhold;
    }

    public void Behaviour()
    {
        CheckCanPlace();
        if (DataAcrossScenes.EnemyChosenStaff == (int)Chosen_Staff.moon && canChangeAmount > 1)
        {
            StartCoroutine(UmbralinaOrder());
        }
        else if (canChangeAmount > 1)
        {
            StartCoroutine(DefaultActionOrder());
        }
        else
        {
            StartCoroutine(BlockedActionOrder());
        }
    }
    
    private IEnumerator DefaultActionOrder()
    {
        yield return new WaitUntil(() => startRandomizer.gameStart);
        CheckCanPlace();
        yield return new WaitForSeconds(1.2f);
        if (!placeBricksDone)
        { 
            StartCoroutine(PlaceBricks());
        }

        yield return new WaitUntil(() => placeBricksDone);
        yield return new WaitForSeconds(0.5f);
        if (!useStaffDone)
        {
            UseStaff();
        }

        yield return new WaitUntil(() => useStaffDone);
        yield return new WaitForSeconds(1f);
        if (!cashOutDone)
        {
            CashOut();
        }

        yield return new WaitUntil(() => cashOutDone);
        yield return new WaitForSeconds(0.5f);
        if (!gameControl.gameOver.activeSelf)
        {
            EndTurn();
        }
    }

    private IEnumerator UmbralinaOrder()
    {
        yield return new WaitUntil(() => startRandomizer.gameStart);
        yield return new WaitForSeconds(1.2f);
        if (!useStaffDone)
        {
            UseStaff();
        }
        
        yield return new WaitUntil(() => useStaffDone);
        yield return new WaitForSeconds(1f);
        if (!placeBricksDone)
        { 
            StartCoroutine(PlaceBricks());
        }
        
        yield return new WaitUntil(() => placeBricksDone);
        yield return new WaitForSeconds(1f);
        if (!cashOutDone)
        {
            CashOut();
        }
        
        yield return new WaitUntil(() => cashOutDone);
        yield return new WaitForSeconds(0.5f);
        if (!gameControl.gameOver.activeSelf)
        {
            EndTurn();
        }
    }

    private IEnumerator BlockedActionOrder()
    {
        yield return new WaitUntil(() => startRandomizer.gameStart);
        yield return new WaitForSeconds(1.2f);
        if (!useStaffDone)
        {
            UseStaff();
        }
        
        yield return new WaitUntil(() => useStaffDone);
        yield return new WaitForSeconds(0.5f);
        if (!cashOutDone)
        {
            ForceCashOut();
        }
        
        CheckCanPlace();
        yield return new WaitUntil(() => cashOutDone);
        yield return new WaitForSeconds(0.4f);
        if (!placeBricksDone)
        { 
            StartCoroutine(PlaceBricks());
        }
        
        yield return new WaitUntil(() => placeBricksDone);
        yield return new WaitForSeconds(0.5f);
        if (!gameControl.gameOver.activeSelf)
        {
            EndTurn();
        }
    }

    private IEnumerator PlaceBricks()
    {
        gameControl.placeMode = true;
        
        while (gameControl.playerMoves > 0 && canChangeAmount > 0)
        {
            CheckCanPlace();
            for (int i = 0; i < spelplan.transform.childCount; i++)
            {
                var child = spelplan.transform.GetChild(i);
                var childScript = child.GetComponent<Owner>();
                if (child == spelplan)
                {
                    continue;
                }

                childScript.CheckNeighbours();
                if (childScript.canChange)
                {
                    if ((int)Random.Range(0, canChangeAmount) == 0 && gameControl.playerMoves > 0 && childScript.owned == (int)Tile_State.empty)
                    {
                        yield return new WaitForSeconds(Random.Range(0.3f, 1));
                        childScript.owned = (int)Tile_State.player2;
                        //gameControl.enemyTempPoints++;
                        childScript.PiecePlaced();
                    }
                }
            }
        }

        gameControl.placeMode = false;
        placeBricksDone = true;
    }
    
    private void CheckCanPlace()
    {
        canChangeAmount = 0;
        for (int i = 0; i < spelplan.transform.childCount; i++)
        {
            var child = spelplan.transform.GetChild(i);
            var childScript = child.GetComponent<Owner>();
            childScript.CheckNeighbours();
            
            if (child == spelplan)
            {
                continue;
            }

            if (childScript.canChange)
            {
                canChangeAmount++;
            }
        }
    }

    private void UseStaff()
    {
        int flipOrNoFlip = 0;
        if (DataAcrossScenes.EnemyChosenStaff == (int)Chosen_Staff.moon)
        {
            foreach (Transform child in GameObject.Find("Spelplan").transform)
            {
                if (child.GetComponent<Owner>().owned == (int)Tile_State.player1 && child.GetComponent<Owner>().specialState == 0)
                {
                    flipOrNoFlip++;
                }
                if (child.GetComponent<Owner>().owned == (int)Tile_State.player2 && child.GetComponent<Owner>().specialState == 0)
                {
                    flipOrNoFlip--;
                }
            }
        }
        
        
        if (!gameControl.staffUsed)
        {           
            if (DataAcrossScenes.EnemyChosenStaff == (int)Chosen_Staff.pumpkin && gameControl.enemyStaffCooldown == 0 && gameControl.playerStaffCooldown > 0)
            {
                pumpkinStaff.PumpkinStaffActiveAbility();
                dialogueTrigger.EnemyUsingStaff();
            }
            else if (DataAcrossScenes.EnemyChosenStaff == (int)Chosen_Staff.skeleton && gameControl.enemyStaffCooldown == 0)
            {
                skeletonStaff.SkeletonStaffActiveAbility();
                dialogueTrigger.EnemyUsingStaff();
            }
            else if (DataAcrossScenes.EnemyChosenStaff == (int)Chosen_Staff.moon && gameControl.enemyStaffCooldown == 0 && flipOrNoFlip >= 0)
            {
                moonStaff.MoonStaffActiveAbility();
                dialogueTrigger.EnemyUsingStaff();
            }
            else if (DataAcrossScenes.EnemyChosenStaff == (int)Chosen_Staff.night && gameControl.enemyStaffCooldown == 0)
            {
                darkNightStaff.DarkNightStaffActiveAbility();
                dialogueTrigger.EnemyUsingStaff();
            }
            else if (DataAcrossScenes.EnemyChosenStaff == (int)Chosen_Staff.hell && gameControl.enemyStaffCooldown == 0)
            {
                hellstaff.hellStaffActiveAbility();
                dialogueTrigger.EnemyUsingStaff();
            }
        }
        useStaffDone = true;
    }

    private void CashOut()
    {
        CheckCanPlace();
        if (canChangeAmount > 0)
        {
            cashOutVariable--;
        }
        
        CheckAmountOwnedPieces();
        if (gameControl.marysHealth <= ownedPieces + 1)
        {
            gameControl.CashOut();
        }
        else if (cashOutVariable <= 0)
        {
            gameControl.CashOut();
            cashOutVariable = cashOutThreshhold;
        }
        cashOutDone = true;
        ownedPieces = 0;
    }

    private void CheckAmountOwnedPieces()
    {
        for (int i = 0; i < spelplan.GetComponent<Spelplan>().gridArray.GetLength(0); i++) //Null-pointer exeption?
        {
            for (int j = 0; j < spelplan.GetComponent<Spelplan>().gridArray.GetLength(1); j++)
            {
                if (spelplan.GetComponent<Spelplan>().gridArray[i, j].GetComponent<Owner>().owned == (int)Tile_State.player2 && spelplan.GetComponent<Spelplan>().gridArray[i, j].GetComponent<Owner>().specialState == 0)
                {
                    ownedPieces++;
                }
            }
        }
    }

    private void ForceCashOut()
    {
        gameControl.CashOut();
        cashOutDone = true;
    }

    private void EndTurn()
    {
        placeBricksDone = false;
        useStaffDone = false;
        cashOutDone = false;

        gameControl.EndTurn();
    }
}
