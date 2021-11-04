using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;
using System.Collections.Generic;

public class TerritoryTilemapManager : MonoBehaviour
{
    public Tilemap territoryTilemap;

    public SpecialTilesData_SO tiles;

    GameWorldMapManager map;

    private void Start()
    {
        map = GameWorldMapManager.instance;
        map.OnPlayerTerritoryAdd.AddListener(AddNewTerritory_Player);
        map.OnPlayerTerritoryRemove.AddListener(RemoveTerritory);
    }

    public void AddNewTerritory_Player(Vector3Int[] positions)
    {
        foreach(Vector3Int pos in positions)
        {
            territoryTilemap.SetTile(pos, tiles.playerTerritioryTile);
        }
    }

    public void RemoveTerritory(Vector3Int[] positions)
    {
        foreach (Vector3Int pos in positions)
        {
            territoryTilemap.SetTile(pos, null);
        }
    }

    public bool IsPlayerTerritory(Vector3Int pos)
    {
        return territoryTilemap.GetTile(pos) == tiles.playerTerritioryTile;
    }
    public bool IsEnemyTerritory(Vector3Int pos)
    {
        return territoryTilemap.GetTile(pos) == tiles.enemyTerritoryTile;
    }
}
