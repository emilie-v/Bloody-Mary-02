using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SecondBaseStaff : MonoBehaviour
{
    // Start is called before the first frame update
    int passiveBloodPoints;

    Button abilityButton;

    GameControl gameControl;

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

        gameControl.marysMaxHealth = 25;
        gameControl.marysHealth = 25;

        gameControl.TurnStart();
    }
}
