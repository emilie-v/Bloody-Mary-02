using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffManager : MonoBehaviour
{
    //Index for playerSelectedStaff. 
    //TODO: Set this variable in StaffSelect scene
    public static int playerSelectedStaff;

    void Start()
    {
        //TODO: Add if statement for every staff.
        if (playerSelectedStaff == 0)
        {
            gameObject.AddComponent(typeof(MirrorStaff));
            DataAcrossScenes.PlayerChosenStaff = 0;
        }
        else if(playerSelectedStaff == 1)
        {
            gameObject.AddComponent(typeof(HellStaff));
            DataAcrossScenes.PlayerChosenStaff = 1;
        }
        else if (playerSelectedStaff == 2)
        {
            gameObject.AddComponent(typeof(DarkNightStaff));
            DataAcrossScenes.PlayerChosenStaff = 2;
        }
        else
        {
            Debug.LogError("No staff selected!");
        }
    }
}
