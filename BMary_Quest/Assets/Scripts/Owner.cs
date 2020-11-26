using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Owner : MonoBehaviour
{
    public int xPos;
    public int yPos;
    public bool OwnedByMary = false;
    public bool OwnedByEnemy = false;
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
    public GameControl gameScript;
    public GameObject Spelplan;

    void Start()
    {
        tile = GetComponent<SpriteRenderer>();
        gc = GameObject.FindGameObjectWithTag("Player");
        gameScript = gc.GetComponent<GameControl>();

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
        if (Input.GetKey(KeyCode.LeftShift)) //todo  alternativ tänkt lösning, få den att gå på gamestate (vilket den ska göra till slut ändå där det beror på vilken spelare som är aktiv)
        {
            if (specialState == 2)
            {
                OwnedByEnemy = true;
                gameScript.enemyTempPoints++;
                tile.sprite = enemys;
                canChange = false;
            }

            //else check enemyNeighbours();
            //pseudokod inc...If it aint The enemys starting tile, check if any neighbours has a tile with Enemy Ownership. I.E if specialstate !2, then run CheckEnemyNeighbours()  som nedan. Special state is set in "spelplan" on init for mary.
            if (OwnedByMary != true && OwnedByEnemy != true /*och can change för fiende, nu funkar den för allt!*/)
            {
                OwnedByEnemy = true;
                gameScript.enemyTempPoints++;
                tile.sprite = enemys;
            }
        }
        else
        {
            if (specialState == 1)
            {
                OwnedByMary = true;
                gameScript.marysTempPoints++;
                tile.sprite = pc;
                canChange = false;
            }
            else
            {
                checkNeighbours();
                if (OwnedByMary != true && OwnedByEnemy != true && canChange == true)
                {
                    OwnedByMary = true;
                    gameScript.marysTempPoints++;
                    tile.sprite = pc;
                    canChange = false;
                }
            }
        }
    }

    //Currently we only want to check the closest neighbours in the X and Y-axis, 4 tiles. A nested for loop would be the thing if we're going to get all eight. 
    void checkNeighbours() 
    {
        //if playerturn =marys then temp var =ownedbyMary, if player turn =enemy then temp var = OwnedByEnemy
        if (xPos >= 0 && xPos < Spelplan.GetComponent<Spelplan>().gridArray.GetLength(0) - 1)
        {
            //Temp variabel Vi skulle kunna göra det som är OwnedbyMary till en variabel som går efter state!
            if (Spelplan.GetComponent<Spelplan>().gridArray[xPos + 1, yPos].GetComponent<Owner>().OwnedByMary == true) 
            {
                canChange = true;
                return;
            }
        }

        if (xPos > 0 && xPos < Spelplan.GetComponent<Spelplan>().gridArray.GetLength(0))
        {
            if (Spelplan.GetComponent<Spelplan>().gridArray[xPos - 1, yPos].GetComponent<Owner>().OwnedByMary == true)
            {
                canChange = true;
                return;
            }
        }

        if (yPos >= 0 && yPos < Spelplan.GetComponent<Spelplan>().gridArray.GetLength(1) - 1)
        {
            if (Spelplan.GetComponent<Spelplan>().gridArray[xPos, yPos + 1].GetComponent<Owner>().OwnedByMary == true)
            {
                canChange = true;
                return;
            }
        }

        if (yPos > 0 && yPos < Spelplan.GetComponent<Spelplan>().gridArray.GetLength(1))
        {
            if (Spelplan.GetComponent<Spelplan>().gridArray[xPos, yPos - 1].GetComponent<Owner>().OwnedByMary == true)
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
    void resetBoard() //Todo, maybe reset the scene instead? Lägga till fienderensning
    {
        OwnedByEnemy = false;
        OwnedByMary = false;
        tile.sprite = neutral;
        gameScript.marysTempPoints = 0;
        gameScript.enemyTempPoints = 0;
        Spelplan.GetComponent<Spelplan>().gridArray[0, 2].GetComponent<Owner>().canChange = true;
    }

    public void resetMary() //Todo, when you use your tiles, reset your tiles, subtract the number and eventual modifiers from the enemy, et cetera...
    {
        OwnedByMary = false;
        tile.sprite = neutral;
    }
    public void resetEnemy() //Todo...
    {
        OwnedByEnemy = false;
        tile.sprite = neutral;
    }
}
