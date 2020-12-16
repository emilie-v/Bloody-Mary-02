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

    [SerializeField] private GameObject spelplan;
    [SerializeField] private GameControl gameControl;
    [SerializeField] private HellStaff hellstaff;
    [SerializeField] private DarkNightStaff darkNightStaff;
    

    private void Start()
    {
        cashOutThreshhold = (int) Random.Range(1, 7);
        cashOutVariable = cashOutThreshhold;
    }

    public void Behaviour()
    {
        CheckCanPlace();
        if (canChangeAmount > 1)
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
        CheckCanPlace();
        yield return new WaitForSeconds(1.2f);
        if (!placeBricksDone)
        { 
            StartCoroutine(PlaceBricks());
        }

        yield return new WaitUntil(() => placeBricksDone);
        yield return new WaitForSeconds(0.2f);
        if (!useStaffDone)
        {
            UseStaff();
        }

        yield return new WaitUntil(() => useStaffDone);
        yield return new WaitForSeconds(0.2f);
        if (!cashOutDone)
        {
            CashOut();
        }

        yield return new WaitUntil(() => cashOutDone);
        yield return new WaitForSeconds(0.2f);
        if (!gameControl.gameOver.activeSelf)
        {
            EndTurn();
        }
    }

    private IEnumerator BlockedActionOrder()
    {
        yield return new WaitForSeconds(1.2f);
        if (!cashOutDone)
        {
            ForceCashOut();
        }
        
        CheckCanPlace();
        yield return new WaitUntil(() => cashOutDone);
        yield return new WaitForSeconds(0.2f);
        if (!placeBricksDone)
        { 
            StartCoroutine(PlaceBricks());
        }
        
        yield return new WaitUntil(() => placeBricksDone);
        yield return new WaitForSeconds(0.2f);
        if (!useStaffDone)
        {
            UseStaff();
        }
        
        
        yield return new WaitUntil(() => useStaffDone);
        yield return new WaitForSeconds(0.2f);
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
        if (!gameControl.staffUsed)
        {
            if (DataAcrossScenes.EnemyChosenStaff == 3 && gameControl.enemyStaffCooldown == 0)
            {
                hellstaff.hellStaffActiveAbility();
            }
            else if (DataAcrossScenes.EnemyChosenStaff == 2 && gameControl.enemyStaffCooldown == 0)
            {
                darkNightStaff.DarkNightStaffActiveAbility();
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
    //TODO First, place Bricks at random
    //TODO Second, use staff
    //TODO Third, decide if cashout or not (randomize a thresh hold)
    //TODO Fourth, end turn

    //TODO Randomize delay between moves
}
