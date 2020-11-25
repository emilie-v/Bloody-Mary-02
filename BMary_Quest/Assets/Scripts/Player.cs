using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    Text bloodCount;
    public int maxBlood = 20;
    public int currentBlood = 20;

    private void Start()
    {
        bloodCount = GetComponent<Text>();
    }

    private void Update()
    {
        bloodCount.text = currentBlood.ToString();

        //testknapp för att ta ner hp
        if(Input.GetKeyDown(KeyCode.X))
        {
            currentBlood--;
        }

        //if den andra checkar ut sina brickor, förlorar denna spelare blood points (loseblood)
    }

    void loseBlood(int blood)
    {
        currentBlood -= blood;
    }
}
