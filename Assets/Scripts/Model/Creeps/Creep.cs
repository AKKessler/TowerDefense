using UnityEngine;

public class Creep : MonoBehaviour {
    
    public float health;

    private void Start()
    {
        Vector3 spawnPoint = WaypointUtility.getWaypoint(0).position;
        spawnPoint.y = 0f;
        float yOffset = spawnPoint.y - GetComponent<MeshRenderer>().bounds.min.y;
        transform.position = spawnPoint + (Vector3.up * yOffset);
    }
}
