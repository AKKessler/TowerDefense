using UnityEngine;
using System.Collections.Generic;

public class WaveManager {
    
    CreepType currentCreepType;

    bool waveInProgress;

    GameObject creepContainer;

    public WaveManager()
    {
        waveInProgress = false;
        currentCreepType = CreepType.Basic;
        creepContainer = GameObject.Find("Creeps");
    }

    public void startNextWave()
    {
        waveInProgress = true;
    }

    public bool isWaveInProgress()
    {
        return waveInProgress = creepContainer.GetComponents<Transform>().Length != 0; // TODO ensure array doesn't contain its own Transform 
    }

    public void spawnCreep()
    {
        GameObject creep = CreepFactory.createCreep(currentCreepType);
        creep.transform.parent = creepContainer.transform;
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
