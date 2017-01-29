using UnityEngine;

public class Creep : MonoBehaviour {

    public Transform[] waypoints;

    public int currentWaypoint;

    public float health;

    private NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, waypoints[currentWaypoint].position);
        // some type of race condition makes this true when it's not?
        //if(agent.remainingDistance <= agent.stoppingDistance) 
        if (distance < 0.5f)
        {
            currentWaypoint++;
            if (currentWaypoint == waypoints.Length)
            {
                Destroy(gameObject);
            }
            else
            {
                // is there a difference between these two ways?
                //agent.SetDestination(waypoints[currentWaypoint].position);
                agent.destination = waypoints[currentWaypoint].position;

            }
        }
    }

    // Currently is an issue where local agent variable is not yet set? 
    // Meaning this is called before Start() function.
    public void setWaypoints(Transform[] waypoints)
    {
        if (waypoints.Length >= 2)
        {
            this.waypoints = waypoints;
            this.currentWaypoint = 0;
            NavMeshAgent agent = GetComponent<NavMeshAgent>();
            agent.Warp(this.waypoints[currentWaypoint].position);
        }
    }
}
