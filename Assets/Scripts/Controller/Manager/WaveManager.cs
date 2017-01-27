using UnityEngine;
using System.Collections.Generic;

public class WaveManager {

    Transform[] waypoints;

    List<GameObject> spawnedCreeps;

    CreepType currentCreepType;

    bool waveInProgress;

    public WaveManager(Transform[] waypoints)
    {
        spawnedCreeps = new List<GameObject>();
        this.waypoints = waypoints;

        waveInProgress = false;
        currentCreepType = CreepType.Basic;
    }

    public void startNextWave()
    {
        waveInProgress = true;
    }

    public bool isWaveInProgress()
    {
        return waveInProgress = spawnedCreeps.Count != 0;
    }

    public void spawnCreep()
    {
        GameObject creep = CreepFactory.createCreep(currentCreepType);
        creep.GetComponent<Creep>().setWaypoints(waypoints);
        spawnedCreeps.Add(creep);
    }

    public void nextWave()
    {
        if(currentCreepType == CreepType.Basic)
        {
            currentCreepType = CreepType.Fast;
        }
        else
        {
            currentCreepType = CreepType.Basic;
        }
    }
}
