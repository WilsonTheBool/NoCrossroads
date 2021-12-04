using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class King_PlacementController : UnitPlacementController
{
    [SerializeField]
    TileBase kingRangeTiles;

    [SerializeField]
    int kingTerritoryRange;

    public override void OnPlacementUpdate(GameInputData data, SpecialTilemapManager specialTilemapManager, GameWorldMapManager gameWorldMapManager)
    {
        specialTilemapManager.aboveSpecialTilemap.ClearAllTiles();
        specialTilemapManager.specialTilemap.SetTile(data.oldTileMousePosition, null);

        

        if (PlacableObject.CanSpawn(data.tileMousePosition, gameWorldMapManager))
        {

            foreach (Vector3Int vec in MathAdd.GetAllPositionInCircle(data.tileMousePosition, kingTerritoryRange))
            {
                specialTilemapManager.aboveSpecialTilemap.SetTile(vec, kingRangeTiles);
            }

            specialTilemapManager.DrawTile_CanPlaceTile(data.tileMousePosition);
        }
            
        else
            specialTilemapManager.DrawTile_NoPlaceTile(data.tileMousePosition);

       
        

    }

    public override void OnPlacementEnd(SpecialTilemapManager specialTilemapManager, GameWorldMapManager gameWorldMapManager)
    {
        specialTilemapManager.ClearTilemap();
        specialTilemapManager.aboveSpecialTilemap.ClearAllTiles();
    }
}

