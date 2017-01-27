using UnityEngine;
using System.Collections;

public class Wall1x1 : Building
{
    //new public static Object prefab = Resources.Load("Prefabs/Wall1x1");

    public Wall1x1()
    {
        width = 1;
        length = 1;
        height = 1;

        prefab = Resources.Load("Prefabs/Wall1x1");
    }
}
