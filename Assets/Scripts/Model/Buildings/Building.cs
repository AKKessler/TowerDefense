using UnityEngine;
using System.Collections;

public abstract class Building
{
    public Object prefab;
    public int width;
    public int length;
    public int height;

    //public Transform projectilePrefab;
    //public Transform target;
    
    public GameObject gameObject;

    public void setGameObject(GameObject gameObject)
    {
        this.gameObject = gameObject;
    }
}
