using System;
using System.Collections.Generic;
using UnityEngine;
public class GameUnitMovementController : GameWorldMap_Dependable
{

    public MovementGridCell[,] movementGrid;

    public  Vector3Int offset;

    private int[,] moveArray;
    private Vector3Int startPos;


    public GameMap GameMap;

    GameWorldMapManager GameWorldMapManager;

    private void Start()
    {

        //foreach(WorldObject worldObject in GameWorldMapManager.allObjectsOnMap)
        //{
        //    if (worldObject.blockMovement)
        //    {
        //        SetUnit(worldObject.worldPosition, true);
        //    }
        //}
    }

    public override void SetUp()
    {
        GameWorldMapManager = GameWorldMapManager.instance;
        GameWorldMapManager.OnUnitMove += GameWorldMapManager_OnUnitMove;
        GameWorldMapManager.OnUnitCreate += GameWorldMapManager_OnUnitCreate;
        GameWorldMapManager.OnUnitDeath += GameWorldMapManager_OnUnitDeath;

        CreateEmptyGrid(GameMap.MapSize, GameMap.MapOffset);

        LandTilemapManager landTilemapManager = FindObjectOfType<LandTilemapManager>();
        SpecialTilemapManager specialTilemapManager = FindObjectOfType<SpecialTilemapManager>();
        SetLand(landTilemapManager.GetAllLandPositions(), true);
    }

    private void GameWorldMapManager_OnUnitDeath(object sender, GameWorldMapManager.UnitEventArgs e)
    {
        if (e.worldObject.blockMovement)
            SetUnit(e.worldObject.worldPosition, false);
    }

    private void GameWorldMapManager_OnUnitCreate(object sender, GameWorldMapManager.UnitEventArgs e)
    {
        if(e.worldObject.blockMovement)
        SetUnit(e.worldObject.worldPosition, true);
    }



