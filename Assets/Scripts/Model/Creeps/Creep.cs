using UnityEngine;

public class Creep : MonoBehaviour {
    
    public float health;

    private void Start()
    {
        transform.position = WaypointUtility.getWaypoint(0).position;
        float yOffset = transform.position.y - GetComponent<MeshRenderer>().bounds.min.y;
        transform.position += Vector3.up * yOffset;
    }
}
