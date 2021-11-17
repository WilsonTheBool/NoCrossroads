using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "TilemapData/TileRules/Is Free Territory Rule")]
public class SpawnRule_IsFreeTerritory : TileSpawnRule_SO
{
    public override bool CanSpawnTile(Vector3Int pos, GameWorldMapManager gameWorldMap)
    {
        return !gameWorldMap.TerritoryTilemapManager.IsPlayerTerritory(pos) && !gameWorldMap.TerritoryTilemapManager.IsEnemyTerritory(pos);
    }
}
