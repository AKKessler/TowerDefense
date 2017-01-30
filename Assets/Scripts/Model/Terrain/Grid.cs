using UnityEngine;
using System.Collections.Generic;

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
                tile.row = i;
                tile.col = j;
                tiles[i, j] = tile;
            }
        }
    }

    // This function does not assume can build object at given row,col pair.
    public bool setObjectAt(GameObject gameObject, int row, int col)
    {
        Building building = gameObject.GetComponent<Building>();
        if (building == null) return false;

        List<Tile> tilesToModify = new List<Tile>();
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
            Tile t = tiles[row, col];
            bool walkable = t.getIsWalkable();
            GameObject g = t.getGameObject();
            if(g != null && !g.GetComponent<Building>().walkable)
            {
                walkable = false;
            }
            return walkable;
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

    public int[,] calculateCostMaps()
    {
        Transform[] waypoints = WaypointUtility.getWaypoints();
        List<int[,]> costList = new List<int[,]>();
        foreach (Transform waypoint in waypoints)
        {
            int[,] costMap = initializeNewCostMap();

            Vector3 endPoint = waypoint.position; 
            int endRow = Mathf.FloorToInt(endPoint.x);
            int endCol = Mathf.FloorToInt(endPoint.z);

            List<Tile> visitQueue = new List<Tile>();
            visitQueue.Add(tiles[endRow, endCol]); // start BFS from goal tile
            costMap[endRow, endCol] = 0; // goal has distance cost 0

            while (visitQueue.Count > 0)
            {
                Tile currTile = visitQueue[0];
                int currCost = costMap[currTile.row, currTile.col];
                visitQueue.RemoveAt(0);
                foreach (Tile adjTile in getAdjacentTiles(currTile.row, currTile.col))
                {
                    int nextCost = costMap[adjTile.row, adjTile.col];
                    if (nextCost == -1 || nextCost > currCost + 1)
                    {
                        costMap[adjTile.row, adjTile.col] = currCost + 1;
                        visitQueue.Add(adjTile);
                    }
                }
            }
            costList.Add(costMap);
        }
        
        for (int w = 0; w < costList.Count; w++)
        {
            // TODO figure out how to print to stdout since Unity console is not in a monospace font, 
            Debug.Log("Waypoint" + w + "'s cost map:\n" + costMapToString(costList[w]));
        }
        return null; // TODO determine how costMaps will be accessed;
    }

    private int[,] initializeNewCostMap()
    {
        int[,] map = new int[rows, cols];
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                map[i, j] = -1;
            }
        }
        return map;
    }

    private List<Tile> getAdjacentTiles(int row, int col) {
        List<Tile> adjacentTiles = new List<Tile>();
        int[,] coords = { 
            {row+1, col}, 
            {row-1, col}, 
            {row, col+1}, 
            {row, col-1}
        };
        for(int i=0; i<coords.GetLength(0); i++)
        {
            for(int j=0; j<coords.GetLength(1); j++)
            {
                int r = coords[i, 0];
                int c = coords[i, 1];
                if(canWalkAt(r,c))
                {
                    adjacentTiles.Add(tiles[r, c]);
                }
            }
        }
        return adjacentTiles;
    }

    private string costMapToString(int[,] map)
    {
        string output = "";
        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {
                int cost = map[i, j];
                output += cost + "  ";
                if (cost < 10 && cost != -1)
                {
                    output += " ";
                }
            }
            output += "\n";
        }
        return output;
    }
}
