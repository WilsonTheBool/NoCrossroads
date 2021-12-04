using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;

public class SpecialTilemapManager : MonoBehaviour
{
    public Tilemap specialTilemap;

    public Tilemap aboveSpecialTilemap;

    [SerializeField]
    SpecialTilesData_SO SpecialTilesData_SO;

    public void DrawTile_CanPlaceTile(Vector3Int position)
    {
        specialTilemap.SetTile(position, SpecialTilesData_SO.CanPlaceTile);
    }

    public void DrawTile(Vector3Int position, TileBase tile)
    {
        specialTilemap.SetTile(position, tile);
    }

    public void DrawTile_NoPlaceTile(Vector3Int position)
    {
        specialTilemap.SetTile(position, SpecialTilesData_SO.NotPlaceTIle);
    }

    public void ClearTilemap()
    {
        specialTilemap.ClearAllTiles();
    }

    public bool TilemapHasTile_CanPlaceTIle(Vector3Int pos)
    {
        return specialTilemap.GetTile(pos) == SpecialTilesData_SO.CanPlaceTile;
    }

    public bool TilemapHasTile_CanNotPlaceTIle(Vector3Int pos)
    {
        return specialTilemap.GetTile(pos) == SpecialTilesData_SO.NotPlaceTIle;
    }
}
