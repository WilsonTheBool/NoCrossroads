using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "TilemapData/TileRules/Is Land Rule")]
public class SpawnRule_IsLand : TileSpawnRule_SO
{

    public override bool CanSpawnTile(Vector3Int pos, GameWorldMapManager gameWorldMap)
    {
        return gameWorldMap.LandTilemapManager.IsLandTile(pos);
    }
}
