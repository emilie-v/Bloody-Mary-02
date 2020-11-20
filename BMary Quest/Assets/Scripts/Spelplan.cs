using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spelplan: MonoBehaviour
{
    private int width;
    private int height;
    public int[,] gridArray;
    private float size =1;
    Spelplan spelplan;
        void Start()
    {
       Spelplan spelplan = new Spelplan(5, 5); 
    }

    public Spelplan(int width, int height)
    {
        this.width = width;
        this.height = height;
        gridArray = new int[width, height];
        GameObject woodsquare = (GameObject)Instantiate(Resources.Load("Standardboard"));
        GameObject temp = GameObject.Find("Spelplan");

        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                GameObject board = Instantiate(woodsquare, temp.transform);

                float posX = x * size;
                float posY = y * -size;

                board.transform.position += new Vector3(posX, posY,0);
                //Debug.Log(board.transform.position);
             
            }

        }

        Destroy(woodsquare);

    }
    

    
}
