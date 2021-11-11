using System;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRule_IsXTilesFromAKing : TileSpawnRule_SO
{
    public int maxDistance;

    public override bool CanSpawnTile(Vector3Int pos, GameWorldMapManager gameWorldMap)
    {
        return false;

        //return gameWorldMap.TryGetNearestKingPosition(pos, out Vector3Int kingPos) && maxDistance >= Vector3Int.Distance(pos, kingPos);
    }
}

