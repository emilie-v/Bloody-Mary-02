using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spelplan : MonoBehaviour
{
    public int width = 5;
    public int height = 5;
    private float size = 1.22f;

    public GameObject[,] gridArray;

    private int maryStartX = 0;
    private int maryStartY = 2;

    private int enemyStartX = 4;
    private int enemyStartY = 2;

    void Awake()
    {
        GameObject woodsquare = (GameObject)Instantiate(Resources.Load("Prefabs/Board_Tile"));
        GameObject temp = GameObject.Find("Spelplan");
        gridArray = new GameObject[width, height];

        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                gridArray[x, y] = Instantiate(woodsquare, temp.transform);

                if (x == maryStartX && y == maryStartY)
                {
                    gridArray[x, y].GetComponent<Owner>().specialState = 1;
                    gridArray[x, y].GetComponent<Owner>().owned = 1;
                }

                if (x == enemyStartX && y == enemyStartY)
                {
                    gridArray[x, y].GetComponent<Owner>().specialState = 2;
                    gridArray[x, y].GetComponent<Owner>().owned = 2;
                }

                gridArray[x, y].GetComponent<Owner>().xPos = x;
                gridArray[x, y].GetComponent<Owner>().yPos = y;
                float posX = x * size;
                float posY = y * -size;

                gridArray[x, y].transform.position += new Vector3(posX, posY, 0);
            }
        }
        Destroy(woodsquare);
    }
}