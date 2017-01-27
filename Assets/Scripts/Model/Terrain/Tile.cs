using UnityEngine;
using System.Collections;

public class Tile {

    GameObject gameObject;

    bool isBuildable; // if object is on then is not buildable? also consider tile types.
    bool isWalkable;

    float x, z;
    float width, length;
    
    public Tile(float x, float z, float width, float length, bool isBuildable, bool isWalkable) : this(null, x, z, width, length, isBuildable, isWalkable)
    {
    }

    public Tile(GameObject gameObject, float x, float z, float width, float length, bool isBuildable, bool isWalkable)
    {
        this.gameObject = gameObject;
        this.x = x;
        this.z = z;
        this.width = width;
        this.length = length;
        this.isBuildable = isBuildable;
        this.isWalkable = isWalkable;
    }

    public void setGameObject(GameObject gameObject)
    {
        this.gameObject = gameObject;

        // TODO Consider if any scaling needs to be done to gameObject

        // this.gameObject.transform.Translate(getCenter3());
    }

    public GameObject getGameObject()
    {
        return gameObject;
    }

    public void setIsBuildable(bool isBuildable)
    {
        this.isBuildable = isBuildable;
    }

    public bool getIsBuildable()
    {
        return isBuildable && gameObject == null;
    }

    public void setIsWalkable(bool isWalkable)
    {
        this.isWalkable = isWalkable;
    }

    public bool getIsWalkable()
    {
        return isWalkable;
    }

    public Vector3 getCenter3()
    {
        return new Vector3(x + width / 2, 0.0f, z - length / 2);
    }

    private Vector2 getCenter2()
    {
        return new Vector2(x + width / 2, z - length / 2);
    }
}