using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LandTilemapManager : MonoBehaviour
{
    public LandTIlesData_SO LandTIlesData_SO;

    public Tilemap landTIlemap;
    public bool IsLandTile(Vector3Int pos)
    {
        return landTIlemap.HasTile(pos) && LandTIlesData_SO.IsLandTile(landTIlemap.GetTile(pos));
    }

    public Vector3Int[] GetAllLandPositions()
    {
        List<Vector3Int> posList = new List<Vector3Int>();
        foreach(Vector3Int pos in landTIlemap.cellBounds.allPositionsWithin)
        {
            if (IsLandTile(pos))
            {
                posList.Add(pos);
            }
        }

        return posList.ToArray();
    }
}

