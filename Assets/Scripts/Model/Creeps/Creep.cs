using UnityEngine;
using System.Collections;

public abstract class Creep {
    
    GameObject creep;

    protected float health;

	public Creep(Object prefab, Transform spawn, Transform destination) {
        creep = Object.Instantiate(prefab, spawn.position, Quaternion.identity) as GameObject;
        setDestination(destination);
    }
	
    public void setDestination(Transform destination)
    {
        NavMeshAgent agent = creep.GetComponent<NavMeshAgent>();
        agent.SetDestination(destination.position);
    }
}
