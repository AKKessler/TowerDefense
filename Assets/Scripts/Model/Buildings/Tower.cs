using UnityEngine;
using System.Collections;

public class Tower : Building
{

    // TODO Make Tower abstract, but add more properties (e.g. damage, attack speed, etc.)
    // public static Object prefab = Resources.Load("Prefabs/Tower");

    public Tower()
    {
        width = 1;
        length = 1;
        height = 3;
    }
}
