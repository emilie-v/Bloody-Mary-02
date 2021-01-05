using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private GameObject tilePrefab1;
    [SerializeField] private GameObject tilePrefab2;

    [SerializeField] private int rows = 5;
    [SerializeField] private int cols = 5;
    private float tileSize = 1;

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

                float posX = col * tileSize - cols / 2;
                float posY = row * -tileSize + rows / 2;

                tile.transform.position = new Vector2(posX + transform.position.x, posY + transform.position.y);
            }
        }
    }
}
