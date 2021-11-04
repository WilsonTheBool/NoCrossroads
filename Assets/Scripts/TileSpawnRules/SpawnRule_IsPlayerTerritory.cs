using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.TileSpawnRules
{

    [CreateAssetMenu(menuName = "TilemapData/TileRules/Is Player Territory Rule")]
    public class SpawnRule_IsPlayerTerritory: TileSpawnRule_SO
    {
        public override bool CanSpawnTile(Vector3Int pos, GameWorldMapManager gameWorldMap)
        {
            return gameWorldMap.TerritoryTilemapManager.IsPlayerTerritory(pos);
        }
    }
}
