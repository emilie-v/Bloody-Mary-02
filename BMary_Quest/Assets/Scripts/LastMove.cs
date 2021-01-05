using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastMove : MonoBehaviour
{
    public bool staffUsed;
    public bool enemyCashedOutThisTurn;
    public bool maryCashedOutThisTurn;

    public bool enemyHellStaffActivePower;
    public bool playerHellStaffActivePower;

    public int[] lastMovesX = new int[5] {6,6,6,6,6};
    public int[] lastMovesY = new int[5] {6,6,6,6,6};

   public void resetArray()
   {
       lastMovesX = new int[5] {6,6,6,6,6};
       lastMovesY = new int[5] {6,6,6,6,6};  
   }
}
