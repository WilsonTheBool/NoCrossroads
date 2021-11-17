using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;

[CreateAssetMenu(menuName = "GameObjects/SelectModule/Draw Melee attack area")]
public class SelectModule_DrawMoveAndMeleeAttack : SelectModule
{
    public TileBase tileToDraw;
    public TileBase tileToDraw_attack;

    public override void OnSelect_Start(GameInputData inputData, SelectEventArgs selectEventArgs)
    {
        SpecialTilemapManager specialTilemapManager = selectEventArgs.SpecialTilemapManager;
        selectEventArgs.MovingCharacter.UnitMovementData.SetUp(selectEventArgs.NewGameMovementController,
            inputData.tileMousePosition, selectEventArgs.MovingCharacter.movePoints);
        Vector3Int[] movePos = selectEventArgs.MovingCharacter.UnitMovementData.GetMovementArea();
        Vector3Int[] attackPos = selectEventArgs.MovingCharacter.UnitMovementData.GetPossiableAttackTiles();

        foreach (Vector3Int pos in movePos)
        {
            specialTilemapManager.DrawTile(pos, tileToDraw);
        }

        foreach (Vector3Int pos in attackPos)
        {
            foreach(WorldObject worldObject in selectEventArgs.GameWorldMapManager.GetAllWorldObjectsOnPosition(pos))
            {

                if(worldObject.TryGetComponent<KillableCharacter>(out KillableCharacter killableCharacter))
                {
                    if (selectEventArgs.AttackingCharacter.CanTarget(killableCharacter))
                    {
                        specialTilemapManager.DrawTile(pos,tileToDraw_attack);
                    }
                }
            }
        }

    }

    public override void OnSelect_End(GameInputData inputData, SelectEventArgs selectEventArgs)
    {
        selectEventArgs.SpecialTilemapManager.ClearTilemap();
    }
}
