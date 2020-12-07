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
    {//behöver ev inte ha?

        GameObject woodsquare = (GameObject)Instantiate(Resources.Load("Prefabs/Board_Tile"));
        GameObject temp = GameObject.Find("Spelplan");
        gridArray = new GameObject[width, height];

        //skapar gameobjects (squares)
        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                gridArray[x, y] = (GameObject)Instantiate(woodsquare, temp.transform);
                //Debug.Log(x + "," + y);

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


/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spelplan: MonoBehaviour
{
    public int width;
    public int height;
    public GameObject[,] gridArray;
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
        gridArray = new GameObject[width, height];
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
*/