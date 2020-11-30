using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Owner : MonoBehaviour
{
    public int xPos;
    public int yPos;

    public int owned;
    //public bool OwnedByMary = false;
    //public bool OwnedByEnemy = false;
    public bool canChange = false;
    public int specialState;
    /* 
    potentiella states: 1: startbricka för Mary, kan alltid vara hennes, aldrig fiendens, 2: tvärtom mot 1, 3: ett state som t.ex. en stav kan lägga på, gör så att poäng dubblas 4: mer stavspecial. Kanske ska ha separata variabler för stavgrejer. 
    */

    SpriteRenderer tile;
    public Sprite pc;
    public Sprite enemys;
    public Sprite neutral;
    public GameObject gc;
    public GameControl gameControl;
    public GameObject Spelplan;

    void Start()
    {
        tile = GetComponent<SpriteRenderer>();
        gc = GameObject.FindGameObjectWithTag("Player");
        gameControl = gc.GetComponent<GameControl>();

        pc = Resources.Load<Sprite>("Sprites/Mark_BloodyMary");
        enemys = Resources.Load<Sprite>("Sprites/Mark_Lucifer");
        neutral = Resources.Load<Sprite>("Sprites/Board_Tile");
        Spelplan = GameObject.FindGameObjectWithTag("Spelplan");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            resetBoard();
        }
    }

    private void OnMouseDown()
    {
        //spelare 2(enemy) controller
        if (gameControl.playerTurn == 1) //todo  alternativ tänkt lösning, få den att gå på gamestate (vilket den ska göra till slut ändå där det beror på vilken spelare som är aktiv)
        {
            if (specialState == 2)
            {
                owned = (int)Tile_State.player2;
                gameControl.enemyTempPoints++;
                canChange = false;
            }
            else
            {
                checkNeighbours();
                if (owned == 0 && canChange)
                {
                    owned = (int)Tile_State.player2;
                    gameControl.enemyTempPoints++;
                    canChange = false;
                }
            }
        }
        else if (gameControl.playerTurn == 0)
        {
            if (specialState == 1)
            {
                owned = (int)Tile_State.player1;
                gameControl.marysTempPoints++;
                canChange = false;
            }
            else
            {
                checkNeighbours();
                if (owned == 0 && canChange)
                {
                    owned = (int)Tile_State.player1;
                    gameControl.marysTempPoints++;
                    canChange = false;
                }
            }
        }
        else
        {
            Debug.Log("Turn Order Error");
        }
    }

    //Currently we only want to check the closest neighbours in the X and Y-axis, 4 tiles. A nested for loop would be the thing if we're going to get all eight. 
    void checkNeighbours()
    {
        //if playerturn =marys then temp var =ownedbyMary, if player turn =enemy then temp var = OwnedByEnemy
        if (xPos >= 0 && xPos < Spelplan.GetComponent<Spelplan>().gridArray.GetLength(0) - 1)
        {
            //Temp variabel Vi skulle kunna göra det som är OwnedbyMary till en variabel som går efter state!
            if (Spelplan.GetComponent<Spelplan>().gridArray[xPos + 1, yPos].GetComponent<Owner>().owned == gameControl.playerTurn + 1)
            {
                canChange = true;
                return;
            }
        }

        if (xPos > 0 && xPos < Spelplan.GetComponent<Spelplan>().gridArray.GetLength(0))
        {
            if (Spelplan.GetComponent<Spelplan>().gridArray[xPos - 1, yPos].GetComponent<Owner>().owned == gameControl.playerTurn + 1)
            {
                canChange = true;
                return;
            }
        }

        if (yPos >= 0 && yPos < Spelplan.GetComponent<Spelplan>().gridArray.GetLength(1) - 1)
        {
            if (Spelplan.GetComponent<Spelplan>().gridArray[xPos, yPos + 1].GetComponent<Owner>().owned == gameControl.playerTurn + 1)
            {
                canChange = true;
                return;
            }
        }

        if (yPos > 0 && yPos < Spelplan.GetComponent<Spelplan>().gridArray.GetLength(1))
        {
            if (Spelplan.GetComponent<Spelplan>().gridArray[xPos, yPos - 1].GetComponent<Owner>().owned == gameControl.playerTurn + 1)
            {
                canChange = true;
                return;
            }
        }
        else
        {
            Debug.Log("You can't make that move");
        }
    }

    //reset methods
    public void resetBoard() //Todo, maybe reset the scene instead? Lägga till fienderensning
    {
        owned = 0;
        tile.sprite = neutral;
        gameControl.marysTempPoints = 0;
        gameControl.enemyTempPoints = 0;
    }

    public void resetMary() //Todo, when you use your tiles, reset your tiles, subtract the number and eventual modifiers from the enemy, et cetera...
    {
        if (owned == (int)Tile_State.player1)
        {
            owned = 0;
        }
    }
    public void resetEnemy() //Todo...
    {
        if (owned == (int)Tile_State.player2)
        {
            owned = 0;
        }
    }
}

public enum Tile_State : int
{
    Empty, // = 0
    player1, // = 1
    player2 // = 2
}
