using System;
using System.Collections.Generic;
using UnityEngine;
public class SelectFreeUnitController: GameWorldMap_Dependable
{
    [SerializeField]
    UnitSelectController UnitSelectController;

    GameWorldMapManager GameWorldMapManager;

    Camera Camera;

    [SerializeField]
    NewGameMovementController newGameMovementController;

    List<MovingCharacter> movingCharacters = new List<MovingCharacter>();
    int curentIndex;

    SelectableObject curetnSelect;

    private void Awake()
    {
       
    }

    public override void SetUp()
    {
        GameWorldMapManager = GameWorldMapManager.instance;
        GameWorldMapManager.OnUnitCreate += GameWorldMapManager_OnUnitCreate;
        GameWorldMapManager.OnUnitDeath += GameWorldMapManager_OnUnitDeath;

        Camera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SelectNextUnit();
        }
    }

    private void GameWorldMapManager_OnUnitDeath(object sender, GameWorldMapManager.UnitEventArgs e)
    {
        SelectableObject selectableObject = e.worldObject.GetComponent<SelectableObject>();
        if (selectableObject != null && selectableObject.canAutoSelect)
        {
            MovingCharacter movingCharacter = selectableObject.GetComponent<MovingCharacter>();

            if (movingCharacter != null)
            {
                movingCharacters.Remove(movingCharacter);
            }
        }
    }

    public void ClampCurentIndexInRange()
    {
        curentIndex = Mathf.Clamp(curentIndex, 0, movingCharacters.Count - 1);
    }

    public void RemoveSelected()
    {
        UnitSelectController.RemoveSelected(new GameInputData());
    }

    private void GameWorldMapManager_OnUnitCreate(object sender, GameWorldMapManager.UnitEventArgs e)
    {
        SelectableObject selectableObject = e.worldObject.GetComponent<SelectableObject>();
        if(selectableObject != null && selectableObject.canAutoSelect)
        {
            MovingCharacter movingCharacter = selectableObject.GetComponent<MovingCharacter>();

            if(movingCharacter != null)
            {
                movingCharacters.Add(movingCharacter);
            }
        }
    }

    public void SelectSameUnit()
    {
        if (movingCharacters.Count > 0)
        {

            ClampCurentIndexInRange();

            int index = curentIndex;

            do
            {
                if (movingCharacters[index].movePoints > 0)
                {
                    SelectUnit(movingCharacters[index]);
                    break;
                }
                else
                {
                    index = TickIndex(index);
                }
            }
            while (index != curentIndex);

            curentIndex = index;
        }
    }

    public void SelectNextUnit()
    {
        if (movingCharacters.Count > 0)
        {
            ClampCurentIndexInRange();

            int index = TickIndex(curentIndex);

            do
            {
                if (movingCharacters[index].movePoints > 0)
                {
                    

                    SelectUnit(movingCharacters[index]);
                    curentIndex = index;
                    break;
                }
                else
                {
                    index = TickIndex(index);
                }
            }
            while (index != curentIndex);

           
        }
    }

    void SelectUnit(MovingCharacter unit)
    {
        if(unit.TryGetComponent<SelectableObject>(out SelectableObject selectableObject))
        {
            RemoveSelected();

            UnitSelectController.OnTrySelect(selectableObject);

            Camera.transform.position = unit.transform.position - new Vector3(0, 0, 10);
        }
    }

    private int TickIndex(int index)
    {
        if(index >= movingCharacters.Count - 1)
        {
            return 0;
        }
        else
        {
            return index + 1;
        }
    }
}

