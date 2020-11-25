using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int maxBlood = 20;
    public int currentBlood;

    private void Start()
    {
        currentBlood = maxBlood;
    }

    private void Update()
    {
        //if den andra checkar ut sina brickor, förlorar denna spelare blood points
    }

    void loseBlood(int blood)
    {
        currentBlood -= blood;
    }
}
