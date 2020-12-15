using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataAcrossScenes
{
    public static bool mirrorStaffUnlocked =true;
    public static bool darkNightStaffUnlocked;
    public static bool hellStaffUnlocked;
    public static bool pumpkinStaffUnlocked;

    public static bool luciferUnlocked;
    public static bool countUnlocked;

    private static int enemyChosenStaff;

    private static int playerChosenStaff;
    //kanske ska göras till enum sen, test för att se om det blir smidigt detta! Hellstaff tänker jag mig är 1 för testsyfte.

    public static int EnemyChosenStaff 
    {
        get 
        {
            return enemyChosenStaff;
        }
        set 
        {
            enemyChosenStaff = value;
        }
    }
    public static int PlayerChosenStaff 
    {
        get 
        {
            return playerChosenStaff;
        }
        set 
        {
            playerChosenStaff = value;
        }
    }


}
