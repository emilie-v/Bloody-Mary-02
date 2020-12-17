using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffManager : MonoBehaviour
{
    //Index for playerSelectedStaff. 
    public static int playerSelectedStaff;

    void Start()
    {
        if (playerSelectedStaff == (int)Chosen_Staff.mirror)
        {
            DataAcrossScenes.PlayerChosenStaff = (int)Chosen_Staff.mirror;
        }
        else if (playerSelectedStaff == (int)Chosen_Staff.pumpkin)
        {
            DataAcrossScenes.PlayerChosenStaff = (int)Chosen_Staff.pumpkin;
        }
        else if (playerSelectedStaff == (int)Chosen_Staff.skeleton)
        {
            DataAcrossScenes.PlayerChosenStaff = (int)Chosen_Staff.skeleton;
        }
        else if (playerSelectedStaff == (int)Chosen_Staff.moon)
        {
            DataAcrossScenes.PlayerChosenStaff = (int)Chosen_Staff.moon;
        }
        else if (playerSelectedStaff == (int)Chosen_Staff.night)
        {
            DataAcrossScenes.PlayerChosenStaff = (int)Chosen_Staff.night;
        }
        else if (playerSelectedStaff == (int)Chosen_Staff.hell)
        {
            DataAcrossScenes.PlayerChosenStaff = (int)Chosen_Staff.hell;
        }
        else
        {
            Debug.LogError("No staff selected!");
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
