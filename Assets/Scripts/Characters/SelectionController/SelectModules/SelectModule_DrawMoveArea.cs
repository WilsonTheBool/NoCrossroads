using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "GameObjects/SelectModule/Draw Move Area")]
public class SelectModule_DrawMoveArea : SelectModule
{
    public TileBase tileToDraw;

    public override void OnSelect_Start(GameInputData inputData, SelectEventArgs selectEventArgs)
    {
        GameUnitMovementController gameUnitMovementController = selectEventArgs.GameUnitMovementController;
        SpecialTilemapManager specialTilemapManager = selectEventArgs.SpecialTilemapManager;

        Vector3Int[] movePos = gameUnitMovementController.GetMovementCircle(inputData.tileMousePosition, selectEventArgs.MovingCharacter.movePoints);

        foreach(Vector3Int pos in movePos)
        {
            specialTilemapManager.DrawTile(pos, tileToDraw);
        }
        
    }

    public override void OnSelect_End(GameInputData inputData, SelectEventArgs selectEventArgs)
    {
        selectEventArgs.SpecialTilemapManager.ClearTilemap();
    }
}

