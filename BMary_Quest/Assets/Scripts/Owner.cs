using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Owner : MonoBehaviour
{
    public int xPos;
    public int yPos;

    public int owned;
    //public bool OwnedByMary = false;
    //public bool OwnedByEnemy = false;
    public bool canChange = false;
    public bool skeletonMark;
    public int locked;
    public int specialState;
    /* 
    potentiella states: 1: startbricka för Mary, kan alltid vara hennes, aldrig fiendens, 2: tvärtom mot 1, 3: ett state som t.ex. en stav kan lägga på, gör så att poäng dubblas 4: mer stavspecial. Kanske ska ha separata variabler för stavgrejer. 
    */

    SpriteRenderer tile;
    public Sprite pc;
    public Sprite enemys;
    public Sprite neutral;
    public GameObject gc;
    public GameControl gameControl;
    public GameObject Spelplan;
    private Boardpiece boardpiece;
    private DialogueManager dialogueManager;
    private MirrorStaffHighlight mirrorStaffHighlight;

    public LastMove lastMove;
    
    
    void Start()
    {
        tile = GetComponent<SpriteRenderer>();
        gc = GameObject.FindGameObjectWithTag("Player");
        gameControl = gc.GetComponent<GameControl>();

        pc = Resources.Load<Sprite>("Sprites/Mark_BloodyMary");
        enemys = Resources.Load<Sprite>("Sprites/Mark_Lucifer");
        neutral = Resources.Load<Sprite>("Sprites/Board_Tile");
        Spelplan = GameObject.FindGameObjectWithTag("Spelplan");
        lastMove = GameObject.Find("PController").GetComponent<LastMove>();
        dialogueManager = GameObject.Find("IngameGUI_Canvas").GetComponent<DialogueManager>();
        mirrorStaffHighlight = GameObject.Find("PlayerButtons/StaffButton").GetComponent<MirrorStaffHighlight>();
    }

    void FixedUpdate()
    {
        ResetCanChange();
        BoardHighlight();
    }

    private void OnMouseDown()
    {
        if (gameControl.playerTurn == (int)Player_Turn.mary && gameControl.placeMode && owned == (int)Tile_State.empty)
        {
            transform.DOScale(new Vector3(1.1f, 1.1f, 1f), 0.2f);
        }
    }

    private void OnMouseUp()
    {
        transform.DOScale(new Vector3(1.22f, 1.22f, 1f), 0.2f);
        if (gameControl.playerMoves > 0 && gameControl.placeMode)
        {
            //spelare 2(enemy) controller
            if (gameControl.playerTurn == (int)Player_Turn.enemy)
            {
                if (specialState == 2)
                {
                    owned = (int)Tile_State.player2;
                    canChange = false;
                }
                else
                {
                    CheckNeighbours();
                    if (owned == (int)Tile_State.empty && canChange)
                    {
                        owned = (int)Tile_State.player2;
                        //gameControl.enemyTempPoints++;
                        PiecePlaced();
                    }
                }
            }
            else if (gameControl.playerTurn == (int)Player_Turn.mary)
            {
                if (specialState == 1)
                {
                    owned = (int)Tile_State.player1;
                    canChange = false;
                }
                else
                {
                    CheckNeighbours();
                    if (owned == (int)Tile_State.empty && canChange)
                    {
                        owned = (int)Tile_State.player1;
                        //gameControl.marysTempPoints++;
                        PiecePlaced();
                    }
                }
            }
            else
            {
                Debug.Log("Turn Order Error");
            }
        }
    }

    public void PiecePlaced()
    {
        if(gameControl.playerTurn == (int)Player_Turn.mary)
        {
            SoundManager.Instance.MaryMarkPlacedSound();
        }

        else if (gameControl.playerTurn == (int)Player_Turn.enemy)
        {
            SoundManager.Instance.EnemyMarkPlacedSound();
        }

        canChange = false;
        gameControl.playerMoves--;

        if (gameControl.playerMoves <= 0)
        {
            gameControl.placeMode = false;
        }

        gameControl.NoMoreMoves();
        gameControl.UpdateMarkIndicators();

        if(gameControl.playerTurn == (int)Player_Turn.enemy)
        {
            for(int i=0; i<lastMove.lastMovesX.Length; i++)
            {
                if (lastMove.lastMovesX[i] == 6)
                {
                    lastMove.lastMovesX[i] = xPos;
                    lastMove.lastMovesY[i] = yPos;
                    break;
                }
                else
                {
                    continue;
                }
            }    

        }
    }

    //Currently we only want to check the closest neighbours in the X and Y-axis, 4 tiles. A nested for loop would be the thing if we're going to get all eight. 
    public void CheckNeighbours()
    {
        if (locked > 0)
        {
            canChange = false;
            return;
        }
        
        //if playerturn =marys then temp var =ownedbyMary, if player turn =enemy then temp var = OwnedByEnemy
        if (xPos >= 0 && xPos < Spelplan.GetComponent<Spelplan>().gridArray.GetLength(0) - 1)
        {
            //Temp variabel Vi skulle kunna göra det som är OwnedbyMary till en variabel som går efter state!
            if (Spelplan.GetComponent<Spelplan>().gridArray[xPos + 1, yPos].GetComponent<Owner>().owned == gameControl.playerTurn + 1 && owned == (int)Tile_State.empty)
            {
                canChange = true;
                return;
            }
        }

        if (xPos > 0 && xPos < Spelplan.GetComponent<Spelplan>().gridArray.GetLength(0))
        {
            if (Spelplan.GetComponent<Spelplan>().gridArray[xPos - 1, yPos].GetComponent<Owner>().owned == gameControl.playerTurn + 1 && owned == (int)Tile_State.empty)
            {
                canChange = true;
                return;
            }
        }

        if (yPos >= 0 && yPos < Spelplan.GetComponent<Spelplan>().gridArray.GetLength(1) - 1)
        {
            if (Spelplan.GetComponent<Spelplan>().gridArray[xPos, yPos + 1].GetComponent<Owner>().owned == gameControl.playerTurn + 1 && owned == (int)Tile_State.empty)
            {
                canChange = true;
                return;
            }
        }

        if (yPos > 0 && yPos < Spelplan.GetComponent<Spelplan>().gridArray.GetLength(1))
        {
            if (Spelplan.GetComponent<Spelplan>().gridArray[xPos, yPos - 1].GetComponent<Owner>().owned == gameControl.playerTurn + 1 && owned == (int)Tile_State.empty)
            {
                canChange = true;
                return;
            }
        }

        canChange = false;
    }

    //reset methods
    public void resetBoard()
    {
        owned = (int)Tile_State.empty;
        tile.sprite = neutral;
        gameControl.marysTempPoints = 0;
        gameControl.enemyTempPoints = 0;
    }

    public void resetMary()
    {

        if (owned == (int)Tile_State.player1)
        {
            owned = (int)Tile_State.empty;
        }
        if (specialState == 1)
        {
            owned = (int)Tile_State.player1;
            canChange = false;
        }
    }
    public void resetEnemy()
    {

        if (owned == (int)Tile_State.player2)
        {
            owned = (int)Tile_State.empty;
        }
        if (specialState == 2)
        {
            owned = (int)Tile_State.player2;
            canChange = false;
        }
    }

    public void ResetCanChange()
    {
        canChange = false;
        CheckNeighbours();
        if (!mirrorStaffHighlight.mirrorPreview)
        {
            if (canChange)
            {
                GetComponent<SpriteRenderer>().color = Color.white;
            }
            else  if (canChange == false)
            {
                GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
    }
    
    private void OnMouseOver()
    {
        canChange = false;
        CheckNeighbours();
        if (gameControl.placeMode && !gameControl.pauseMode)
        {
            if (owned == (int)Tile_State.empty && canChange && gameControl.playerMoves > 0)
            {
                GetComponent<SpriteRenderer>().color = Color.green;
            }
            else if (owned == (int)Tile_State.empty && !canChange && gameControl.playerMoves > 0)
            {
                GetComponent<SpriteRenderer>().color = Color.red;
            }
            else
            {
                GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
    }

    private void BoardHighlight()
    {
        if (gameControl.placeMode)
        {
            if (owned == (int)Tile_State.empty && canChange && gameControl.playerMoves > 0)
            {
                GetComponent<SpriteRenderer>().color = new Color(0.8f, 1f, 0.8f, 1f);
            }
            else if (owned == (int)Tile_State.empty && !canChange && gameControl.playerMoves > 0)
            {
                GetComponent<SpriteRenderer>().color = new Color(1f, 0.8f, 0.8f, 1f);
            }
            else
            {
                GetComponent<SpriteRenderer>().color = Color.white;
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
}

public enum Tile_State : int
{
    empty, // = 0
    player1, // = 1
    player2 // = 2
}
