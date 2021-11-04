using UnityEngine;
using System.Collections;

public class PlacableObject : MonoBehaviour
{
    public TileSpawnRule_SO[] spawnRules;
    public bool CanSpawn(Vector3Int pos, GameWorldMapManager map)
    {
        foreach(TileSpawnRule_SO rule in spawnRules)
        {
            if (!rule.CanSpawnTile(pos, map)) 
            {
                return false;
            }
        }

        return true;
    }
}
