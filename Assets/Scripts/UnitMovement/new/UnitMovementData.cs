using UnityEngine;
using System.Collections.Generic;

public class UnitMovementData
{
    public static int UnitsVisionRange = 14;

    List<UnitMovementCell> moveAreaPositions;

    List<Vector3Int> unitsInMovingArea = new List<Vector3Int>();

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

    public Vector3Int GetPathMaxRange(Vector3Int globalEndPos, int maxRange)
    {
        UnitMovementCell parent = GetCell(globalEndPos);
        while (parent != null)
        {
            if(parent.moveLength <= maxRange && parent.canStand)
            {
                
                return parent.position;
            }

            parent = parent.parent;
        }

        return new Vector3Int(0, 0, 0);
    }

    public int GetPathCost(Vector3Int globalPos)
    {
        return GetCell(globalPos).moveLength;
    }

    public int GetPathCost_Attack(Vector3Int globalPos)
    {
        int minLength = int.MaxValue;

        Vector3Int offset;
        offset = new Vector3Int(0, 1,0);
        var data = GetCell(globalPos + offset);
        if (data != null && data.moveLength < minLength)
        {
            minLength = data.moveLength;
        }

        offset = new Vector3Int(1, 0, 0);
        data = GetCell(globalPos + offset);
        if (data != null && data.moveLength < minLength)
        {
            minLength = data.moveLength;
        }

        offset = new Vector3Int(0, -1, 0);
       data = GetCell(globalPos + offset);
        if (data != null && data.moveLength < minLength)
        {
            minLength = data.moveLength;
        }

        offset = new Vector3Int(-1, 0, 0);
        data = GetCell(globalPos + offset);
        if (data != null && data.moveLength < minLength)
        {
            minLength = data.moveLength;
        }

        return minLength;
    }

    public Vector3Int[] GetMovementArea()
    {
        List<Vector3Int> area = new List<Vector3Int>(moveAreaPositions.Count);

        foreach(UnitMovementCell cell in moveAreaPositions)
        {
            if (cell.canStand)
            area.Add(cell.position);
        }

        return area.ToArray();
    }

    public Vector3Int[] GetMovementArea(int Speed)
    {
        List<Vector3Int> area = new List<Vector3Int>(moveAreaPositions.Count);

        foreach (UnitMovementCell cell in moveAreaPositions)
        {
            if(cell.moveLength <= Speed && cell.canStand)
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
        startCell.canStand = true;
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
                            var nCell = new UnitMovementCell(cell.moveLength + 1, vec, cell, !controller.IsPathable(vec));
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

                    if(!controller.IsPathable(cell.position) || cell.position == startCell.position)
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

        public bool canStand;

        public UnitMovementCell(int moveLength, Vector3Int position)
        {
            this.moveLength = moveLength;
            this.position = position;
            this.parent = null;
        }

        public UnitMovementCell(int moveLength, Vector3Int position, UnitMovementCell parent, bool canStand)
        {
            this.moveLength = moveLength;
            this.position = position;
            this.parent = parent;
            this.canStand = canStand;
        }
    }
}
