using UnityEngine;
using System.Collections.Generic;

public class Creep : MonoBehaviour {
    
    public int currentWaypoint;

    public float health;

    public float speed;

    private List<Vector3> path; // get this as a queue/something with .pop() method?

    private Vector3 dest;

    private void Start()
    {
        currentWaypoint = 1;
        transform.position = WaypointUtility.getWaypoint(0).position;
        path = WaypointUtility.getShortestPath(transform, currentWaypoint);
        dest = path[0];
        path.RemoveAt(0);
    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, dest);
        if(distance < 0.1f)
        {
            if(path.Count == 0)
            {
                if(++currentWaypoint >= WaypointUtility.waypointCount())
                {
                    Destroy(gameObject); // TODO Consider handling destroy logic elsewhere? Collider trigger?
                    return; // TODO does code stop execution upon Destroy? Putting return statement to be safe...
                }
                path = WaypointUtility.getShortestPath(transform, currentWaypoint);
            }
            dest = path[0];
            path.RemoveAt(0);
        }
        transform.position = Vector3.Slerp(transform.position, dest, Time.deltaTime * speed);
    }

    public void updatePath()
    {
        path = WaypointUtility.getShortestPath(transform, currentWaypoint);
        dest = path[0];
        path.RemoveAt(0);
    }
}
