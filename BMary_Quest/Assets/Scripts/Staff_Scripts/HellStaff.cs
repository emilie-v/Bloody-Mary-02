using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HellStaff : MonoBehaviour
{
    public GameControl gameControl;
    public Button abilityButton;
    public LastMove lastMove;

    public int staffCooldown = 2;
    

    void Start()
    {
        //Find player button. 
        abilityButton = GameObject.Find("Buttons/PlayerButtons/StaffButton").GetComponent<Button>();
        //Connect button with function.
        abilityButton.onClick.AddListener(hellStaffActiveAbility);

        gameControl = GameObject.Find("PController").GetComponent<GameControl>();
        lastMove = GameObject.Find("PController").GetComponent<LastMove>();
    }

    public void hellStaffPassiveAbility()
    {
        if (DataAcrossScenes.EnemyChosenStaff == (int)Chosen_Staff.hell)
        {
            if(lastMove.enemyCashedOutThisTurn)
                gameControl.enemyHealth += 2;
        }

        if (DataAcrossScenes.PlayerChosenStaff == (int)Chosen_Staff.hell)
        {
            if(lastMove.maryCashedOutThisTurn)
                gameControl.marysHealth += 2;
        }
    }
    public void hellStaffActiveAbility()
    {
        if (DataAcrossScenes.EnemyChosenStaff == (int)Chosen_Staff.hell && gameControl.playerTurn == (int)Player_Turn.enemy && gameControl.enemyStaffCooldown == 0)
        {
            gameControl.enemyStaffCooldown = staffCooldown;
            lastMove.enemyHellStaffActivePower = true;
        }
        if (DataAcrossScenes.PlayerChosenStaff == (int)Chosen_Staff.hell && gameControl.playerTurn == (int)Player_Turn.mary && gameControl.playerStaffCooldown == 0)
        {
            gameControl.playerStaffCooldown = staffCooldown;
            lastMove.playerHellStaffActivePower = true;
        }

        if (DataAcrossScenes.PlayerChosenStaff == (int)Chosen_Staff.hell && gameControl.playerTurn == (int)Player_Turn.mary)
        {
            gameControl.staffUsed = true;
            gameControl.Staff();
        }
        if (DataAcrossScenes.EnemyChosenStaff == (int)Chosen_Staff.hell && gameControl.playerTurn == (int)Player_Turn.enemy)
        {
            gameControl.staffUsed = true;
            gameControl.Staff();
        }
    }
}
