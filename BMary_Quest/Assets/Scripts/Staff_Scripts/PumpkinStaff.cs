using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PumpkinStaff : MonoBehaviour
{
    public GameControl gameControl;
    public Button abilityButton;
    public LastMove lastMove;

    public int staffCooldown = 2;

    private int movesPerTurn = 3;
    

    void Awake()
    {
        //Find player button. 
        abilityButton = GameObject.Find("Buttons/PlayerButtons/StaffButton").GetComponent<Button>();
        //Connect button with function.
        abilityButton.onClick.AddListener(PumpkinStaffActiveAbility);

        gameControl = GameObject.Find("PController").GetComponent<GameControl>();
        lastMove = GameObject.Find("PController").GetComponent<LastMove>();
    }

    public void PumpkinStaffPassiveAbility()
    {
        if (DataAcrossScenes.PlayerChosenStaff == 1)
        {
            gameControl.playerMovesPerTurn = movesPerTurn;
        }
        if (DataAcrossScenes.EnemyChosenStaff == 1)
        {
            gameControl.enemyMovesPerTurn = movesPerTurn;
        }
    }

    public void PumpkinStaffActiveAbility()
    {
        if (!gameControl.staffUsed)
        {
            if (DataAcrossScenes.PlayerChosenStaff == 1 && gameControl.playerTurn == (int)Player_Turn.mary)
            {
                gameControl.enemyStaffCooldown++;
                gameControl.playerStaffCooldown = staffCooldown;
                gameControl.staffUsed = true;
                gameControl.Staff();
            }
            if (DataAcrossScenes.EnemyChosenStaff == 1 && gameControl.playerTurn == (int)Player_Turn.enemy)
            {
                gameControl.playerStaffCooldown++;
                gameControl.enemyStaffCooldown = staffCooldown;
                gameControl.staffUsed = true;
                gameControl.Staff();
            }
        }
    }
}
