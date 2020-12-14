using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastMove : MonoBehaviour
{
    public bool staffUsed = false;
    public bool enemyCashedOutThisTurn = false;
    public bool maryCashedOutThisTurn = false;

    public bool enemyHellStaffActivePower = false;
    public bool playerHellStaffActivePower = false;

    public bool enemyDarkNightStaffActivePower = false;
    public bool playerDarkNightStaffActivePower = false;

    public int[] lastMovesX = new int[5] {6,6,6,6,6};
    public int[] lastMovesY = new int[5] {6,6,6,6,6};

   public void resetArray()
   {
       Debug.Log("ResetArray körs");
    lastMovesX = new int[5] {6,6,6,6,6};
    lastMovesY = new int[5] {6,6,6,6,6};  
   }
}
