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
    
}
