using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boardpiece : MonoBehaviour
{
    SpriteRenderer piece;
    public Sprite mary;
    public Sprite enemys;
    public Sprite nothing;
    // Start is called before the first frame update
    void Start()
    {
    piece= GetComponent<SpriteRenderer>();
    mary = Resources.Load<Sprite>("Sprites/Mark_BloodyMary");
    enemys = Resources.Load<Sprite>("Sprites/Mark_Lucifer");  
    }

    // Update is called once per frame
    void Update()
    {
        if(GetComponentInParent<Owner>().OwnedByMary!=true && GetComponentInParent<Owner>().OwnedByEnemy!=true) 
       piece.sprite=nothing;

       if(GetComponentInParent<Owner>().OwnedByMary==true) 
       piece.sprite=mary;

       if(GetComponentInParent<Owner>().OwnedByEnemy==true)
       piece.sprite=enemys;
    }
}
