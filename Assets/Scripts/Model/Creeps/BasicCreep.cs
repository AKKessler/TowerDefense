using UnityEngine;
using System.Collections;

public class BasicCreep : Creep {

    public static Object prefab = Resources.Load("Prefabs/BasicCreep");

    public BasicCreep(Transform start, Transform destination) : base(prefab, start, destination)
    {
        health = 100;
    }
}
