using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DarkNightStaff : MonoBehaviour
{
    public GameControl gameControl;
    public Button abilityButton;
    public LastMove lastMove;
    

    void Start()
    {
        //Find player button. 
        abilityButton = GameObject.Find("Buttons/PlayerButtons/StaffButton").GetComponent<Button>();
        //Connect button with function.
        //abilityButton.onClick.AddListener(darkNightStaffActiveAbility);

        gameControl = GameObject.Find("PController").GetComponent<GameControl>();
        lastMove = GameObject.Find("PController").GetComponent<LastMove>();
    }

   public void darkNightStaffPassiveAbility()
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
    public void darkNightStaffActiveAbility()
    {

    }
}
