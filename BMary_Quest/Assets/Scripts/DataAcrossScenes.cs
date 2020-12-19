using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataAcrossScenes
{
    public static bool mirrorStaffUnlocked = true;
    public static bool pumpkinStaffUnlocked;
    public static bool skeletonStaffUnlocked;
    public static bool moonStaffUnlocked;
    public static bool darkNightStaffUnlocked;
    public static bool hellStaffUnlocked;

    public static bool ghastellaUnlocked = true;
    public static bool seniorBonesUnlocked;
    public static bool umbralinaUnlocked;
    public static bool countUnlocked;
    public static bool luciferUnlocked;

    private static int enemyChosenStaff;

    private static int playerChosenStaff;

    private static int chosenEnemy=1;
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
    public static int ChosenEnemy
    {
        get
        {
            return chosenEnemy;
        }
        set
        {
            chosenEnemy = value;
        }
    }
}
