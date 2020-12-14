using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Boardpiece : MonoBehaviour
{
    SpriteRenderer piece;
    public Sprite mary;
    public Sprite enemys;
    public Sprite nothing;

    Sprite enemyStarter;
    Sprite maryStarter;

    Sprite locked;

    private SpriteRenderer tile;

    private GameControl gameControl;

    // Start is called before the first frame update
    void Start()
    {
        piece = GetComponent<SpriteRenderer>();
        mary = Resources.Load<Sprite>("Sprites/Mark_BloodyMary");
        enemys = Resources.Load<Sprite>("Sprites/Mark_Lucifer");

        enemyStarter = Resources.Load<Sprite>("Sprites/Marks/Mark_Lucifer_Start");
        maryStarter = Resources.Load<Sprite>("Sprites/Marks/Mark_BloodyMary_Start");

        locked = Resources.Load<Sprite>("Sprites/Marks/Lucifer_staff_ability");
    }

    // Update is called once per frame
    public void Update()
    {
        if (GetComponentInParent<Owner>().owned == (int)Tile_State.empty)
            piece.sprite = nothing;

        if (GetComponentInParent<Owner>().owned == (int)Tile_State.player1)
            piece.sprite = mary;

        if (GetComponentInParent<Owner>().owned == (int)Tile_State.player2)
            piece.sprite = enemys;

        if (GetComponentInParent<Owner>().specialState == 1)
            piece.sprite = maryStarter;

        if (GetComponentInParent<Owner>().specialState == 2)
            piece.sprite = enemyStarter;

        if (GetComponentInParent<Owner>().locked)
            piece.sprite = locked;
    }
}
