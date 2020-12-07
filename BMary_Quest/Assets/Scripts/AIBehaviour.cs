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
    [SerializeField] private int cashOutThreshhold;
    private bool placeBricksDone;
    private bool useStaffDone;
    private bool cashOutDone;
    private bool readyToEndTurn;

    [SerializeField] private GameObject spelplan;
    [SerializeField] private GameControl gameControl;

    private void Start()
    {
        cashOutThreshhold = (int) Random.Range(1, 7);
    }

    public void Behaviour()
    {
        CheckCanPlace();
        if (canChangeAmount > 0)
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
        Debug.Log("default");
        CheckCanPlace();
        yield return new WaitForSeconds(1);
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
        EndTurn();
    }

    private IEnumerator BlockedActionOrder()
    {
        Debug.Log("blocked");
        yield return new WaitForSeconds(1f);
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
        EndTurn();
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
                        gameControl.enemyTempPoints++;
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
        useStaffDone = true;
    }

    private void CashOut()
    {
        cashOutThreshhold--;
        if (gameControl.marysHealth <= gameControl.enemyTempPoints)
        {
            gameControl.CashOut();
        }
        else if (cashOutThreshhold <= 0)
        {
            gameControl.CashOut();
            cashOutThreshhold = Random.Range(1, 5);
        }
        cashOutDone = true;
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
