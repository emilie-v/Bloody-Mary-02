using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AIBehaviour : MonoBehaviour
{
    public bool AIMode;
    private int canChangeAmount;
    private bool placeBricksDone;
    private bool useStaffDone;
    private bool cashOutDone;
    private bool readyToEndTurn;
    
    [SerializeField] private GameObject spelplan;
    [SerializeField] private GameControl gameControl;
    
    public IEnumerator ActionOrder()
    {
        yield return new WaitForSeconds(1);
        StartCoroutine(PlaceBricks());
        yield return new WaitForSeconds(0);
        UseStaff();
        yield return new WaitForSeconds(0);
        CashOut();
        
        yield return new WaitUntil(() => readyToEndTurn);
        yield return new WaitForSeconds(0.2f);
        EndTurn();
    }

    private void CheckCanPlace()
    {
        canChangeAmount = 0;
        for (int i = 0; i < spelplan.transform.childCount; i++)
        {
            var child = spelplan.transform.GetChild(i);
            var childScript = child.GetComponent<Owner>();
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

    private IEnumerator PlaceBricks()
    {
        gameControl.placeMode = true;
        CheckCanPlace();
        while (gameControl.playerMoves > 0)
        {
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
                        childScript.PiecePlaced();
                    }
                }
            }
        }

        gameControl.placeMode = false;
        placeBricksDone = true;
    }

    private void UseStaff()
    {
        
    }

    private void CashOut()
    {
        
    }

    private void CanEndTurn()
    {
        if (placeBricksDone && useStaffDone && cashOutDone)
        {
            readyToEndTurn = true;
        }
    }

    private void EndTurn()
    {
        placeBricksDone = false;
        gameControl.EndTurn();
    }
    //TODO First, place Bricks at random
    //TODO Second, use staff
    //TODO Third, decide if cashout or not (randomize a thresh hold)
    //TODO Fourth, end turn

    //TODO Randomize delay between moves
}
