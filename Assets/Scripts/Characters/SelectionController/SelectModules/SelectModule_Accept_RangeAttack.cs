using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;

[CreateAssetMenu(menuName = "GameObjects/SelectModule/(Range) Attack On Accept ")]
public class SelectModule_Accept_RangeAttack : SelectModule
{

    public TileBase attackAreaTile;
    public TileBase moveAreaTile;

    //public Texture2D cursorSprite_attack;
    //public Texture2D cursorSprite_default;

    public override void OnSelect_AcceptPressed(GameInputData inputData, SelectEventArgs selectEventArgs)
    {
        if (selectEventArgs.SpecialTilemapManager.specialTilemap.GetTile(inputData.tileMousePosition) == attackAreaTile && selectEventArgs.MovingCharacter.movePoints > 0)
        {
            selectEventArgs.AttackingCharacter.Attack(inputData.tileMousePosition);

            selectEventArgs.MovingCharacter.movePoints = 0;
        }
    }

    public override void OnSelect_Update(GameInputData inputData, SelectEventArgs selectEventArgs)
    {
        if (selectEventArgs.SpecialTilemapManager.specialTilemap.GetTile(inputData.tileMousePosition) == attackAreaTile && selectEventArgs.MovingCharacter.movePoints > 0)
        {
            selectEventArgs.GameWorldMapManager.pathTilemap.pathTilemap.ClearAllTiles();
        }

    }

}
