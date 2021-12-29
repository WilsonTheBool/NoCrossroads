using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;
using System.Collections.Generic;

public class TerritoryTilemapManager : MonoBehaviour
{
    public Tilemap territoryTilemap;

    public SpecialTilesData_SO tiles;

    GameWorldMapManager map;
    private void Awake()
    {
        if (territoryTilemap == null)
        {
            foreach (Tilemap tilemap in FindObjectsOfType<Tilemap>())
            {
                if (tilemap.CompareTag("TerritoryTilemap"))
                {
                    territoryTilemap = tilemap;
                    return;
                }
            }
        }
    }
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

    public void AddNewTerritory_Player(Vector3Int pos)
    {
        territoryTilemap.SetTile(pos, tiles.playerTerritioryTile);
    }

    public void AddNewTerritory_Enemy(Vector3Int[] positions)
    {
        foreach (Vector3Int pos in positions)
        {
            territoryTilemap.SetTile(pos, tiles.enemyTerritoryTile);
        }
    }

    public void AddNewTerritory_Enemy(Vector3Int pos)
    {
        territoryTilemap.SetTile(pos, tiles.enemyTerritoryTile);
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
