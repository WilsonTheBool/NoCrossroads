using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;

public class Guard_PlacementController : UnitPlacementController
{

    [SerializeField]
    TileBase guardRangeTiles;

    [SerializeField]
    int guardVisionRange;

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

    public override void OnPlacementUpdate(GameInputData data, SpecialTilemapManager specialTilemapManager, GameWorldMapManager gameWorldMapManager)
    {
        specialTilemapManager.aboveSpecialTilemap.ClearAllTiles();

        if (PlacableObject.CanSpawn(data.tileMousePosition, gameWorldMapManager))
        {
            foreach (Vector3Int vec in MathAdd.GetAllPositionInCircle(data.tileMousePosition, guardVisionRange))
            {
                specialTilemapManager.aboveSpecialTilemap.SetTile(vec, guardRangeTiles);
            }
        }


    }

    public override void OnPlacementEnd(SpecialTilemapManager specialTilemapManager, GameWorldMapManager gameWorldMapManager)
    {
        specialTilemapManager.ClearTilemap();
        specialTilemapManager.aboveSpecialTilemap.ClearAllTiles();
    }
}
