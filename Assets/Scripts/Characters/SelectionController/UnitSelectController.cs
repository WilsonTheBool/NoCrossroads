using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class UnitSelectController : MonoBehaviour
{
    public UI_CanvasGroupsController groupsController;

    public SelectUnitEvent OnNewSelect;
    public SelectUnitEvent OnRemoveSelect;

    public SelectableObject curentSelect;

    public NewGameMovementController NewGameMovementController;
    public SpecialTilemapManager specialTilemapManager;
    public GameWorldMapManager GameWorldMapManager;

    private SelectModule.SelectEventArgs curentSelectEventArgs;

    public SelectFreeUnitController SelectFreeUnitController;
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
        if (!inputData.isOverUI)
        {

        Ray ray = Camera.main.ScreenPointToRay(inputData.mouseMositionScreen);
        RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                SelectableObject selectableObject = hit.collider.GetComponent<SelectableObject>();

                if (selectableObject != null && selectableObject != curentSelect)
                {
                    if (curentSelect != null)
                    {
                        RemoveSelected(inputData);
                        OnRemoveSelect.Invoke(curentSelect);
                    }
                        

                    specialTilemapManager.ClearTilemap();
                    specialTilemapManager.aboveSpecialTilemap.ClearAllTiles();
                    curentSelect = selectableObject;

                    curentSelectEventArgs = new SelectModule.SelectEventArgs
                    {
                        SelectableObject = curentSelect,
                        //GameUnitMovementController = GameUnitMovementController,
                        GameWorldMapManager = this.GameWorldMapManager,
                        NewGameMovementController = this.NewGameMovementController,
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
    }

    public void OnTrySelect(SelectableObject selectableObject)
    {
        if (selectableObject != null && selectableObject != curentSelect)
        {
            if (curentSelect != null)
            {
                
                    OnRemoveSelect.Invoke(curentSelect);
            }
                

            specialTilemapManager.ClearTilemap();
            specialTilemapManager.aboveSpecialTilemap.ClearAllTiles();
            curentSelect = selectableObject;

            GameInputData curentInputData = new GameInputData
            {
                tileMousePosition = selectableObject.WorldObject.worldPosition

            };


            curentSelectEventArgs = new SelectModule.SelectEventArgs
            {
                SelectableObject = curentSelect,
                
                GameWorldMapManager = this.GameWorldMapManager,
                NewGameMovementController = this.NewGameMovementController,
                SpecialTilemapManager = this.specialTilemapManager,
                MovingCharacter = curentSelect.GetComponent<MovingCharacter>(),
                AttackingCharacter = curentSelect.GetComponent<AttackingCharacter>()
            };

            curentSelect.Select_Start(curentInputData, curentSelectEventArgs);

            OnNewSelect.Invoke(curentSelect);
        }
    }

    public void TryMoveCharacter_OnAccept(GameInputData data)
    {
        if(curentSelect != null)
        {
            curentSelect.Seleect_Accept(data, curentSelectEventArgs);

            MovingCharacter movingCharacter = curentSelect.GetComponent<MovingCharacter>();
            if (movingCharacter != null && movingCharacter.movePoints > 0)
            {
                var select = curentSelect;
                RemoveSelected(data);
                OnTrySelect(select);
            }
            else
            {
                RemoveSelected(data);
            }
            

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

    public void UpdateSelected(GameInputData inputData)
    {
        if(curentSelect != null && inputData.oldTileMousePosition != inputData.tileMousePosition)
        {
            curentSelect.Select_Update(inputData, curentSelectEventArgs);
        }
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
