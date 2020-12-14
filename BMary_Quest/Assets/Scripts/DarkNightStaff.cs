using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DarkNightStaff : MonoBehaviour
{
    public GameControl gameControl;
    public Button abilityButton;
    public LastMove lastMove;

    public int bricksToLock = 2;
    public int bricksToLockLeft = 2;
    
    public int staffCooldown = 1;
    

    void Start()
    {
        //Find player button. 
        abilityButton = GameObject.Find("Buttons/PlayerButtons/StaffButton").GetComponent<Button>();
        //Connect button with function.
        abilityButton.onClick.AddListener(DarkNightStaffActiveAbility);

        gameControl = GameObject.Find("PController").GetComponent<GameControl>();
        lastMove = GameObject.Find("PController").GetComponent<LastMove>();
    }

   public void DarkNightStaffPassiveAbility()
    {
        if (DataAcrossScenes.EnemyChosenStaff == 2 && gameControl.playerTurn == (int)Player_Turn.enemy) 
        {
            gameControl.enemyHealth += gameControl.enemyTempPoints;
        }

        if (DataAcrossScenes.PlayerChosenStaff == 2 && gameControl.playerTurn == (int)Player_Turn.mary)
        {
            gameControl.marysHealth += gameControl.marysTempPoints;
        }
    }
    public void DarkNightStaffActiveAbility()
    {
        if (DataAcrossScenes.PlayerChosenStaff == 2 || DataAcrossScenes.EnemyChosenStaff == 2)
        {
            while (!gameControl.staffUsed && bricksToLockLeft > 0)
            {
                foreach (Transform child in GameObject.Find("Spelplan").transform)
                {
                    if (child.GetComponent<Owner>().owned == (int)Tile_State.empty && !child.GetComponent<Owner>().locked)
                    {
                        if (Random.Range(0, 25) == 0 && bricksToLockLeft > 0)
                        {
                            bricksToLockLeft--;
                            child.GetComponent<Owner>().locked = true;
                        }
                    }
                }
            }

            if (DataAcrossScenes.PlayerChosenStaff == 2 && gameControl.playerTurn == (int)Player_Turn.mary)
            {
                gameControl.playerStaffCooldown = staffCooldown;
            }
            if (DataAcrossScenes.EnemyChosenStaff == 2 && gameControl.playerTurn == (int)Player_Turn.enemy)
            {
                gameControl.enemyStaffCooldown = staffCooldown;
            }

            gameControl.staffUsed = true;
            gameControl.Staff();
        }
    }
}
