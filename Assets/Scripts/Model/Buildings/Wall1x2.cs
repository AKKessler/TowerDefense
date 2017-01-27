using UnityEngine;
using System.Collections;

public class Wall1x2 : Building
{
    //new public static Object prefab = Resources.Load("Prefabs/Wall1x2");

    public Wall1x2()
    {
        width = 2;
        length = 1;
        height = 1;

        prefab = Resources.Load("Prefabs/Wall1x2");
    }
}
