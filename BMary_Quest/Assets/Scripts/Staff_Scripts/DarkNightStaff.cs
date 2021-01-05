using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DarkNightStaff : MonoBehaviour
{
    public GameControl gameControl;
    public Button abilityButton;
    [SerializeField] private DialogueTrigger dialogueTrigger;

    public int bricksToLock = 1;
    public int bricksToLockLeft = 1;
    
    public int staffCooldown = 1;
    

    void Start()
    {
        abilityButton = GameObject.Find("Buttons/PlayerButtons/StaffButton").GetComponent<Button>();
        abilityButton.onClick.AddListener(DarkNightStaffActiveAbility);

        gameControl = GameObject.Find("PController").GetComponent<GameControl>();
    }

   public void DarkNightStaffPassiveAbility()
    {
        if (DataAcrossScenes.EnemyChosenStaff == (int)Chosen_Staff.night && gameControl.playerTurn == (int)Player_Turn.enemy) 
        {
            gameControl.enemyHealth += (gameControl.enemyTempPoints + 1) / 2;
        }
        if (DataAcrossScenes.PlayerChosenStaff == (int)Chosen_Staff.night && gameControl.playerTurn == (int)Player_Turn.mary)
        {
            gameControl.marysHealth += (gameControl.marysTempPoints + 1) / 2;
        }
    }
    public void DarkNightStaffActiveAbility()
    {
        if (DataAcrossScenes.PlayerChosenStaff == (int)Chosen_Staff.night || DataAcrossScenes.EnemyChosenStaff == (int)Chosen_Staff.night)
        {
            if (gameControl.playerTurn == (int)Player_Turn.mary && gameControl.playerStaffCooldown <= 0)
            {
                LockBrick();
            }
            if (gameControl.playerTurn == (int)Player_Turn.enemy && gameControl.enemyStaffCooldown <= 0)
            {
                LockBrick();
            }
            
            if (DataAcrossScenes.PlayerChosenStaff == (int)Chosen_Staff.night && gameControl.playerTurn == (int)Player_Turn.mary)
            {
                gameControl.playerStaffCooldown = staffCooldown;
                gameControl.staffUsed = true;
                gameControl.Staff();
                dialogueTrigger.MaryStaffPower();
                SoundManager.Instance.ActivateStaffButtonSound();
            }
            if (DataAcrossScenes.EnemyChosenStaff == (int)Chosen_Staff.night && gameControl.playerTurn == (int)Player_Turn.enemy)
            {
                gameControl.enemyStaffCooldown = staffCooldown;
                gameControl.staffUsed = true;
                gameControl.Staff();
                SoundManager.Instance.ActivateStaffButtonSound();
            }
        }
    }

    private void LockBrick()
    {
        while (!gameControl.staffUsed && bricksToLockLeft > 0)
        {
            int check = 0;
            foreach (Transform child in GameObject.Find("Spelplan").transform)
            {
                if (child.GetComponent<Owner>().owned == (int)Tile_State.empty && child.GetComponent<Owner>().locked <= 0)
                {
                    if (Random.Range(0, 25) == 0 && bricksToLockLeft > 0)
                    {
                        bricksToLockLeft--;
                        child.GetComponent<Owner>().locked = 4;
                    }
                }

                if (child.GetComponent<Owner>().owned != (int)Tile_State.empty || child.GetComponent<Owner>().locked > 0)
                {
                    check++;
                }
            }

            if (check >= 25)
            {
                return;
            }
        }
    }
}
