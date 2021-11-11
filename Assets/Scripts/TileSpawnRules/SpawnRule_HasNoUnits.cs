using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TilemapData/TileRules/Has No units")]
public class SpawnRule_HasNoUnits:  TileSpawnRule_SO
{
    public override bool CanSpawnTile(Vector3Int pos, GameWorldMapManager gameWorldMap)
    {
        foreach (WorldObject worldObject in gameWorldMap.GetAllWorldObjectsOnPosition(pos))
        {
            if (worldObject.blockMovement)
            {
                return false;
            }
        }

        return true;
    }
}
