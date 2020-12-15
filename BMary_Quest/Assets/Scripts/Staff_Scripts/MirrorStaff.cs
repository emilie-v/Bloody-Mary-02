﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MirrorStaff : MonoBehaviour
{
    public GameControl gameControl;
    public Button abilityButton;
    public Owner owner;
    public GameObject spelplan;
    public LastMove lastMove;

    public int[] lastMovesX;
    public int[] lastMovesY;
    private int[] mirroredX=new int[5] {6,6,6,6,6};
    private int[] mirroredY=new int[5]{6,6,6,6,6};

    public int staffCooldown = 1;

    void Awake()
    {
        spelplan = GameObject.FindGameObjectWithTag("Spelplan");
        lastMove = GameObject.Find("PController").GetComponent<LastMove>();

        abilityButton = GameObject.Find("Buttons/PlayerButtons/StaffButton").GetComponent<Button>();

        abilityButton.onClick.AddListener(staffPower);

        gameControl = GameObject.Find("PController").GetComponent<GameControl>();

        gameControl.marysMaxHealth = 25;
        gameControl.marysHealth = 25;
    }

    void staffPower()
    {
        if (!gameControl.staffUsed)
        {
            //lastMovesX = lastMove.lastMoveX;
            //lastMovesY = lastMove.lastMoveY;
            for(int i=0; i<lastMove.lastMovesX.Length; i++)
            {
                if(lastMove.lastMovesX[i] !=6)
                {
                    //Debug.Log("X: " + lastMoveX + "Y: " + lastMoveY);
                    int diffX, diffY;
                    diffX = lastMove.lastMovesX[i] - 4;
                    diffY = lastMove.lastMovesY[i] - 2;

                    mirroredX[i] = -diffX;
                    mirroredY[i] = -diffY + 2;
                }
            
                //Debug.Log("MirroredX: " + mirroredX + "MirroredY: " + mirroredY);
                if (mirroredX[i] <5)
                {
                    if (spelplan.GetComponent<Spelplan>().gridArray[mirroredX[i], mirroredY[i]].GetComponent<Owner>().owned == (int)Tile_State.empty && gameControl.playerTurn == 0 && lastMove.staffUsed == false)
                    {
                        if(mirroredX[i] < 5 && mirroredY[i] < 5)
                        {
                            SoundManager.Instance.ActivateStaffButtonSound();
                            Debug.Log("Using staff mirror");
                            spelplan.GetComponent<Spelplan>().gridArray[mirroredX[i], mirroredY[i]].GetComponent<Owner>().owned = (int)Tile_State.player1;
                            //gameControl.marysTempPoints++;
                        }
        
                    }
                }
            }    
            
            if (DataAcrossScenes.PlayerChosenStaff == 0 && gameControl.playerTurn == (int)Player_Turn.mary)
            {
                gameControl.playerStaffCooldown = staffCooldown;
            }
            if (DataAcrossScenes.EnemyChosenStaff == 0 && gameControl.playerTurn == (int)Player_Turn.enemy)
            {
                gameControl.enemyStaffCooldown = staffCooldown;
            }
            
            lastMove.staffUsed = true;
            gameControl.staffUsed = true;
            gameControl.Staff();
            resetMirrorArray();   
        }
    }
    void resetMirrorArray()
    {
        Debug.Log("ResetMirrorArray körs");
    mirroredX = new int[5] {6,6,6,6,6}; 
    mirroredY = new int[5] {6,6,6,6,6};  
    }
}
