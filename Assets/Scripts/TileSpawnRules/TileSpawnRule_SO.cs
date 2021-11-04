using UnityEngine;
using System.Collections;

public class TileSpawnRule_SO : ScriptableObject
{
    public virtual bool CanSpawnTile(Vector3Int pos, GameWorldMapManager gameWorldMap)
    {
        return false;
    }

    public static bool TrueForAllRules(Vector3Int pos, GameWorldMapManager game, TileSpawnRule_SO[] rules)
    {
        foreach (TileSpawnRule_SO rule in rules)
        {
            if (!rule.CanSpawnTile(pos, game))
            {
                return false;
            }
        }

        return true;
    }
}
