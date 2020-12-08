using System.Collections;
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

    public int lastMoveX;
    public int lastMoveY;
    private int mirroredX;
    private int mirroredY;

    void Start()
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
        lastMoveX = lastMove.lastMoveX;
        lastMoveY = lastMove.lastMoveY;

        Debug.Log("X: " + lastMoveX + "Y: " + lastMoveY);
        int diffX, diffY;
        diffX = lastMoveX - 4;
        diffY = lastMoveY - 2;

        mirroredX = -diffX;
        mirroredY = -diffY + 2;
        Debug.Log("MirroredX: " + mirroredX + "MirroredY: " + mirroredY);

        if (spelplan.GetComponent<Spelplan>().gridArray[mirroredX, mirroredY].GetComponent<Owner>().owned == (int)Tile_State.empty && gameControl.playerTurn == 0 && lastMove.staffUsed == false)
        {
            if(lastMoveX <= 5 && lastMoveY <= 5)
            {
                Debug.Log("Using staff mirror");
                spelplan.GetComponent<Spelplan>().gridArray[mirroredX, mirroredY].GetComponent<Owner>().owned = (int)Tile_State.player1;
                lastMove.staffUsed = true;
                return;
            }
        }
    }
}

