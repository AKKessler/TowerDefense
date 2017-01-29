using UnityEngine;
using System.Collections.Generic;

public class WaveManager {
    
    List<GameObject> spawnedCreeps;

    CreepType currentCreepType;

    bool waveInProgress;

    public WaveManager()
    {
        spawnedCreeps = new List<GameObject>();
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
        creep.GetComponent<Creep>().setWaypoints(WaypointUtility.getWaypoints());
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
