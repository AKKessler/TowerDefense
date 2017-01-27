using UnityEngine;
using System.Collections;

public class FastCreep : Creep
{

    public static Object prefab = Resources.Load("Prefabs/FastCreep");

    public FastCreep(Transform start, Transform destination) : base(prefab, start, destination)
    {
        health = 75;
    }
}