using UnityEngine;

public class BuildManager
{
    private readonly static float PREVIEW_OPACITY = 0.15f;
    private readonly static Color PREVIEW_RED = new Color(1.0f, 0.0f, 0.0f, PREVIEW_OPACITY);
    private readonly static Color PREVIEW_GREEN = new Color(0.0f, 1.0f, 0.0f, PREVIEW_OPACITY);
    private readonly static Vector3 PREVIEW_OFFSET = new Vector3(0.0f, 0.69f, 0.0f);

    private GridManager gridManager;

    private bool buildMode;

    private bool buildMenu;

    private BuildingType currentBuildingType;

    private GameObject preview;


    public BuildManager(GridManager gridManager)
    {
        this.gridManager = gridManager;

        buildMode = false;
        buildMenu = false;
        preview = null;
    }

    public BuildManager(GridManager gridManager , Transform[] waypoints)
    {
        this.gridManager = gridManager;

        buildMode = false;
        buildMenu = false;
        preview = null;
    }

    public void update()
    {
        updateBuildingPreview();
        handleInput();
    }

    private void handleInput()
    {

        if (buildMenu) {
            if (Input.GetKeyUp(KeyCode.Alpha1)) {
                selectBuildingType(BuildingType.Wall1x1);
            }
            else if (Input.GetKeyUp(KeyCode.Alpha2)) {
                selectBuildingType(BuildingType.Wall2x2);
            }
            else if (Input.GetKeyUp(KeyCode.Alpha3)) {
                selectBuildingType(BuildingType.Wall2x1);
            }
            else if (Input.GetKeyUp(KeyCode.Alpha4)) {
                selectBuildingType(BuildingType.Wall1x2);
            }
        }
        else if (Input.GetMouseButtonUp(0)) {
            if(buildAtMouse())
            {
                exitBuildMode();
            }
        }
        else if (Input.GetMouseButtonUp(1)) {
            buildMenu = true;
        }
    }

    private void updateBuildingPreview()
    {
        if (!buildMode || buildMenu) return;

        Color previewColor;
        float distance = 0;
        Plane plane = new Plane(Vector3.up, Vector3.zero);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (plane.Raycast(ray, out distance))
        {
            GameObject buildingPrefab = BuildingFactory.getBuildingPrefab(currentBuildingType);

            createPreview(buildingPrefab);

            Vector3 intersect = ray.GetPoint(distance);
            int row = Mathf.FloorToInt(intersect.x);
            int col = Mathf.FloorToInt(intersect.z);
            if (gridManager.canBuildAt(buildingPrefab, row, col))
            {
                previewColor = PREVIEW_GREEN;
            }
            else
            {
                previewColor = PREVIEW_RED;
            }
            Vector3 center = gridManager.getCenterAt(buildingPrefab.GetComponent<Building>(), row, col);
            if(center != Vector3.zero)
                preview.transform.position = center + PREVIEW_OFFSET;

            Renderer renderer = preview.GetComponent<Renderer>();
            renderer.material.color = previewColor;
        }
    }

    private void createPreview(GameObject buildingPrefab)
    {
        if (preview == null)
        {
            preview = Object.Instantiate(buildingPrefab, PREVIEW_OFFSET, Quaternion.identity) as GameObject;
            preview.name = "Preview";
            preview.GetComponent<NavMeshObstacle>().enabled = false;
            preview.transform.parent = gridManager.transform;
            //Object.Destroy(preview.GetComponent<NavMeshObstacle>());
        }
    }

    private bool buildAtMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, Vector3.zero);
        float distance = 0;
        if (plane.Raycast(ray, out distance))
        {
            Vector3 intersect = ray.GetPoint(distance);
            int row = Mathf.FloorToInt(intersect.x);
            int col = Mathf.FloorToInt(intersect.z);
            GameObject prefab = BuildingFactory.getBuildingPrefab(currentBuildingType);
            if(gridManager.canBuildAt(prefab, row, col)) {
                //if (!gridManager.wouldBlockPathAt(prefab, row, col)) {
                return gridManager.placeBuildingAt(BuildingFactory.createBuilding(currentBuildingType), row, col);
                //}
            }
        }
        return false;
    }

    private void selectBuildingType(BuildingType type)
    {
        currentBuildingType = type;
        buildMenu = false;
    }

    public void enterBuildMode()
    {
        buildMode = true;
        buildMenu = true;
    }

    public void exitBuildMode()
    {
        buildMode = false;
        buildMenu = false;

        if (preview != null)
        {
            Object.Destroy(preview);
        }
    }

    public bool inBuildMode()
    {
        return buildMode;
    }
}
