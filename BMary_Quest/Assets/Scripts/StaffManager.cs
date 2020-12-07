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
        }
        else if(playerSelectedStaff == 1)
        {
            gameObject.AddComponent(typeof(SecondBaseStaff));
        }
        else
        {
            Debug.LogError("No staff selected!");
        }
    }
}