    private void GameWorldMapManager_OnUnitMove(object sender, MovingCharacter.MoveEventArg e)
    {
        SetUnit(e.newPos, true);
        SetUnit(e.oldPos, false);
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
        foreach(Vector3Int position in positions)
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

    public bool CanMoveTo(Vector3Int position)
    {
        position -= offset;
        return IsVectorInsideArray(position) && movementGrid[position.x, position.y].CanMoveTo();
    }

    private bool IsVectorInsideArray(Vector3Int vector)
    {
        return vector.x >= 0 && vector.x < movementGrid.GetLength(0) && vector.y >= 0 && vector.y < movementGrid.GetLength(1);
    }


    public Vector3Int[] GetMovementCircle(Vector3Int startPos, int movePoints)
    {
        moveArray  = new int[movePoints * 2 + 1, movePoints * 2 + 1];
        this.startPos = startPos;
        List<Vector3Int> nodesToLook = new List<Vector3Int>
        {
            new Vector3Int(movePoints, movePoints, 0)
        };

        Vector3Int size = new Vector3Int(moveArray.GetLength(0), moveArray.GetLength(1),0);
        Vector3Int offset = new Vector3Int(movePoints, movePoints,0);

        //Set up array
        for(int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                if (!CanMoveTo(new Vector3Int(x,y,0) + startPos - offset))
                {
                    moveArray[x, y] = -1;
                }
                else
                {
                    moveArray[x, y] = int.MaxValue;
                }
            }
        }

        moveArray[movePoints, movePoints] = 0;

        List<Vector3Int> temp = new List<Vector3Int>();

        //Calculate all paths
        while(nodesToLook.Count > 0)
        {
            foreach (Vector3Int node in nodesToLook)
            {
                if (moveArray[node.x, node.y] < movePoints)
                {


                    Vector3Int neighbour;

                    neighbour = node + new Vector3Int(1, 0, 0);
                    if (neighbour.x >= 0 && neighbour.x < moveArray.GetLength(0) && neighbour.y >= 0 && neighbour.y < moveArray.GetLength(1))
                        if (moveArray[neighbour.x, neighbour.y] > moveArray[node.x, node.y] + 1)
                        {
                            temp.Add(neighbour);
                            moveArray[neighbour.x, neighbour.y] = moveArray[node.x, node.y] + 1;
                        }

                    neighbour = node + new Vector3Int(-1, 0, 0);
                    if (neighbour.x >= 0 && neighbour.x < moveArray.GetLength(0) && neighbour.y >= 0 && neighbour.y < moveArray.GetLength(1))
                        if (moveArray[neighbour.x, neighbour.y] > moveArray[node.x, node.y] + 1)
                        {
                            temp.Add(neighbour);
                            moveArray[neighbour.x, neighbour.y] = moveArray[node.x, node.y] + 1;
                        }

                    neighbour = node + new Vector3Int(0, 1, 0);
                    if (neighbour.x >= 0 && neighbour.x < moveArray.GetLength(0) && neighbour.y >= 0 && neighbour.y < moveArray.GetLength(1))
                        if (moveArray[neighbour.x, neighbour.y] > moveArray[node.x, node.y] + 1)
                        {
                            temp.Add(neighbour);
                            moveArray[neighbour.x, neighbour.y] = moveArray[node.x, node.y] + 1;
                        }

                    neighbour = node + new Vector3Int(0, -1, 0);
                    if (neighbour.x >= 0 && neighbour.x < moveArray.GetLength(0) && neighbour.y >= 0 && neighbour.y < moveArray.GetLength(1))
                        if (moveArray[neighbour.x, neighbour.y] > moveArray[node.x, node.y] + 1)
                        {
                            temp.Add(neighbour);
                            moveArray[neighbour.x, neighbour.y] = moveArray[node.x, node.y] + 1;
                        }
                }
            }

            nodesToLook.Clear();
            nodesToLook.AddRange(temp);
            temp.Clear();
        }

        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                if(moveArray[x,y] != -1 && moveArray[x,y] != int.MaxValue)
                {
                    temp.Add(new Vector3Int(x, y, 0) + startPos - offset);
                }
            }
        }



        return temp.ToArray();

    }

    public Vector3Int[] GetMovementAndAttackCircle(Vector3Int startPos, int movePoints, out Vector3Int[] posibleAttackPos)
    {
        moveArray = new int[movePoints * 2 + 1, movePoints * 2 + 1];
        this.startPos = startPos;
        List<Vector3Int> nodesToLook = new List<Vector3Int>
        {
            new Vector3Int(movePoints, movePoints, 0)
        };

        Vector3Int size = new Vector3Int(moveArray.GetLength(0), moveArray.GetLength(1), 0);
        Vector3Int offset = new Vector3Int(movePoints, movePoints, 0);

        //Set up array
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                if (!CanMoveTo(new Vector3Int(x, y, 0) + startPos - offset))
                {
                    moveArray[x, y] = -1;
                }
                else
                {
                    moveArray[x, y] = int.MaxValue;
                }
            }
        }

        moveArray[movePoints, movePoints] = 0;

        List<Vector3Int> temp = new List<Vector3Int>();

        //Calculate all paths
        while (nodesToLook.Count > 0)
        {
            foreach (Vector3Int node in nodesToLook)
            {
                if (moveArray[node.x, node.y] < movePoints)
                {


                    Vector3Int neighbour;

                    neighbour = node + new Vector3Int(1, 0, 0);
                    if (neighbour.x >= 0 && neighbour.x < moveArray.GetLength(0) && neighbour.y >= 0 && neighbour.y < moveArray.GetLength(1))
                        if (moveArray[neighbour.x, neighbour.y] > moveArray[node.x, node.y] + 1)
                        {
                            temp.Add(neighbour);
                            moveArray[neighbour.x, neighbour.y] = moveArray[node.x, node.y] + 1;
                        }

                    neighbour = node + new Vector3Int(-1, 0, 0);
                    if (neighbour.x >= 0 && neighbour.x < moveArray.GetLength(0) && neighbour.y >= 0 && neighbour.y < moveArray.GetLength(1))
                        if (moveArray[neighbour.x, neighbour.y] > moveArray[node.x, node.y] + 1)
                        {
                            temp.Add(neighbour);
                            moveArray[neighbour.x, neighbour.y] = moveArray[node.x, node.y] + 1;
                        }

                    neighbour = node + new Vector3Int(0, 1, 0);
                    if (neighbour.x >= 0 && neighbour.x < moveArray.GetLength(0) && neighbour.y >= 0 && neighbour.y < moveArray.GetLength(1))
                        if (moveArray[neighbour.x, neighbour.y] > moveArray[node.x, node.y] + 1)
                        {
                            temp.Add(neighbour);
                            moveArray[neighbour.x, neighbour.y] = moveArray[node.x, node.y] + 1;
                        }

                    neighbour = node + new Vector3Int(0, -1, 0);
                    if (neighbour.x >= 0 && neighbour.x < moveArray.GetLength(0) && neighbour.y >= 0 && neighbour.y < moveArray.GetLength(1))
                        if (moveArray[neighbour.x, neighbour.y] > moveArray[node.x, node.y] + 1)
                        {
                            temp.Add(neighbour);
                            moveArray[neighbour.x, neighbour.y] = moveArray[node.x, node.y] + 1;
                        }
                }
            }

            nodesToLook.Clear();
            nodesToLook.AddRange(temp);
            temp.Clear();
        }

        List<Vector3Int> attackPos = new List<Vector3Int>();
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                if(moveArray[x,y] == -1 && isPositionAttack(x,y, movePoints))
                {
                    attackPos.Add(new Vector3Int(x, y, 0) + startPos - offset);
                }
            }
        }



        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                if (moveArray[x, y] != -1 && moveArray[x, y] != int.MaxValue)
                {
                    temp.Add(new Vector3Int(x, y, 0) + startPos - offset);
                }
            }
        }


        posibleAttackPos = attackPos.ToArray();
        return temp.ToArray();

    }

    private bool isPositionAttack(int x, int y, int movePoints)
    {
        return (x > 0 && moveArray[x - 1, y] < movePoints && moveArray[x - 1, y] != -1
            || y > 0 && moveArray[x, y - 1] < movePoints && moveArray[x, y - 1] != -1
            || x < moveArray.GetLength(0) - 1 && moveArray[x + 1, y] < movePoints && moveArray[x + 1, y] != -1
            || y < moveArray.GetLength(1) - 1 && moveArray[x, y + 1] < movePoints && moveArray[x, y + 1] != -1);
    }

    public int GetPathCost(Vector3Int destination)
    {
        return 0;
    }

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

}



