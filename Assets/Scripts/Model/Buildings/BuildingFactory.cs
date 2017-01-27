using UnityEngine;

public class BuildingFactory
{

    public static Building createBuilding(BuildingType type)
    {
        switch (type)
        {
            case BuildingType.Wall1x1:
                return new Wall1x1();
            case BuildingType.Wall2x2:
                return new Wall2x2();
            case BuildingType.Wall2x1:
                return new Wall2x1();
            case BuildingType.Wall1x2:
                return new Wall1x2();
            default:
                return null;

        }
    }

    //public static Object getPrefab(BuildingType type)
    //{
    //    switch (type)
    //    {
    //        case BuildingType.Wall1x1:
    //            return Wall1x1.prefab;
    //        case BuildingType.Wall2x2:
    //            return Wall2x2.prefab;
    //        case BuildingType.Wall2x1:
    //            return Wall2x1.prefab;
    //        case BuildingType.Wall1x2:
    //            return Wall1x2.prefab;
    //        default:
    //            return null;

    //    }
    //}
}
