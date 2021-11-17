using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "GameObjects/SelectModule/Attack On Accept")]
public class SelectModule_Accept_Attack : SelectModule
{
    public TileBase attackAreaTile;
    public TileBase moveAreaTile;

    public override void OnSelect_AcceptPressed(GameInputData inputData, SelectEventArgs selectEventArgs)
    {
        if (selectEventArgs.SpecialTilemapManager.specialTilemap.GetTile(inputData.tileMousePosition) == attackAreaTile)
        {
            selectEventArgs.AttackingCharacter.Attack(inputData.tileMousePosition);

            Vector3Int movePos = GetMovePosition(inputData.tileMousePosition, selectEventArgs.MovingCharacter.UnitMovementData.GetMovementArea(), inputData, selectEventArgs);

            selectEventArgs.MovingCharacter.Move(movePos, 0);

            selectEventArgs.MovingCharacter.movePoints = 0;
        }
    }

    public override void OnSelect_Update(GameInputData inputData, SelectEventArgs selectEventArgs)
    {
        if (selectEventArgs.SpecialTilemapManager.specialTilemap.GetTile(inputData.tileMousePosition) == attackAreaTile)
        {
            Vector3Int movePos;
            movePos = GetMovePosition(inputData.tileMousePosition, selectEventArgs.MovingCharacter.UnitMovementData.GetMovementArea(), inputData, selectEventArgs);
            selectEventArgs.GameWorldMapManager.pathTilemap.pathTilemap.ClearAllTiles();
            selectEventArgs.GameWorldMapManager.pathTilemap.SetPathTiles(selectEventArgs.MovingCharacter.UnitMovementData.GetPath(movePos));
        }

       

    }

    public Vector3Int GetMovePosition(Vector3Int attackPos, Vector3Int[] movingArea, GameInputData inputData, SelectEventArgs selectEventArgs)
    {
        Vector3 mousePos = inputData.worldMousePosition;
        Vector3Int bestPos;


        float minDistace = float.MaxValue;
        bestPos = new Vector3Int(-1, -1, -1);

        foreach (Vector3Int vec in movingArea)
        {
            float distance = Vector3Int.Distance(vec, attackPos);
            if (distance <= 1)
            {
                float worldDistance = Vector3.Distance(selectEventArgs.GameWorldMapManager.GetTileCenterInWorld(vec), mousePos);
                if (worldDistance < minDistace)
                {
                    minDistace = worldDistance;
                    bestPos = vec;
                }
            }
        }

        
        return bestPos;
    }
}
