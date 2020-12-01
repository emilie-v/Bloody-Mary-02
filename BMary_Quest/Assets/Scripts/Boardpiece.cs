using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boardpiece : MonoBehaviour
{
    SpriteRenderer piece;
    public Sprite mary;
    public Sprite enemys;
    public Sprite nothing;

    private SpriteRenderer tile;

    private GameControl gameControl;
    // Start is called before the first frame update
    void Start()
    {
    piece = GetComponent<SpriteRenderer>();
    mary = Resources.Load<Sprite>("Sprites/Mark_BloodyMary");
    enemys = Resources.Load<Sprite>("Sprites/Mark_Lucifer");  
    }

    // Update is called once per frame
    public void Update()
    {
        if(GetComponentInParent<Owner>().owned == (int)Tile_State.Empty) 
       piece.sprite=nothing;

       if(GetComponentInParent<Owner>().owned == (int)Tile_State.player1) 
       piece.sprite=mary;

       if(GetComponentInParent<Owner>().owned == (int)Tile_State.player2)
       piece.sprite=enemys;
    }
}
