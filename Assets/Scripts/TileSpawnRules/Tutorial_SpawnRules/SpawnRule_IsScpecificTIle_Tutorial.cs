using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TilemapData/TileRules/Has specific tile (Tutorial)")]
public class SpawnRule_IsScpecificTIle_Tutorial : TileSpawnRule_SO
{
    public string TileName;
    public override bool CanSpawnTile(Vector3Int pos, GameWorldMapManager gameWorldMap)
    {
        foreach(WorldObject worldObject in gameWorldMap.GetAllWorldObjectsOnPosition(pos))
        {
            if(worldObject.typeName == TileName)
            {
                return true;
            }
        }

        return false;
    }
}

