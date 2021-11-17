using System;
using System.Collections.Generic;
using UnityEngine;
public class GameWorldTerritoryController : MonoBehaviour
{
    GameWorldMapManager GameWorldMapManager;

    public List<TerritoryCreator> territoryCreators;



    private void Start()
    {
        GameWorldMapManager = GameWorldMapManager.instance;
        GameWorldMapManager.OnUnitCreate += GameWorldMapManager_OnUnitCreate;
        GameWorldMapManager.OnUnitDeath += GameWorldMapManager_OnUnitDeath;

        territoryCreators.AddRange(FindObjectsOfType<TerritoryCreator>());
    }

    public void ReclaimTerritory(Vector3Int[] territory)
    {
        GameWorldMapManager.ClearTerritory(territory);

        foreach(Vector3Int pos in territory)
        {
            ReclaimTile(pos);
        }
    }

    private void ReclaimTile(Vector3Int pos)
    {
        TerritoryCreator creator = GetClosestTerritoryCreatorInRange(pos);

        if(creator != null)
        {
            creator.AddTerritory(pos);
            if (creator.isEnemy)
            {
                GameWorldMapManager.AddEnemyTerritory(pos);
            }
            else
            {
                GameWorldMapManager.AddPlayerTerritory(pos);
            }
        }
    }

    public TerritoryCreator GetClosestTerritoryCreatorInRange(Vector3Int pos)
    {
        float minDistance = 0;
        TerritoryCreator minCreator = null;
        foreach(TerritoryCreator creator in territoryCreators)
        {
            float value = Vector3Int.Distance(creator.WorldObject.worldPosition, pos) - creator.createRadius;

            if(value <= minDistance)
            {
                minDistance = value;
                minCreator = creator;
            }
        }

        return minCreator;
    }

    private void GameWorldMapManager_OnUnitDeath(object sender, GameWorldMapManager.UnitEventArgs e)
    {
        if(e.worldObject.TryGetComponent<TerritoryCreator>(out TerritoryCreator territoryCreator))
        {
            territoryCreators.Remove(territoryCreator);
            ReclaimTerritory(territoryCreator.GetAllTerritory());
        }
       
    }

    private void GameWorldMapManager_OnUnitCreate(object sender, GameWorldMapManager.UnitEventArgs e)
    {
        if (e.worldObject.TryGetComponent<TerritoryCreator>(out TerritoryCreator territoryCreator))
        {
            territoryCreators.Add(territoryCreator);
        }

        
    }
}
