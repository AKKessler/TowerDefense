using UnityEngine;
using System.Collections;

public class Grid {

    public int rows;
    public int cols;
    public float width;
    public float length;

    private Tile[,] tiles;

    public Grid(int rows, int cols, float width, float length)
    {
        this.rows = rows;
        this.cols = cols;
        this.width = width;
        this.length = length;

        initializeTiles();
    }

    private void initializeTiles()
    {
        tiles = new Tile[rows, cols];

        bool isBuildable = true;
        bool isWalkable = true;

        float tileWidth = width / cols;
        float tileLength = length / rows;
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {

                float x = i * tileLength;
                float z = (j+1) * tileWidth;
                Tile tile = new Tile(x, z, tileWidth, tileLength, isBuildable, isWalkable);
                tiles[i, j] = tile;
            }
        }
    }

    public void setObjectAt(GameObject gameObject, int row, int col)
    {
        if (areValidCoords(row, col))
        {
            Tile tile = tiles[row, col];
            tile.setGameObject(gameObject);
        }
    }

    public bool setObjectAt2(Building building, int row, int col)
    {
        GameObject gameObject = building.gameObject;
        ArrayList tilesToModify = new ArrayList();
        for (int i = 0; i < building.length; i++)
        {
            for (int j = 0; j < building.width; j++)
            {
                if (canBuildAt(row + i, col + j))
                {
                    tilesToModify.Add(tiles[row + i, col + j]);
                }
                else
                {
                    return false;
                }
            }
        }
    
        Vector3 centerAverage = Vector3.zero;
        foreach (Tile tile in tilesToModify)
        {
            centerAverage += tile.getCenter3();
            tile.setGameObject(gameObject);
        }
        float towerArea = building.length * building.width;
        gameObject.transform.position = centerAverage / towerArea;
        return true;
    }

    // will return null if no GameObject at space
    public GameObject getObjectAt(int row, int col)
    {
        return areValidCoords(row, col) ? tiles[row, col].getGameObject() : null;
    }

    public bool canBuildAt(int row, int col)
    {
        if(areValidCoords(row, col))
        {
            return tiles[row, col].getIsBuildable() && tiles[row, col].getGameObject() == null;
        }
        return false;
    }

    public bool canWalkAt(int row, int col)
    {
        if(areValidCoords(row, col))
        {
            return tiles[row, col].getIsWalkable();
        }
        return false;
    }

    public bool areValidCoords(int row, int col)
    {
        bool validRow = (0 <= row) && (row < rows);
        bool validCol = (0 <= col) && (col < cols);
        return validRow && validCol;
    }
    
    public Vector3 getCenterAt(int row, int col)
    {
        if (areValidCoords(row, col))
        {
            return tiles[row, col].getCenter3();
        }
        return Vector3.zero;
    }
}
