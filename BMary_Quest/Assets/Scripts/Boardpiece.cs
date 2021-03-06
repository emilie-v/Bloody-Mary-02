﻿using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Boardpiece : MonoBehaviour
{
    private SpriteRenderer piece;
    private SpriteRenderer markedPiece;
    public Sprite mary;
    public Sprite enemys;
    public Sprite nothing;

    private Sprite enemyStarter;
    private Sprite maryStarter;

    private Sprite locked;
    private Sprite skeletonMark;

    private SpriteRenderer tile;

    private GameControl gameControl;

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
            enemyStarter = Resources.Load<Sprite>("Sprites/Marks/Mark_SenorBones_Start");
            enemys = Resources.Load<Sprite>("Sprites/Marks/Mark_SenorBones");
        }
        else if (DataAcrossScenes.EnemyChosenStaff == (int)Chosen_Staff.moon)
        {
            enemyStarter = Resources.Load<Sprite>("Sprites/Marks/Mark_Umbralina_Start");
            enemys = Resources.Load<Sprite>("Sprites/Marks/Mark_Umbralina");
        }
        else if (DataAcrossScenes.EnemyChosenStaff == (int)Chosen_Staff.night)
        {
            enemyStarter = Resources.Load<Sprite>("Sprites/Marks/Mark_Count_Start");
            enemys = Resources.Load<Sprite>("Sprites/Marks/Mark_Count");
        }
        else if (DataAcrossScenes.EnemyChosenStaff == (int)Chosen_Staff.hell)
        {
            enemyStarter = Resources.Load<Sprite>("Sprites/Marks/Mark_Lucifer_Start");
            enemys = Resources.Load<Sprite>("Sprites/Marks/Mark_Lucifer");
        }

        locked = Resources.Load<Sprite>("Sprites/Marks/Count_staff_ability_(Sowilo)");
        skeletonMark = Resources.Load<Sprite>("Sprites/Marks/SenorBones_Staff_Ability");
    }

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
            markedPiece.sprite = skeletonMark;
        
        if (!GetComponentInParent<Owner>().skeletonMark && GetComponentInParent<Owner>().specialState == 0)
            markedPiece.sprite = null;
    }
}
