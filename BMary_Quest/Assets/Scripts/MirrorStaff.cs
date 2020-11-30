using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorStaff : MonoBehaviour
{
    int bloodPoints = 5;
    private GameControl gameControl;


    void Start()
    {
       gameControl.marysMaxHealth += bloodPoints; 
    }

    void Update()
    {
        
    }
}
