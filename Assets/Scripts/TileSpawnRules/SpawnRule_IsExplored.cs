using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "TilemapData/TileRules/Is Explored")]
public class SpawnRule_IsExplored : TileSpawnRule_SO
{

    public override bool CanSpawnTile(Vector3Int pos, GameWorldMapManager gameWorldMap)
    {
        return GameWorld_ExplorationController.instance.IsExploredFromGlobal(pos);
    }
}
