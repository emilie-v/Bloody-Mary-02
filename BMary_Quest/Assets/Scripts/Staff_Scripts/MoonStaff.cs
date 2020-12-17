using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoonStaff : MonoBehaviour
{
    public GameControl gameControl;
    public Button abilityButton;
    public LastMove lastMove;
    
    public int staffCooldown = 1;
    
    void Start()
    {
        //Find player button. 
        abilityButton = GameObject.Find("Buttons/PlayerButtons/StaffButton").GetComponent<Button>();
        //Connect button with function.
        abilityButton.onClick.AddListener(MoonStaffActiveAbility);

        gameControl = GameObject.Find("PController").GetComponent<GameControl>();
        lastMove = GameObject.Find("PController").GetComponent<LastMove>();
    }

    public void MoonStaffPassiveAbility()
    {
        
    }
    
    public void MoonStaffActiveAbility()
    {
        if (!gameControl.staffUsed)
        {
            if (DataAcrossScenes.PlayerChosenStaff == (int)Chosen_Staff.moon || DataAcrossScenes.EnemyChosenStaff == (int)Chosen_Staff.moon)
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

            if (DataAcrossScenes.PlayerChosenStaff == (int)Chosen_Staff.moon)
            {
                gameControl.playerStaffCooldown = staffCooldown;
                gameControl.staffUsed = true;
                gameControl.Staff();
            }
            if (DataAcrossScenes.EnemyChosenStaff == (int)Chosen_Staff.moon)
            {
                gameControl.enemyStaffCooldown = staffCooldown;
                gameControl.staffUsed = true;
                gameControl.Staff();
            }
        }
    }
}
