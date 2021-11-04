using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;

public class SpecialTilemapManager : MonoBehaviour
{
    [SerializeField]
    Tilemap specialTilemap;

    [SerializeField]
    SpecialTilesData_SO SpecialTilesData_SO;

    public void DrawTile_CanPlaceTile(Vector3Int position)
    {
        specialTilemap.SetTile(position, SpecialTilesData_SO.CanPlaceTile);
    }

    public void DrawTile_NoPlaceTile(Vector3Int position)
    {
        specialTilemap.SetTile(position, SpecialTilesData_SO.NotPlaceTIle);
    }

    public void ClearTilemap()
    {
        specialTilemap.ClearAllTiles();
    }
}
