using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class StartRandomizer : MonoBehaviour
{
    public bool gameStart;
    public GameControl gameControl;

    private void Start()
    {
        StartCoroutine(TurnRandomizer());
    }

    private IEnumerator TurnRandomizer()
    {
        yield return new WaitForSeconds(0);
        for (int i = 0; i < Random.Range(10, 20); i++)
        {
            yield return new WaitForSeconds(0.05f * i);
            if (gameControl.playerTurn == (int)Player_Turn.mary)
            {
                gameControl.playerTurn = (int) Player_Turn.enemy;
            }
            else if (gameControl.playerTurn == (int) Player_Turn.enemy)
            {
                gameControl.playerTurn = (int) Player_Turn.mary;
            }
            
            gameControl.CharacterScaling();
            gameControl.CharacterDarkening();
        }
        
        gameControl.TurnStart();
        gameStart = true;
    }
}
