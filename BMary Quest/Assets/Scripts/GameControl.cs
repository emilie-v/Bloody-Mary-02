using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    public int marysTempPoints;

    private int Char;
    public int enemyTempPoints;
    public GameObject Spelplan;

    public int marysHealth; //kanske ska ha standardvärde + eventuell staff-modifier? Eller kommer hälsan med staven så att säga?
    public int enemyHealth;

    public int playerTurn;
   

    // Start is called before the first frame update
    void Start()
    {
        Spelplan= GameObject.FindGameObjectWithTag ("Spelplan");
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("spelplanens storlek i x-led " + Spelplan.GetComponent<Spelplan>().gridArray.GetLength(0)
        if (Input.GetKeyDown(KeyCode.E))
        {
         Debug.Log("Mary äger " + marysTempPoints + " Brickor");
         Debug.Log("Fienden äger " + enemyTempPoints + " Brickor");
         if (playerTurn==0)
            {
             playerTurn=1;
            }
         else
            {
             playerTurn=0;
            }
        } 

        if (Input.GetKeyDown(KeyCode.G)) //Testar med arrayen...update, onödig kod när väl arrayen nu fungerar, sparar den än så länge, whats the harm?
        for (int i = 0; i <Spelplan.GetComponent<Spelplan>().gridArray.GetLength(0); i++) //Null-pointer exeption?
        {
            for (int j = 0; j <Spelplan.GetComponent<Spelplan>().gridArray.GetLength(1); j++) 
            {
            
              if(Spelplan.GetComponent<Spelplan>().gridArray[i,j].GetComponent<Owner>().OwnedByMary==true)
              {
                Debug.Log("Hurra");
              } 
              else
              {
              Debug.Log("inget än");        
              }
            }
        }
        //if (Input.GetKeyDown(KeyCode.T)) //t för test
       // {
       //     Spelplan.GetComponent<Spelplan>().gridArray[2,3].GetComponent<Owner>().OwnedByMary=true; //funkar, nu får vi se om vi kan fixa det lokalt.
       // }
    }





    void ResetScore(int playerTurn)  //To(rea)Do(r).....no call for it yet, possibly the start of doing damage etc...
    {
        if(playerTurn==0)
        {
        marysTempPoints=0;
        }
            else if (playerTurn==1)
            {
            enemyTempPoints=0;
            }
                else
                {
                Debug.Log("This should not happen!");
                }

    }

        

}
