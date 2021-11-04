using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Tilemaps;

public class GameWorldMapManager : MonoBehaviour
{
    public static GameWorldMapManager instance;

    public LandTilemapManager LandTilemapManager;
    public TerritoryTilemapManager TerritoryTilemapManager;
    public SpecialTilemapManager SpecialTilemapManager;

    public Tilemap mainTilemap;
    public List<ResourceTile> resourceTiles;
    public List<Miner_Structure> miners;

    public List<Vector3Int> playerTerritory;

    public List<King_Structure> kingStructures;

    public UnityEngine.Events.UnityEvent<Vector3Int[]> OnPlayerTerritoryAdd;
    public UnityEngine.Events.UnityEvent<Vector3Int[]> OnPlayerTerritoryRemove;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(this);
            return;
        }
        else
        {
            instance = this;
        }

        GetAllMinerStructuresOnMap();
        GetAllResourceTilesOnMap();

        //Change later (!!!)......................
        foreach (Vector3Int pos in TerritoryTilemapManager.territoryTilemap.cellBounds.allPositionsWithin)
        {
            if(TerritoryTilemapManager.territoryTilemap.HasTile(pos))
            playerTerritory.Add(pos);
        }
        //.....................................

        if (mainTilemap == null)
        {
            mainTilemap = FindObjectOfType<Tilemap>();
        }
    }

    private void GetAllResourceTilesOnMap()
    {
        resourceTiles = new List<ResourceTile>(FindObjectsOfType<ResourceTile>());
    }

    private void GetAllMinerStructuresOnMap()
    {
        miners = new List<Miner_Structure>(FindObjectsOfType<Miner_Structure>());
    }

    public Vector3Int GetTilePosition(Vector3 position)
    {
        return mainTilemap.WorldToCell(position);
    }

    public Vector3 GetTileCenterInWorld(Vector3Int position)
    {
        return mainTilemap.GetCellCenterWorld(position);
    }

    public bool TryGetResourceTIle(Vector3Int position, out ResourceTile tile)
    {
        foreach(ResourceTile resource in resourceTiles)
        {
            if(resource.GetPosition() == position)
            {
                tile = resource;
                return true;
            }
        }

        tile = null;
        return false;
    }

    public void AddPlayerTerritory(Vector3Int[] positions)
    {
        playerTerritory.AddRange(positions);
        TerritoryTilemapManager.AddNewTerritory_Player(positions);
        OnPlayerTerritoryAdd.Invoke(positions);
    }

    public void RemovePlayerTerritory(Vector3Int[] positions)
    {
        foreach (Vector3Int pos in positions)
        {
            playerTerritory.Remove(pos);
        }

        OnPlayerTerritoryRemove.Invoke(positions);
    }


    //Can be done by ray casting
    public bool HasResourceTile(Vector3Int pos)
    {
        foreach(ResourceTile tile in resourceTiles)
        {
            if(tile.worldObject.worldPosition == pos)
            {
                return true;
            }
        }

        return false;
    }

    public bool TryGetNearestKingPosition(Vector3Int startPos, out Vector3Int kingPosition)
    {
        float minDistance = float.MaxValue;
        kingPosition = new Vector3Int(-1, -1, -1);

        if (kingStructures.Count > 0)
        {
            foreach (King_Structure king in kingStructures)
            {
                float distance = Vector3Int.Distance(startPos, king.WorldObject.worldPosition);

                if (distance < minDistance)
                {
                    minDistance = distance;
                    kingPosition = king.WorldObject.worldPosition;
                }
            }

            return true;
        }
        else
        {
            
            return false;
        }

        
    }

    public bool TryGetKing(Vector3Int position, out King_Structure king)
    {
        foreach(King_Structure k in kingStructures)
        {
            if(k.WorldObject.worldPosition == position)
            {
                king = k;
                return true;
            }
        }

        king = null;
        return false;
    }
}
