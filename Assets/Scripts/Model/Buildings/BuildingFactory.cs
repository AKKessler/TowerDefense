using UnityEngine;

public class BuildingFactory
{
    public readonly static GameObject WALL_1x1_PREFAB = (GameObject) Resources.Load("Prefabs/Wall1x1");
    public readonly static GameObject WALL_2x2_PREFAB = (GameObject) Resources.Load("Prefabs/Wall2x2");
    public readonly static GameObject WALL_2x1_PREFAB = (GameObject) Resources.Load("Prefabs/Wall2x1");
    public readonly static GameObject WALL_1x2_PREFAB = (GameObject) Resources.Load("Prefabs/Wall1x2");

    public static GameObject createBuilding(BuildingType type)
    {
        GameObject prefab = getBuildingPrefab(type);
        GameObject building = Object.Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
        return building;
    }

    public static GameObject createBuilding(BuildingType type, Transform spawn)
    {
        GameObject prefab = getBuildingPrefab(type);
        GameObject building = Object.Instantiate(prefab, spawn.position, Quaternion.identity) as GameObject;
        return building;
    }

    public static GameObject getBuildingPrefab(BuildingType type)
    {
        GameObject prefab = null;
        switch (type)
        {
            case BuildingType.Wall1x1:
                prefab = WALL_1x1_PREFAB;
                break;
            case BuildingType.Wall2x2:
                prefab = WALL_2x2_PREFAB;
                break;
            case BuildingType.Wall2x1:
                prefab = WALL_2x1_PREFAB;
                break;
            case BuildingType.Wall1x2:
                prefab = WALL_1x2_PREFAB;
                break;

        }
        return prefab;
    }
}
