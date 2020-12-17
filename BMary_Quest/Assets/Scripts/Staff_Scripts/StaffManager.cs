using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaffManager : MonoBehaviour
{
    //Index for playerSelectedStaff. 
    public static int playerSelectedStaff;

    Sprite[] selectedStaffList;

    Sprite selectedMirrorStaff;
    Sprite selectedPumpkinStaff;
    Sprite selectedDarkestNightStaff;
    Sprite selectedHellStaff;

    public Image currentSelectedStaff;
    void Start()
    {
        selectedMirrorStaff = Resources.Load<Sprite>("Sprites/Staffs/Staff_Mirror");
        selectedPumpkinStaff = Resources.Load<Sprite>("Sprites/Staffs/Staff_Pumpkin");
        selectedDarkestNightStaff = Resources.Load<Sprite>("Sprites/Staffs/Staff_Darkest_Night");
        selectedHellStaff = Resources.Load<Sprite>("Sprites/Staffs/Staff_HellStaff");

        selectedStaffList = new Sprite[4];

        selectedStaffList[0] = selectedMirrorStaff;
        selectedStaffList[1] = selectedPumpkinStaff;
        selectedStaffList[2] = selectedDarkestNightStaff;
        selectedStaffList[3] = selectedHellStaff;

        if (playerSelectedStaff == 0)
        {
            gameObject.AddComponent(typeof(MirrorStaff));
            DataAcrossScenes.PlayerChosenStaff = 0;

            currentSelectedStaff.sprite = selectedStaffList[playerSelectedStaff];
        }
        else if (playerSelectedStaff == 1)
        {
            gameObject.AddComponent(typeof(PumpkinStaff));
            DataAcrossScenes.PlayerChosenStaff = 1;

            currentSelectedStaff.sprite = selectedStaffList[playerSelectedStaff];
        }
        else if (playerSelectedStaff == 2)
        {
            gameObject.AddComponent(typeof(DarkNightStaff));
            DataAcrossScenes.PlayerChosenStaff = 2;

            currentSelectedStaff.sprite = selectedStaffList[playerSelectedStaff];
        }
        else if (playerSelectedStaff == 3)
        {
            gameObject.AddComponent(typeof(HellStaff));
            DataAcrossScenes.PlayerChosenStaff = 3;

            currentSelectedStaff.sprite = selectedStaffList[playerSelectedStaff];
        }
        else
        {
            Debug.LogError("No staff selected!");
        }
    }
}
