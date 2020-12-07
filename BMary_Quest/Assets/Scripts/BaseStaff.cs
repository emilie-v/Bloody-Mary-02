using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseStaff : MonoBehaviour
{
    Button abilityButton;
    public GameControl gameControl;

    public void UseStaffAbility()
    {
        Debug.Log("Use Staff");
    }
 
    void Start()
    {
        //Find player button. 
        abilityButton = GameObject.Find("Buttons/PlayerButtons/StaffButton").GetComponent<Button>();
        //Connect button with function.
        abilityButton.onClick.AddListener(UseStaffAbility);

        gameControl = GameObject.Find("PController").GetComponent<GameControl>();

        gameControl.playerMovesPerTurn = 3;
    }

    void Update()
    {
        
    }
}
