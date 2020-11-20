using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Owner : MonoBehaviour
{
    public bool OwnedByMary = false;
    public bool OwnedByEnemy = false;
    public int specialState;

    SpriteRenderer tile; 
    public Sprite pc;
    public Sprite enemys;
    public Sprite neutral;
    public GameObject gc;
    public GameControl gameScript;
    // Start is called before the first frame update
    void Start()
    {
    tile = GetComponent<SpriteRenderer>();
    gc = GameObject.FindGameObjectWithTag ("Player");
    gameScript= gc.GetComponent<GameControl>();  

    pc = Resources.Load<Sprite>("Sprites/Marys");
    enemys = Resources.Load<Sprite>("Sprites/enemy");
    neutral = Resources.Load<Sprite>("Sprites/neutral");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
        resetBoard();
        }

    }

    private void OnMouseDown()
    {
        if (Input.GetKey(KeyCode.C)) //todo  alternativ tänkt lösning, få den att gå på gamestate (vilket den ska göra till slut ändå där det beror på vilken spelare som är aktiv)
        {
            OwnedByEnemy = true;
            gameScript.enemyTempPoints++;
            tile.sprite = enemys;
        }
        else
        {
            if(OwnedByMary !=true)
            {
            OwnedByMary = true;
            gameScript.marysTempPoints++;
            tile.sprite = pc;
            }

        }

    }
     void resetBoard()
    {
        OwnedByEnemy = false;
        OwnedByMary = false;
        tile.sprite = neutral;
        gameScript.marysTempPoints=0;
        gameScript.enemyTempPoints=0;
    }
    void resetMary()
    {
        OwnedByMary = false;
    }
    void resetEnemy()
    {
        OwnedByEnemy = false;
    }
}
