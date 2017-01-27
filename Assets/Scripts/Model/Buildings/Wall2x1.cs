using UnityEngine;
using System.Collections;

public class Wall2x1 : Building
{
    //new public static Object prefab = Resources.Load("Prefabs/Wall2x1");

    public Wall2x1()
    {
        width = 1;
        length = 2;
        height = 1;

        prefab = Resources.Load("Prefabs/Wall2x1");
    }
}
