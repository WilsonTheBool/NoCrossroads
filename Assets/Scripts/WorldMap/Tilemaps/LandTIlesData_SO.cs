using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;

[CreateAssetMenu(menuName = "TilemapData/LandTilesData")]
public class LandTIlesData_SO : ScriptableObject
{
    public TileBase[] landTiles;

    public TileBase[] waterTiles;

    public bool IsLandTile(TileBase tile)
    {
        foreach(TileBase t in landTiles)
        {
            if(t == tile)
            {
                return true;
            }
        }

        return false;
    }

    public bool IsWaterTile(TileBase tile)
    {
        foreach (TileBase t in waterTiles)
        {
            if (t == tile)
            {
                return true;
            }
        }

        return false;
    }
}
