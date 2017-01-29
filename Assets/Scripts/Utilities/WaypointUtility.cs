using UnityEngine;

public class WaypointUtility {
    public readonly static GameObject WAYPOINT_PREFAB = (GameObject) Resources.Load("Prefabs/Waypoint");

    private static Transform[] waypoints;

    public static Transform[] getWaypoints()
    {
        return waypoints;
    }
    
    public static void setWaypoints(Transform[] waypoints)
    {
        WaypointUtility.waypoints = waypoints;
    }

}
