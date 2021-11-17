using System;
using UnityEngine;

[RequireComponent(typeof(WorldObject), typeof(TurnBaseObject), typeof(Structure))]
public  class Miner_Structure: MonoBehaviour
{
    public WorldObject WorldObject;

    TurnBaseObject TurnBaseObject;

    Structure Structure;

    GameResourceManager resourceManager;

    public ResourceTile resourceTile;

    GameWorldMapManager gameWorldMapManager;

    public void Free_Resource()
    {
        resourceTile.SetIsFree(true);
    }

    public void Mine()
    {
        resourceManager.AddResource(resourceTile.resource, resourceTile.GetMineAmmount(Structure.workEffectivenessValue));
    }

    public GameResourceManager.ResourceHolder GetMineAmmount()
    {
        return new GameResourceManager.ResourceHolder() { data = resourceTile.resource, value = resourceTile.GetMineAmmount(Structure.workEffectivenessValue) };

    }  

    private void Start()
    {
        WorldObject = GetComponent<WorldObject>();
        TurnBaseObject = GetComponent<TurnBaseObject>();
        Structure = GetComponent<Structure>();
        gameWorldMapManager = GameWorldMapManager.instance;
        resourceManager = GameResourceManager.instance;
        if (!gameWorldMapManager.TryGetResourceTIle(gameWorldMapManager.GetTilePosition(this.transform.position), out resourceTile))
        {
            Debug.LogError("Mine structure created not on resource tile");
        }

        resourceTile.SetIsFree(false);
        resourceTile.ResourceIconController.ShowIcon(false);
        // TurnBaseObject.OnTurnEnd.AddListener(Mine);

        WorldObject.SetUp();
    }

    private void OnDestroy()
    {
        resourceTile.isFree = true;
    }
}

