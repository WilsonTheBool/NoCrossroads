using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;

public class PathTilemapManager : MonoBehaviour
{
    public Tilemap pathTilemap;

    public TileBase pathTile;

    public void SetPathTiles(Vector3Int[] path)
    {
        foreach(Vector3Int vec in path)
        {
            pathTilemap.SetTile(vec, pathTile);
        }
        
    }

    public void DrawTiles(Vector3Int[] pos, TileBase tile)
    {
        foreach (Vector3Int vec in pos)
        {
            pathTilemap.SetTile(vec, tile);
        }
    }
    
}
