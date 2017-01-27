using UnityEngine;
using System.Collections.Generic;

public class WaveManager {

    Transform start, finish;

    List<GameObject> spawnedCreeps;

    CreepType currentCreepType;

    bool waveInProgress;

    public WaveManager(Transform start, Transform finish)
    {
        spawnedCreeps = new List<GameObject>();
        this.start = start;
        this.finish = finish;

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
        spawnedCreeps.Add(CreepFactory.createCreep(currentCreepType, start, finish));
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
