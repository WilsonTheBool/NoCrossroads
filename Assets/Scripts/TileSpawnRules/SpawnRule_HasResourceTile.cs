using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "TilemapData/TileRules/Has Free Resource tile")]
public class SpawnRule_HasResourceTile : TileSpawnRule_SO
{
    public override bool CanSpawnTile(Vector3Int pos, GameWorldMapManager gameWorldMap)
    {
        return gameWorldMap.TryGetResourceTIle(pos, out ResourceTile tile) && tile.isFree;
    }
}
