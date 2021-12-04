using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class InputListener : MonoBehaviour
{
    public InputManager_SO inputManager_SO;

    public GameInputEvent OnUpdate;

    private GameInputData curentInputData;

    private EventSystem eventSystem;

    private Camera cam;
    private GameWorldMapManager map;

    public static InputListener instance;
    
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        cam = FindObjectOfType<Camera>();
        map = GameWorldMapManager.instance;
        eventSystem = EventSystem.current;
        curentInputData = new GameInputData();

        inputManager_SO.SetInputListener(this);
    }

    public void Update()
    {
        ResetInputData();

        UpdateInputEvents();

        OnUpdate?.Invoke(curentInputData);
    }

    private void ResetInputData()
    {
        curentInputData.oldTileMousePosition = curentInputData.tileMousePosition;
        curentInputData.AcceptTrigger = false;
        curentInputData.CancelTrigger = false;
    }

    public void UpdateInputEvents()
    {
        curentInputData.mouseMositionScreen = Input.mousePosition;
        curentInputData.worldMousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        curentInputData.worldMousePosition -= new Vector3(0, 0, curentInputData.worldMousePosition.z);

        curentInputData.isOverUI = eventSystem.IsPointerOverGameObject();

        curentInputData.tileMousePosition = map.GetTilePosition(curentInputData.worldMousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            curentInputData.AcceptTrigger = true;
        }

        if (Input.GetMouseButtonDown(1))
        {
            curentInputData.CancelTrigger = true;
        }
    }
}
