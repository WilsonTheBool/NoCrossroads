using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;

[CreateAssetMenu(menuName = "GameObjects/SelectModule/Draw heal area")]
public class SelectModule_DrawMoveAndHeal : SelectModule
{
    public TileBase tileToDraw;
    public TileBase tileToDraw_heal;
    public TileBase tileToDraw_attackRange;

    public int attackRange;

    public override void OnSelect_Start(GameInputData inputData, SelectEventArgs selectEventArgs)
    {
        if (selectEventArgs.MovingCharacter.movePoints > 0)
        {
            SpecialTilemapManager specialTilemapManager = selectEventArgs.SpecialTilemapManager;
            selectEventArgs.MovingCharacter.UnitMovementData.SetUp(selectEventArgs.NewGameMovementController,
                inputData.tileMousePosition, selectEventArgs.MovingCharacter.movePoints);
            Vector3Int[] movePos = selectEventArgs.MovingCharacter.UnitMovementData.GetMovementArea();
            Vector3Int[] attackPos = MathAdd.GetAllPositionInCircle(inputData.tileMousePosition, attackRange);

            foreach (Vector3Int pos in attackPos)
            {
                specialTilemapManager.aboveSpecialTilemap.SetTile(pos, tileToDraw_attackRange);
            }

            foreach (Vector3Int pos in movePos)
            {
                specialTilemapManager.DrawTile(pos, tileToDraw);
            }



            foreach (Vector3Int pos in attackPos)
            {
                foreach (WorldObject worldObject in selectEventArgs.GameWorldMapManager.GetAllWorldObjectsOnPosition(pos))
                {

                    if (worldObject.TryGetComponent<KillableCharacter>(out KillableCharacter killableCharacter))
                    {
                        if (selectEventArgs.HealingCharacter.CanTarget(killableCharacter))
                        {
                            specialTilemapManager.DrawTile(pos, tileToDraw_heal);
                        }
                    }
                }
            }
        }
    }

    public override void OnSelect_End(GameInputData inputData, SelectEventArgs selectEventArgs)
    {
        selectEventArgs.SpecialTilemapManager.ClearTilemap();
        selectEventArgs.SpecialTilemapManager.aboveSpecialTilemap.ClearAllTiles();
    }

    public override void OnSelect_AcceptPressed(GameInputData inputData, SelectEventArgs selectEventArgs)
    {
        if (selectEventArgs.SpecialTilemapManager.specialTilemap.GetTile(inputData.tileMousePosition) == tileToDraw_heal && selectEventArgs.MovingCharacter.movePoints > 0)
        {
            selectEventArgs.HealingCharacter.Heal(inputData.tileMousePosition);

            selectEventArgs.MovingCharacter.movePoints = 0;
        }
    }

    public override void OnSelect_Update(GameInputData inputData, SelectEventArgs selectEventArgs)
    {
        if (selectEventArgs.SpecialTilemapManager.specialTilemap.GetTile(inputData.tileMousePosition) == tileToDraw_heal && selectEventArgs.MovingCharacter.movePoints > 0)
        {
            selectEventArgs.GameWorldMapManager.pathTilemap.pathTilemap.ClearAllTiles();
        }

    }
}
