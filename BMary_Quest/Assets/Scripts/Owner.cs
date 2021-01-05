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
    public bool canChange = false;
    public bool skeletonMark;
    public int locked;
    public int specialState;

    private SpriteRenderer tile;
    public Sprite neutral;
    public GameObject gc;
    public GameControl gameControl;
    public GameObject Spelplan;
    private Boardpiece boardpiece;
    private MirrorStaffHighlight mirrorStaffHighlight;

    public LastMove lastMove;

    void Start()
    {
        tile = GetComponent<SpriteRenderer>();
        gc = GameObject.FindGameObjectWithTag("Player");
        gameControl = gc.GetComponent<GameControl>();

        neutral = Resources.Load<Sprite>("Sprites/Board_Tile");
        Spelplan = GameObject.FindGameObjectWithTag("Spelplan");
        lastMove = GameObject.Find("PController").GetComponent<LastMove>();
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
            if (gameControl.playerTurn == (int)Player_Turn.mary)
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
                        PiecePlaced();
                    }
                }
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
            }
        }
    }

    public void CheckNeighbours()
    {
        if (locked > 0)
        {
            canChange = false;
            return;
        }
        
        if (xPos >= 0 && xPos < Spelplan.GetComponent<Spelplan>().gridArray.GetLength(0) - 1)
        {
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
    empty,
    player1,
    player2
}
