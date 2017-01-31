using UnityEngine;
using System.Collections.Generic;

public class WaypointUtility {

    public readonly static GameObject WAYPOINT_PREFAB = (GameObject) Resources.Load("Prefabs/Waypoint");

    private static Transform[] waypoints;

    private static List<int[,]> costMaps;

    private static Grid grid;

    public static Transform[] getWaypoints()
    {
        return waypoints;
    }

    public static Transform getWaypoint(int index)
    {
        return waypoints[index];
    }

    public static int waypointCount()
    {
        return waypoints.GetLength(0);
    }
    
    public static void setWaypoints(Transform[] waypoints)
    {
        WaypointUtility.waypoints = waypoints;
    }

    public static void updateCostMaps(Grid grid)
    {
        WaypointUtility.grid = grid; // maintain reference of grid costMap was built upon (used for getting tile coordinates)
        costMaps = new List<int[,]>();
        foreach(Transform waypoint in waypoints)
        {
            int row = Mathf.FloorToInt(waypoint.position.x);
            int col = Mathf.FloorToInt(waypoint.position.z);
            int[,] costMap = grid.calculateCostMap(row, col);
            costMaps.Add(costMap);
        }
        GameObject.Find("Creeps").BroadcastMessage("updatePath", SendMessageOptions.DontRequireReceiver);
    }

    public static List<Vector3> getShortestPath(Transform transform, int currentWaypoint)
    {
        int[,] costMap = costMaps[currentWaypoint];
        List<Vector3> pathPoints = traverseCostMap(costMap, transform.position, waypoints[currentWaypoint].position);
        return pathPoints;
    }

    private static List<Vector3> traverseCostMap(int[,] costMap, Vector3 start, Vector3 goal)
    {
        int currRow = Mathf.FloorToInt(start.x);
        int currCol = Mathf.FloorToInt(start.z);
        int goalRow = Mathf.FloorToInt(goal.x);
        int goalCol = Mathf.FloorToInt(goal.z);
        List<Vector3> pathPoints = new List<Vector3>();
        while (currRow != goalRow || currCol != goalCol)
        {
            int[,] coords =
            {
                { currRow +1, currCol},
                { currRow -1, currCol},
                { currRow, currCol +1},
                { currRow, currCol -1}
            };
            int minIndex = -1;
            int minCost = 1337;
            for (int i = 0; i < coords.GetLength(0); i++)
            {
                int r = coords[i, 0];
                int c = coords[i, 1];
                if (!grid.areValidCoords(r, c)) continue;

                int cost = costMap[r,c];
                // TODO Consider adding random chance if costs to traverse either direction if costs are equal.
                if (minCost > cost && cost != -1)  // TODO remove cost != -1 once default value of costMap is infinty (vs. -1)
                {
                    minCost = cost;
                    minIndex = i;
                }
            }
            currRow = coords[minIndex, 0];
            currCol = coords[minIndex, 1];
            pathPoints.Add(grid.getCenterAt(currRow, currCol));
        }
        return pathPoints;
    }
}
