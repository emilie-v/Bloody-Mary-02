using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoonStaff : MonoBehaviour
{
    public GameControl gameControl;
    public Button abilityButton;
    [SerializeField] private DialogueTrigger dialogueTrigger;

    public int staffCooldown = 1;
    
    void Start()
    {
        //Find player button. 
        abilityButton = GameObject.Find("Buttons/PlayerButtons/StaffButton").GetComponent<Button>();
        //Connect button with function.
        abilityButton.onClick.AddListener(MoonStaffActiveAbility);

        gameControl = GameObject.Find("PController").GetComponent<GameControl>();

        if (DataAcrossScenes.EnemyChosenStaff == (int)Chosen_Staff.moon)
        {
            gameControl.enemyMaxHealth = 25;
            gameControl.enemyHealth = 25;
        }
    }
    
    public void MoonStaffActiveAbility()
    {
        if (!gameControl.staffUsed)
        {
            SoundManager.Instance.ActivateStaffButtonSound();
            if (DataAcrossScenes.PlayerChosenStaff == (int)Chosen_Staff.moon && gameControl.playerTurn == (int)Player_Turn.mary && gameControl.playerStaffCooldown <= 0
                || DataAcrossScenes.EnemyChosenStaff == (int)Chosen_Staff.moon && gameControl.playerTurn == (int)Player_Turn.enemy && gameControl.enemyStaffCooldown <= 0)
            {
                foreach (Transform child in GameObject.Find("Spelplan").transform)
                {
                    if (child.GetComponent<Owner>().owned != (int)Tile_State.empty && child.GetComponent<Owner>().specialState == 0)
                    {
                        if (child.GetComponent<Owner>().owned == (int)Tile_State.player1)
                        {
                            child.GetComponent<Owner>().owned = (int)Tile_State.player2;
                        }
                        else if (child.GetComponent<Owner>().owned == (int)Tile_State.player2)
                        {
                            child.GetComponent<Owner>().owned = (int)Tile_State.player1;
                        }
                    }
                }
            }

            if (DataAcrossScenes.PlayerChosenStaff == (int)Chosen_Staff.moon && gameControl.playerTurn == (int)Player_Turn.mary)
            {
                gameControl.playerStaffCooldown = staffCooldown;
                gameControl.staffUsed = true;
                gameControl.Staff();
                dialogueTrigger.MaryStaffPower();
                SoundManager.Instance.ActivateStaffButtonSound();
            }
            if (DataAcrossScenes.EnemyChosenStaff == (int)Chosen_Staff.moon && gameControl.playerTurn == (int)Player_Turn.enemy)
            {
                gameControl.enemyStaffCooldown = staffCooldown;
                gameControl.staffUsed = true;
                gameControl.Staff();
                SoundManager.Instance.ActivateStaffButtonSound();
            }
        }
    }
}
