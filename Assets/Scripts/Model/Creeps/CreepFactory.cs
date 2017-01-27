using UnityEngine;

public class CreepFactory {

    public static Creep createCreep(CreepType type, Transform spawn, Transform destination)
    {
        switch (type)
        {
            case CreepType.Basic:
                return new BasicCreep(spawn, destination);
            case CreepType.Fast:
                return new FastCreep(spawn, destination);
            default:
                return null;
        }
    }
}
