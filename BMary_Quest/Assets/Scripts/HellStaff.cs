using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HellStaff : MonoBehaviour
{
    public GameControl gameControl;
    public Button abilityButton;

    void Start()
    {
        //Find player button. 
        abilityButton = GameObject.Find("Buttons/PlayerButtons/StaffButton").GetComponent<Button>();
        //Connect button with function.
        abilityButton.onClick.AddListener(hellStaffAbility);

        gameControl = GameObject.Find("PController").GetComponent<GameControl>();
    }

    void hellStaffAbility()
    {

    }
}
