using UnityEngine;
using System.Collections;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "GameObjects/SelectModule/Draw Range attack area")]
public class SelectModule_DrawRangeAttack : SelectModule
{

    public TileBase tileToDraw;
    public TileBase tileToDraw_attack;
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
                        if (selectEventArgs.AttackingCharacter.CanTarget(killableCharacter))
                        {
                            specialTilemapManager.DrawTile(pos, tileToDraw_attack);
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
}
