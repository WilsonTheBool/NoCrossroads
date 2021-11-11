using UnityEngine;
using System.Collections;

public class Worker_PlacementController : UnitPlacementController
{
    public override void OnPlacementStart(SpecialTilemapManager specialTilemapManager, GameWorldMapManager gameWorldMapManager)
    {
        foreach (Vector3Int pos in gameWorldMapManager.playerTerritory)
        {
            if (PlacableObject.CanSpawn(pos, gameWorldMapManager))
            {
                specialTilemapManager.DrawTile_CanPlaceTile(pos);
            }
            else
            {
                specialTilemapManager.DrawTile_NoPlaceTile(pos);
            }
        }
    }

    public override void OnPlacementEnd(SpecialTilemapManager specialTilemapManager, GameWorldMapManager gameWorldMapManager)
    {
        specialTilemapManager.ClearTilemap();
    }
}
