using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class UnitSelectController : MonoBehaviour
{
    public UI_CanvasGroupsController groupsController;

    public SelectUnitEvent OnNewSelect;
    public SelectUnitEvent OnRemoveSelect;

    public SelectableObject curentSelect;

    public GameUnitMovementController GameUnitMovementController;
    public SpecialTilemapManager specialTilemapManager;
    public GameWorldMapManager GameWorldMapManager;

    private SelectModule.SelectEventArgs curentSelectEventArgs;
    private void Start()
    {
        GameWorldMapManager = GameWorldMapManager.instance;
    }
    /// <summary>
    /// Add to InputManager.OnAccept
    /// </summary>
    /// <param name="inputData"></param>
    public void OnTrySelect(GameInputData inputData)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100))
        {
            SelectableObject selectableObject = hit.collider.GetComponent<SelectableObject>();

            if (selectableObject != null && selectableObject != curentSelect)
            {
                if (curentSelect != null)
                    OnRemoveSelect.Invoke(curentSelect);

                specialTilemapManager.ClearTilemap();
                curentSelect = selectableObject;

                curentSelectEventArgs = new SelectModule.SelectEventArgs
                {
                    SelectableObject = curentSelect,
                    GameUnitMovementController = GameUnitMovementController,
                    GameWorldMapManager = this.GameWorldMapManager,
                    SpecialTilemapManager = this.specialTilemapManager,
                    MovingCharacter = curentSelect.GetComponent<MovingCharacter>(),
                    AttackingCharacter = curentSelect.GetComponent<AttackingCharacter>()
                };

                curentSelect.Select_Start(inputData, curentSelectEventArgs);

                //MovingCharacter movingCharacter = hit.collider.GetComponent<MovingCharacter>();

                //if (movingCharacter != null)
                //{
                //    DrawMoveRange(inputData.tileMousePosition, movingCharacter.movePoints);
                //}

                OnNewSelect.Invoke(curentSelect);
            }

           
        }
    }

    public void TryMoveCharacter_OnAccept(GameInputData data)
    {
        if(curentSelect != null)
        {
            MovingCharacter movingCharacter = curentSelect.GetComponent<MovingCharacter>();
            if (movingCharacter != null && specialTilemapManager.TilemapHasTile_CanPlaceTIle(data.tileMousePosition))
            {
                
                movingCharacter.Move(data.tileMousePosition, 0);
                RemoveSelected(data);
            }
        }
    }

    public void DrawMoveRange(Vector3Int startPos, int chSpeed)
    {
       Vector3Int[] moveRange = GameUnitMovementController.GetMovementCircle(startPos, chSpeed);

        foreach(Vector3Int vec in moveRange)
        {
            specialTilemapManager.DrawTile_CanPlaceTile(vec);
        }
        
    }

    public void SetSelectUI(SelectableObject selectableObject)
    {
        UI_CanvasGroupsController.CanvasGroupFlags flags = new UI_CanvasGroupsController.CanvasGroupFlags() { setDefault = true};
        UI_CanvasGroupsController.CanvasGroupFlags flags2 = new UI_CanvasGroupsController.CanvasGroupFlags() {setSelectUnitWondow = true };

        groupsController.SetActiveWindows(flags, false);
        groupsController.SetActiveWindows(flags2, true);
    }

    public void SetDeselectUI(SelectableObject selectableObject)
    {
        UI_CanvasGroupsController.CanvasGroupFlags flags = new UI_CanvasGroupsController.CanvasGroupFlags() { setDefault = true };
        UI_CanvasGroupsController.CanvasGroupFlags flags2 = new UI_CanvasGroupsController.CanvasGroupFlags() { setSelectUnitWondow = true };

        groupsController.SetActiveWindows(flags, true);
        groupsController.SetActiveWindows(flags2, false);
    }

    public void RemoveSelected(GameInputData inputData)
    {

        if (curentSelect != null)
        {
            curentSelect.Select_End(inputData, curentSelectEventArgs);
            OnRemoveSelect.Invoke(curentSelect);
            curentSelectEventArgs = null;
        }
          
        curentSelect = null;
    }

    [System.Serializable]
    public class SelectUnitEvent : UnityEvent<SelectableObject> { };
}
