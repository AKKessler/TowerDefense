using UnityEngine;
using System.Collections;

public class Wall2x2 : Building
{
    //new public static Object prefab = Resources.Load("Prefabs/Wall2x2");

    public Wall2x2()
    {
        width = 2;
        length = 2;
        height = 1;

        prefab = Resources.Load("Prefabs/Wall2x2");
    }
}
