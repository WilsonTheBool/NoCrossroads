using System;
using System.Collections.Generic;
using UnityEngine;

public class NewGameMovementController: MonoBehaviour
{

    public MovementGridCell[,] movementGrid;

    public Vector3Int offset;

    private int[,] moveArray;
    private Vector3Int startPos;


    public GameMap GameMap;

    GameWorldMapManager GameWorldMapManager;

    public struct MovementGridCell
    {
        public bool isLand;
        public bool isUnit;


        public bool CanMoveTo()
        {
            return isLand && !isUnit;
        }

        public void SetLand(bool isLand)
        {
            this.isLand = isLand;
        }

        public void SetUnit(bool isUnit)
        {
            this.isUnit = isUnit;
        }
    }

    private void Start()
    {
        GameWorldMapManager = GameWorldMapManager.instance;
        GameWorldMapManager.OnUnitMove += GameWorldMapManager_OnUnitMove;
        GameWorldMapManager.OnUnitCreate += GameWorldMapManager_OnUnitCreate;
        GameWorldMapManager.OnUnitDeath += GameWorldMapManager_OnUnitDeath;

        CreateEmptyGrid(GameMap.MapSize, GameMap.MapOffset);

        LandTilemapManager landTilemapManager = FindObjectOfType<LandTilemapManager>();
        SpecialTilemapManager specialTilemapManager = FindObjectOfType<SpecialTilemapManager>();
        SetLand(landTilemapManager.GetAllLandPositions(), true);

        foreach (WorldObject worldObject in GameWorldMapManager.allObjectsOnMap)
        {
            if (worldObject.blockMovement)
            {
                SetUnit(worldObject.worldPosition, true);
            }
        }
    }

    private void GameWorldMapManager_OnUnitDeath(object sender, GameWorldMapManager.UnitEventArgs e)
    {
        if (e.worldObject.blockMovement)
            SetUnit(e.worldObject.worldPosition, false);
    }

    private void GameWorldMapManager_OnUnitCreate(object sender, GameWorldMapManager.UnitEventArgs e)
    {
        if (e.worldObject.blockMovement)
            SetUnit(e.worldObject.worldPosition, true);
    }



    private void GameWorldMapManager_OnUnitMove(object sender, MovingCharacter.MoveEventArg e)
    {
        SetUnit(e.oldPos, false);
        SetUnit(e.newPos, true);
        
    }

    public void CreateEmptyGrid(Vector3Int size, Vector3Int offset)
    {
        this.offset = offset;
        movementGrid = new MovementGridCell[size.x, size.y];
    }

    public void SetLand(Vector3Int position, bool isLand)
    {
        position -= offset;
        movementGrid[position.x, position.y].isLand = isLand;
    }

    public void SetLand(Vector3Int[] positions, bool isLand)
    {
        foreach (Vector3Int position in positions)
        {
            Vector3Int oPos = position - offset;
            movementGrid[oPos.x, oPos.y].isLand = isLand;
        }

    }

    public void SetUnit(Vector3Int position, bool isUnit)
    {
        position -= offset;
        movementGrid[position.x, position.y].isUnit = isUnit;
    }

    public void SetUnit(Vector3Int[] positions, bool isUnit)
    {
        foreach (Vector3Int position in positions)
        {
            Vector3Int oPos = position - offset;
            movementGrid[oPos.x, oPos.y].isUnit = isUnit;
        }

    }

    public bool CanMoveTo(Vector3Int local)
    {
        return IsVectorInsideArray(local) && movementGrid[local.x, local.y].CanMoveTo();
    }

    public bool IsUnit(Vector3Int local)
    {
        return IsVectorInsideArray(local) && movementGrid[local.x, local.y].isUnit;
    }

    private bool IsVectorInsideArray(Vector3Int vector)
    {
        return vector.x >= 0 && vector.x < movementGrid.GetLength(0) && vector.y >= 0 && vector.y < movementGrid.GetLength(1);
    }

    private Vector3Int GlobalToLocal(Vector3Int global)
    {
        return global - offset;
    }

    private Vector3Int LocalToGLobal(Vector3Int local)
    {
        return local + offset;
    }

    public bool HasUnit(Vector3Int global)
    {
        Vector3Int local = GlobalToLocal(global);
        return movementGrid[local.x, local.y].isUnit;
    }

    public Vector3Int[] GetFreeNeigbours(Vector3Int globalPos)
    {
        Vector3Int startPos = GlobalToLocal(globalPos);

        Vector3Int neighbour;

        List<Vector3Int> neighbours = new List<Vector3Int>(4);

        neighbour = startPos + new Vector3Int(1, 0, 0);
        if (CanMoveTo(neighbour))
        {
            neighbours.Add(LocalToGLobal(neighbour));
        }

        neighbour = startPos + new Vector3Int(0, 1, 0);
        if (CanMoveTo(neighbour))
        {
            neighbours.Add(LocalToGLobal(neighbour));
        }

        neighbour = startPos + new Vector3Int(-1, 0, 0);
        if (CanMoveTo(neighbour))
        {
            neighbours.Add(LocalToGLobal(neighbour));
        }

        neighbour = startPos + new Vector3Int(0, -1, 0);
        if (CanMoveTo(neighbour))
        {
            neighbours.Add(LocalToGLobal(neighbour));
        }

        return neighbours.ToArray();
    }

    public Vector3Int[] GetBlockingNeigbours(Vector3Int globalPos)
    {
        Vector3Int startPos = GlobalToLocal(globalPos);

        Vector3Int neighbour;

        List<Vector3Int> neighbours = new List<Vector3Int>(4);

        neighbour = startPos + new Vector3Int(1, 0, 0);
        if (IsUnit(neighbour))
        {
            neighbours.Add(LocalToGLobal(neighbour));
        }

        neighbour = startPos + new Vector3Int(0, 1, 0);
        if (IsUnit(neighbour))
        {
            neighbours.Add(LocalToGLobal(neighbour));
        }

        neighbour = startPos + new Vector3Int(-1, 0, 0);
        if (IsUnit(neighbour))
        {
            neighbours.Add(LocalToGLobal(neighbour));
        }

        neighbour = startPos + new Vector3Int(0, -1, 0);
        if (IsUnit(neighbour))
        {
            neighbours.Add(LocalToGLobal(neighbour));
        }

        return neighbours.ToArray();
    }
}

