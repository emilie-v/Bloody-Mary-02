using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    public int marysTempPoints;

    private int Char;
    public int enemyTempPoints;
    // Start is called before the first frame update
    void Start()
    {
 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
         {
         Debug.Log("Mary äger " + marysTempPoints + " Brickor");
         Debug.Log("Fienden äger " + enemyTempPoints + " Brickor");
         } 



    void ResetScore(int Char)
    {
        if(Char==0)
        {
        marysTempPoints=0;
        }
            else if (Char==1)
            {
            enemyTempPoints=0;
            }
                else
                {
                Debug.Log("This should not happen!");
                }

    }

       /* if (Input.GetKeyDown(KeyCode.E))
        {
            GameObject temp = GameObject.Find("Spelplan");
            if(temp.GetComponentInChildren<Owner>().OwnedByMary ==true)
            {
                Maryspoints++;
            }
           
        } */
        
            
    }

}
