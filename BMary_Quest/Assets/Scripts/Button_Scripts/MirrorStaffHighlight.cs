using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MirrorStaffHighlight : MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler
{
    public MirrorStaff mirrorStaff;
    public LastMove lastmove;
    public GameObject spelplan;
    public GameControl gameControl;
    public bool mirrorPreview;

    private void Awake()
    {
        spelplan = GameObject.FindGameObjectWithTag("Spelplan");
        lastmove = GameObject.Find("PController").GetComponent<LastMove>();
        gameControl = GameObject.Find("PController").GetComponent<GameControl>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!gameControl.staffUsed && gameControl.playerTurn == (int)Player_Turn.mary && DataAcrossScenes.PlayerChosenStaff == (int)Chosen_Staff.mirror)
        {
            mirrorPreview = true;
            
            foreach (Transform child in GameObject.Find("Spelplan").transform)
            {
                child.GetComponent<SpriteRenderer>().color = new Color(1f, 0.8f, 0.8f, 1f);
            }
            for(int i = 0; i < lastmove.lastMovesX.Length; i++)
            {
                //Debug.Log("MirroredX: " + mirroredX + "MirroredY: " + mirroredY);
                if (mirrorStaff.mirroredX[i] < 5)
                {
                    if (spelplan.GetComponent<Spelplan>().gridArray[mirrorStaff.mirroredX[i], mirrorStaff.mirroredY[i]].GetComponent<Owner>().owned == (int)Tile_State.empty 
                        && gameControl.playerTurn == 0 && lastmove.staffUsed == false)
                    {
                        if(mirrorStaff.mirroredX[i] < 5 && mirrorStaff.mirroredY[i] < 5)
                        {
                            spelplan.GetComponent<Spelplan>().gridArray[mirrorStaff.mirroredX[i], mirrorStaff.mirroredY[i]].GetComponent<SpriteRenderer>().color = new Color(0.8f, 1f, 0.8f, 1f);
                        }
                    }
                }
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (gameControl.playerTurn == (int)Player_Turn.mary)
        {
            mirrorPreview = false;
            foreach (Transform child in GameObject.Find("Spelplan").transform)
            {
                child.GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
    }
}
