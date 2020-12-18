using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Boardpiece : MonoBehaviour
{
    SpriteRenderer piece;
    SpriteRenderer markedPiece;
    public Sprite mary;
    public Sprite enemys;
    public Sprite nothing;

    Sprite enemyStarter;
    Sprite maryStarter;

    Sprite locked;
    Sprite skeletonMark;

    private SpriteRenderer tile;

    private GameControl gameControl;

    // Start is called before the first frame update
    void Start()
    {
        piece = GetComponent<SpriteRenderer>();
        markedPiece = transform.GetChild(0).GetComponent<SpriteRenderer>();
        mary = Resources.Load<Sprite>("Sprites/Mark_BloodyMary");
        maryStarter = Resources.Load<Sprite>("Sprites/Marks/Mark_BloodyMary_Start");

        if (DataAcrossScenes.EnemyChosenStaff == (int)Chosen_Staff.pumpkin)
        {
            enemyStarter = Resources.Load<Sprite>("Sprites/Marks/Mark_Ghastella_Start");
            enemys = Resources.Load<Sprite>("Sprites/Marks/Mark_Ghastella");
        }
        else if (DataAcrossScenes.EnemyChosenStaff == (int)Chosen_Staff.skeleton)
        {
            //TODO change when the senior bones marks has been added
            enemyStarter = Resources.Load<Sprite>("Sprites/Marks/Mark_Lucifer_Start");
            enemys = Resources.Load<Sprite>("Sprites/Marks/Mark_Lucifer");
        }
        else if (DataAcrossScenes.EnemyChosenStaff == (int)Chosen_Staff.moon)
        {
            //TODO change when the umbralina marks has been added
            enemyStarter = Resources.Load<Sprite>("Sprites/Marks/Mark_Lucifer_Start");
            enemys = Resources.Load<Sprite>("Sprites/Marks/Mark_Lucifer");
        }
        else if (DataAcrossScenes.EnemyChosenStaff == (int)Chosen_Staff.night)
        {
            //TODO change when the counts marks has been added
            enemyStarter = Resources.Load<Sprite>("Sprites/Marks/Mark_Count_Start");
            enemys = Resources.Load<Sprite>("Sprites/Marks/Mark_Count");
        }
        else if (DataAcrossScenes.EnemyChosenStaff == (int)Chosen_Staff.hell)
        {
            enemyStarter = Resources.Load<Sprite>("Sprites/Marks/Mark_Lucifer_Start");
            enemys = Resources.Load<Sprite>("Sprites/Marks/Mark_Lucifer");
        }

        skeletonMark = null;
        locked = Resources.Load<Sprite>("Sprites/Marks/Count_staff_ability_(Sowilo)");
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

        if (GetComponentInParent<Owner>().locked > 0)
            piece.sprite = locked;

        if (GetComponentInParent<Owner>().skeletonMark)
            markedPiece.sprite = locked;
        
        if (!GetComponentInParent<Owner>().skeletonMark)
            markedPiece.sprite = null;
    }
}
