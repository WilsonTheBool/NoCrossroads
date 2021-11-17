using UnityEngine;
using System.Collections.Generic;

public class UnitMovementData
{
    List<UnitMovementCell> moveAreaPositions;

    List<Vector3Int> unitsInMovingArea = new List<Vector3Int>();

    GameUnitMovementController gameUnitMovementController;

    private UnitMovementCell GetCell(Vector3Int globalPos)
    {
        foreach(UnitMovementCell cell in moveAreaPositions)
        {
            if(cell.position == globalPos)
            {
                return cell;
            }
        }

        return null;
    }

    public bool MoveAreaContains(Vector3Int pos)
    {
        return unitsInMovingArea.Contains(pos);
    }

    public Vector3Int[] GetPath(Vector3Int globalEndPos)
    {
        List<Vector3Int> path = new List<Vector3Int>();
        UnitMovementCell parent = GetCell(globalEndPos);
        while (parent != null)
        {
            path.Add(parent.position);
            parent = parent.parent;
        }

        return path.ToArray();
    }

    public int GetPathCost(Vector3Int globalPos)
    {
        return GetCell(globalPos).moveLength;
    }

    public Vector3Int[] GetMovementArea()
    {
        List<Vector3Int> area = new List<Vector3Int>(moveAreaPositions.Count);

        foreach(UnitMovementCell cell in moveAreaPositions)
        {
            area.Add(cell.position);
        }

        return area.ToArray();
    }

    public Vector3Int[] GetPossiableAttackTiles()
    {
        return unitsInMovingArea.ToArray();
    }

    public void SetUp(NewGameMovementController controller, Vector3Int originPos, int movePoint)
    {

        //CreateMovementArea
        List<UnitMovementCell> nodsToLook = new List<UnitMovementCell>();
        List<UnitMovementCell> allNodes = new List<UnitMovementCell>();

        List<UnitMovementCell> tempAdd = new List<UnitMovementCell>();
        unitsInMovingArea.Clear();
        var startCell = new UnitMovementCell(0, originPos);
        allNodes.Add(startCell);
        nodsToLook.Add(startCell);

        while(nodsToLook.Count > 0)
        {
            foreach(UnitMovementCell cell in nodsToLook)
            {
                if(cell.moveLength < movePoint)
                {
                    foreach (Vector3Int vec in controller.GetFreeNeigbours(cell.position))
                    {
                        UnitMovementCell foundCell = GetCell(vec);
                        if(foundCell == null)
                        {
                            var nCell = new UnitMovementCell(cell.moveLength + 1, vec, cell);
                            tempAdd.Add(nCell);
                            allNodes.Add(nCell);
                        }
                        else
                        {
                            if(foundCell.moveLength > cell.moveLength + 1)
                            {
                                foundCell.moveLength = cell.moveLength + 1;
                                foundCell.parent = cell;

                                tempAdd.Add(foundCell);
                            }
                        }
                    }

                    foreach(Vector3Int vec in controller.GetBlockingNeigbours(cell.position))
                    {
                        if (!unitsInMovingArea.Contains(vec))
                        {
                            unitsInMovingArea.Add(vec);
                        }
                    }



                }
                
            }

            nodsToLook.Clear();
            nodsToLook.AddRange(tempAdd);
            tempAdd.Clear();
        }

        //allNodes.Remove(startCell);
        moveAreaPositions = allNodes;

        UnitMovementCell GetCell(Vector3Int pos)
        {
            foreach (UnitMovementCell cell in allNodes)
            {
                if (cell.position == pos)
                {
                    return cell;
                }
            }

            return null;
        }
    }

    private class UnitMovementCell
    {
        public int moveLength;

        public Vector3Int position;

        public UnitMovementCell parent;

        public UnitMovementCell(int moveLength, Vector3Int position)
        {
            this.moveLength = moveLength;
            this.position = position;
            this.parent = null;
        }

        public UnitMovementCell(int moveLength, Vector3Int position, UnitMovementCell parent)
        {
            this.moveLength = moveLength;
            this.position = position;
            this.parent = parent;
        }
    }
}
