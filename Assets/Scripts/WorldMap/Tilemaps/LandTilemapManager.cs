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
        return LandTIlesData_SO.IsLandTile(landTIlemap.GetTile(pos));
    }
}

