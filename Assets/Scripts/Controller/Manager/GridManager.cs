using UnityEngine;
using System.Collections;

public class GridManager : MonoBehaviour {

    private Vector3 ORIGIN_OFFSET = new Vector3(0.0f, 0.5f, 0.0f);

    public int numColumns = 16;
    public int numRows = 16;

    public Texture tileTexture;
    public Texture tileTextureAndOverlay;

    GameObject ground;
    Grid grid;
    float width, length;
    bool showOverlay;
    
    void Start () {
        length = numRows;
        width = numColumns;
        showOverlay = false;

        ground = transform.Find("Ground").gameObject;
        ground.transform.localScale = new Vector3(numRows / 10.0f, 1.0f, numColumns / 10.0f);
        ground.transform.position = new Vector3(length / 2, 0, width / 2);

        Renderer renderer = ground.GetComponent<Renderer>();
        renderer.material.mainTextureScale = new Vector2(length, width);

        grid = new Grid(numRows, numColumns, width, length);
	}

    public Vector3 getCenterAt(int row, int col)
    {
        return grid.getCenterAt(row, col);
    }

    public Vector3 getCenterAt2(Building building, int row, int col)
    {
        Vector3 average = Vector3.zero;
        for(int i = 0; i < building.length; i++)
        {
            for(int j = 0; j < building.width; j++)
            {
                average += grid.getCenterAt(row + i, col + j);
            }
        }
        float towerArea = building.width * building.length;
        return average / towerArea;
    }

    public bool createObjectAt(Building building, int row, int col)
    {
        if (canBuildAt(building, row, col))
        {
            //Transform tf = Instantiate(building.prefab, ORIGIN_OFFSET, Quaternion.identity) as Transform;
            //tf.parent = this.transform;
            //GameObject go = tf.gameObject;
            GameObject go = Instantiate(building.prefab, ORIGIN_OFFSET, Quaternion.identity) as GameObject;
            go.transform.parent = ground.transform;
            building.setGameObject(go);
            return grid.setObjectAt2(building, row, col);
        }
        return false;
    }

    public bool canBuildAt(Building building, int row, int col)
    {
        bool canBuild = true;
        for (int i = 0; i < building.length; i++)
        {
            for(int j = 0; j < building.width; j++)
            {
                canBuild &= grid.canBuildAt(row + i, col + j);
            }
        }
        return canBuild;
    }

    public void toggleOverlay()
    {
        showOverlay = !showOverlay;

        Renderer renderer = ground.GetComponent<Renderer>();
        Texture currentTexture;
        if (showOverlay)
        {
            currentTexture = tileTextureAndOverlay;
        }
        else
        {
            currentTexture = tileTexture;
        }
        renderer.material.mainTexture = currentTexture;
    }
   
}
