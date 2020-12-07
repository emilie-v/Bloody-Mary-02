using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MirrorStaff : MonoBehaviour
{
    public GameControl gameControl;
    public Button abilityButton;

    public int lastMoveX;
    public int lastMoveY;



    void Start()
    {
        //Find player button. 
        abilityButton = GameObject.Find("Buttons/PlayerButtons/StaffButton").GetComponent<Button>();
        //Connect button with function.
        abilityButton.onClick.AddListener(staffPower);

        gameControl = GameObject.Find("PController").GetComponent<GameControl>();

        gameControl.marysMaxHealth = 25;
        gameControl.marysHealth = 25;
        
        gameControl.TurnStart();
    }

    void staffPower()
    {
        Debug.Log("Y: " + lastMoveY + "X: " + lastMoveX);
    }
}
