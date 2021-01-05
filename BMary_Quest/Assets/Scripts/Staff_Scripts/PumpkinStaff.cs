using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PumpkinStaff : MonoBehaviour
{
    public GameControl gameControl;
    public Button abilityButton;
    [SerializeField] private DialogueTrigger dialogueTrigger;
    public int staffCooldown = 2;

    private int movesPerTurn = 3;

    void Awake()
    {
        abilityButton = GameObject.Find("Buttons/PlayerButtons/StaffButton").GetComponent<Button>();
        abilityButton.onClick.AddListener(PumpkinStaffActiveAbility);

        gameControl = GameObject.Find("PController").GetComponent<GameControl>();
    }

    public void PumpkinStaffPassiveAbility()
    {
        if (DataAcrossScenes.PlayerChosenStaff == (int)Chosen_Staff.pumpkin)
        {
            gameControl.playerMovesPerTurn = movesPerTurn;
        }
        if (DataAcrossScenes.EnemyChosenStaff == (int)Chosen_Staff.pumpkin)
        {
            gameControl.enemyMovesPerTurn = movesPerTurn;
        }
    }

    public void PumpkinStaffActiveAbility()
    {
        if (!gameControl.staffUsed)
        {
            SoundManager.Instance.ActivateStaffButtonSound();
            if (DataAcrossScenes.PlayerChosenStaff == (int)Chosen_Staff.pumpkin && gameControl.playerTurn == (int)Player_Turn.mary && gameControl.playerStaffCooldown <= 0)
            {
                dialogueTrigger.MaryStaffPower();
                gameControl.enemyStaffCooldown++;
                gameControl.playerStaffCooldown = staffCooldown;
                gameControl.staffUsed = true;
                gameControl.Staff();
            }
            if (DataAcrossScenes.EnemyChosenStaff == (int)Chosen_Staff.pumpkin && gameControl.playerTurn == (int)Player_Turn.enemy && gameControl.enemyStaffCooldown <= 0)
            {
                gameControl.playerStaffCooldown++;
                gameControl.enemyStaffCooldown = staffCooldown;
                gameControl.staffUsed = true;
                gameControl.Staff();
            }
        }
    }
}
