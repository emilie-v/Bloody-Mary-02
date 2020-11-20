using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] GameObject tilePrefab1 = null;
    [SerializeField] GameObject tilePrefab2 = null;

    [SerializeField] int rows = 5;
    [SerializeField] int cols = 5;
    private float tileSize = 1;

    // Start is called before the first frame update
    void Start()
    {
        GenerateGrid();
    }

    private void GenerateGrid()
    {

        GameObject tile = tilePrefab1;

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                if (row % 2 == 0)
                {
                    if (col % 2 == 0)
                        tile = Instantiate(tilePrefab1, transform);

                    else
                        tile = Instantiate(tilePrefab2, transform);
                }

                else
                {
                    if (col % 2 == 0)
                        tile = Instantiate(tilePrefab2, transform);

                    else
                        tile = Instantiate(tilePrefab1, transform);
                }

                /*  GameObject tile = Instantiate(tilePrefab1, transform);  this line can be commented if we don't want checkerboard. Then comment away 30 - 46 */

                float posX = col * tileSize - cols / 2;
                float posY = row * -tileSize + rows / 2;

                tile.transform.position = new Vector2(posX + transform.position.x, posY + transform.position.y);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
