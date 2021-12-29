using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "TilemapData/Special tiles Data")]
public class SpecialTilesData_SO: ScriptableObject
{

    public TileBase playerTerritioryTile;

    public TileBase enemyTerritoryTile;

    public TileBase CanPlaceTile;

    public TileBase NotPlaceTIle;

    public TileBase AttackTargetTile;

}

