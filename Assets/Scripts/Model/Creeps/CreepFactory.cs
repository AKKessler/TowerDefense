using UnityEngine;

public class CreepFactory {

    public readonly static GameObject BASIC_CREEP_PREFAB = (GameObject) Resources.Load("Prefabs/BasicCreep");
    public readonly static GameObject FAST_CREEP_PREFAB = (GameObject) Resources.Load("Prefabs/FastCreep");
    
    public static GameObject createCreep(CreepType type, Transform spawn, Transform destination)
    {
        GameObject prefab = null;
        switch (type)
        {
            case CreepType.Basic:
                prefab = BASIC_CREEP_PREFAB;
                break;
            case CreepType.Fast:
                prefab = FAST_CREEP_PREFAB;
                break;
        }
        GameObject creep = Object.Instantiate(prefab, spawn.position, Quaternion.identity) as GameObject;
        NavMeshAgent agent = creep.GetComponent<NavMeshAgent>();
        agent.SetDestination(destination.position);
        return creep;
    }
}
