using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private const string SELECTABLE_TAG = "Selectable";

    public GameObject gridObject;

    public GameObject selectedObject;
    
    public GameObject waypoints;

    private Transform start, finish;

    private WaveManager waveManager;

    private BuildManager buildManager;

    private GridManager gridManager;
    
    void Start()
    {
        gridManager = gridObject.GetComponent<GridManager>();

        waypoints = GameObject.Find("Waypoints");
        start = waypoints.transform.Find("Start");
        finish = waypoints.transform.Find("Finish");
        waveManager = new WaveManager(start, finish);

        buildManager = new BuildManager(gridManager);
    }
    
    void Update()
    {
        handleInput();

        if (buildManager.inBuildMode()) {
            buildManager.update();
        }
    }

    private void handleInput()
    {
        if (Input.GetMouseButtonDown(0) && !buildManager.inBuildMode()) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            clickAtPoint(ray); // TODO rename? dont pass ray param
        }
        else if (Input.GetKeyUp(KeyCode.B) && !buildManager.inBuildMode()) {
            ClearSelection();
            buildManager.enterBuildMode();
        }
        else if (Input.GetKeyUp(KeyCode.Escape)){
            ClearSelection();
            buildManager.exitBuildMode();
        }
        else if (Input.GetKeyUp(KeyCode.G)) {
            gridManager.toggleOverlay();
        }
        else if (Input.GetKeyUp(KeyCode.Space)){
            waveManager.spawnCreep();
        }
        else if (Input.GetKeyUp(KeyCode.ScrollLock)){
            waveManager.nextWave();
        }
    }

    // consider using plane for rayCast? and then can use grid.getObjectAt(row,col)?
    void clickAtPoint(Ray ray)
    {
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.tag == SELECTABLE_TAG)
            {
                GameObject hitObject = hit.collider.gameObject;
                SelectObject(hitObject);
            }
            else
            {
                ClearSelection();
            }
        }
        else
        {
            ClearSelection();
        }
    }

    void SelectObject(GameObject obj)
    {
        if (selectedObject != null)
        {
            if (obj == selectedObject)
                return;

            ClearSelection();
        }

        selectedObject = obj;
    }

    void ClearSelection()
    {
        selectedObject = null;
    }

}
