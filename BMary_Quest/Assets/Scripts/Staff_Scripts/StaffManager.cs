using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaffManager : MonoBehaviour
{
    public static int playerSelectedStaff;

    private Sprite[] selectedStaffList;

    private Sprite selectedMirrorStaff;
    private Sprite selectedPumpkinStaff;
    private Sprite selectedSkeletonStaff;
    private Sprite selectedMoonStaff;
    private Sprite selectedDarkestNightStaff;
    private Sprite selectedHellStaff;

    public Image currentSelectedStaff;
    void Start()
    {
        selectedMirrorStaff = Resources.Load<Sprite>("Sprites/Staffs/Staff_Mirror");
        selectedPumpkinStaff = Resources.Load<Sprite>("Sprites/Staffs/Staff_Pumpkin");
        selectedSkeletonStaff = Resources.Load<Sprite>("Sprites/Staffs/Staff_Skeleton");
        selectedMoonStaff = Resources.Load<Sprite>("Sprites/Staffs/Staff_Moon");
        selectedDarkestNightStaff = Resources.Load<Sprite>("Sprites/Staffs/Staff_Darkest_Night");
        selectedHellStaff = Resources.Load<Sprite>("Sprites/Staffs/Staff_Hell");

        selectedStaffList = new Sprite[6];

        selectedStaffList[0] = selectedMirrorStaff;
        selectedStaffList[1] = selectedPumpkinStaff;
        selectedStaffList[2] = selectedSkeletonStaff;
        selectedStaffList[3] = selectedMoonStaff;
        selectedStaffList[4] = selectedDarkestNightStaff;
        selectedStaffList[5] = selectedHellStaff;

        if (playerSelectedStaff == (int)Chosen_Staff.mirror)
        {
            DataAcrossScenes.PlayerChosenStaff = (int)Chosen_Staff.mirror;

            currentSelectedStaff.sprite = selectedStaffList[playerSelectedStaff];
        }
        else if (playerSelectedStaff == (int)Chosen_Staff.pumpkin)
        {
            DataAcrossScenes.PlayerChosenStaff = (int)Chosen_Staff.pumpkin;

            currentSelectedStaff.sprite = selectedStaffList[playerSelectedStaff];
        }
        else if (playerSelectedStaff == (int)Chosen_Staff.skeleton)
        {
            DataAcrossScenes.PlayerChosenStaff = (int)Chosen_Staff.skeleton;

            currentSelectedStaff.sprite = selectedStaffList[playerSelectedStaff];
        }
        else if (playerSelectedStaff == (int)Chosen_Staff.moon)
        {
            DataAcrossScenes.PlayerChosenStaff = (int)Chosen_Staff.moon;

            currentSelectedStaff.sprite = selectedStaffList[playerSelectedStaff];
        }
        else if (playerSelectedStaff == (int)Chosen_Staff.night)
        {
            DataAcrossScenes.PlayerChosenStaff = (int)Chosen_Staff.night;

            currentSelectedStaff.sprite = selectedStaffList[playerSelectedStaff];
        }
        else if (playerSelectedStaff == (int)Chosen_Staff.hell)
        {
            DataAcrossScenes.PlayerChosenStaff = (int)Chosen_Staff.hell;

            currentSelectedStaff.sprite = selectedStaffList[playerSelectedStaff];
        }
    }
}

public enum Chosen_Staff : int
{
    mirror,
    pumpkin,
    skeleton,
    moon,
    night,
    hell
}
