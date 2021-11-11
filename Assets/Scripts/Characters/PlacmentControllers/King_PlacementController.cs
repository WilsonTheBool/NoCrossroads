using System;
using System.Collections.Generic;
using UnityEngine;
public class King_PlacementController : UnitPlacementController
{
    public override void OnPlacementUpdate(GameInputData data, SpecialTilemapManager specialTilemapManager, GameWorldMapManager gameWorldMapManager)
    {
        specialTilemapManager.specialTilemap.SetTile(data.oldTileMousePosition, null);

        if (PlacableObject.CanSpawn(data.tileMousePosition, gameWorldMapManager))
            specialTilemapManager.DrawTile_CanPlaceTile(data.tileMousePosition);
        else
            specialTilemapManager.DrawTile_NoPlaceTile(data.tileMousePosition);



    }

    public override void OnPlacementEnd(SpecialTilemapManager specialTilemapManager, GameWorldMapManager gameWorldMapManager)
    {
        specialTilemapManager.ClearTilemap();
    }
}

