using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataAcrossScenes
{
    private static int chosenStaff;
    //kanske ska göras till enum sen, test för att se om det blir smidigt detta! Hellstaff tänker jag mig är 1 för testsyfte.

    public static int ChosenStaff 
    {
        get 
        {
            return chosenStaff;
        }
        set 
        {
            chosenStaff = value;
        }
    }
}
