using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "GameObjects/SelectModule/Move On Accept")]
public class SelectModule_Accept_Move : SelectModule
{
    public TileBase moveAreaTile;

    
    public override void OnSelect_AcceptPressed(GameInputData inputData, SelectEventArgs selectEventArgs)
    {
        if(selectEventArgs.SpecialTilemapManager.specialTilemap.GetTile(inputData.tileMousePosition) == moveAreaTile)
        {
            selectEventArgs.MovingCharacter.Move(inputData.tileMousePosition, selectEventArgs.MovingCharacter.UnitMovementData.GetPathCost(inputData.tileMousePosition));
        }
    }

    public override void OnSelect_Update(GameInputData inputData, SelectEventArgs selectEventArgs)
    {
        if (selectEventArgs.SpecialTilemapManager.specialTilemap.GetTile(inputData.tileMousePosition) == moveAreaTile)
        {
            selectEventArgs.GameWorldMapManager.pathTilemap.pathTilemap.ClearAllTiles();
            selectEventArgs.GameWorldMapManager.pathTilemap.SetPathTiles(selectEventArgs.MovingCharacter.UnitMovementData.GetPath(inputData.tileMousePosition));
        }
       
        
        
    }

    public override void OnSelect_End(GameInputData inputData, SelectEventArgs selectEventArgs)
    {
        selectEventArgs.GameWorldMapManager.pathTilemap.pathTilemap.ClearAllTiles();
        
    }
}


