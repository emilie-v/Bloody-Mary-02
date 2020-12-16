using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffManager : MonoBehaviour
{
    //Index for playerSelectedStaff. 
    public static int playerSelectedStaff;

    void Start()
    {
        if (playerSelectedStaff == 0)
        {
            gameObject.AddComponent(typeof(MirrorStaff));
            DataAcrossScenes.PlayerChosenStaff = 0;
        }
        else if (playerSelectedStaff == 1)
        {
            gameObject.AddComponent(typeof(PumpkinStaff));
            DataAcrossScenes.PlayerChosenStaff = 1;
        }
        else if (playerSelectedStaff == 2)
        {
            gameObject.AddComponent(typeof(DarkNightStaff));
            DataAcrossScenes.PlayerChosenStaff = 2;
        }
        else if (playerSelectedStaff == 3)
        {
            gameObject.AddComponent(typeof(HellStaff));
            DataAcrossScenes.PlayerChosenStaff = 3;
        }
        else
        {
            Debug.LogError("No staff selected!");
        }
    }
}
