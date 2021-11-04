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

    public void Mine()
    {
        resourceManager.AddResource(resourceTile.resource, resourceTile.GetMineAmmount(Structure.workEffectivenessValue));
    }

    private void Start()
    {
        WorldObject = GetComponent<WorldObject>();
        WorldObject.SetUp();
        TurnBaseObject = GetComponent<TurnBaseObject>();
        Structure = GetComponent<Structure>();
        gameWorldMapManager = GameWorldMapManager.instance;
        resourceManager = GameResourceManager.instance;
        if (!gameWorldMapManager.TryGetResourceTIle(WorldObject.worldPosition, out resourceTile))
        {
            Debug.LogError("Mine structure created not on resource tile");
        }

        resourceTile.SetIsFree(false);
       // TurnBaseObject.OnTurnEnd.AddListener(Mine);

    }

    private void OnDestroy()
    {
        resourceTile.isFree = true;
    }
}

