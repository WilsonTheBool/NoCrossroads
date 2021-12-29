using UnityEngine;
using UnityEditor;

[CreateAssetMenu(menuName = "TilemapData/TileRules/X Tiles From Enemy units")]
public class SpawnRule_Is_X_TillesFromEnemy : TileSpawnRule_SO
{
    public int Range;

    public override bool CanSpawnTile(Vector3Int pos, GameWorldMapManager gameWorldMap)
    {
        return !gameWorldMap.GameWorld_EnemyPositionController.IsInRangeOfMonster(Range, pos);
    }
}