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

    private float workEffectiveness = 1;

    public void Free_Resource()
    {
        resourceTile.SetIsFree(true);
    }

    public void Mine()
    {
        resourceManager.AddResource(resourceTile.resource, resourceTile.GetMineAmmount(workEffectiveness));
    }

    public GameResourceManager.ResourceHolder GetMineAmmount()
    {
        return new GameResourceManager.ResourceHolder() { data = resourceTile.resource, value = resourceTile.GetMineAmmount(workEffectiveness) };

    }  

    public void MultWorkEffectiveness(float value )
    {
        if (gameObject.activeSelf)
        {
            var holder = GetMineAmmount();
            resourceManager.AddResourcePerTurn(holder.data, -holder.value);

            workEffectiveness *= value;

            holder = GetMineAmmount();
            resourceManager.AddResourcePerTurn(holder.data, holder.value);
        }
        else
        {
            workEffectiveness *= value;
        }

    }

    private void Awake()
    {
        WorldObject = GetComponent<WorldObject>();
        TurnBaseObject = GetComponent<TurnBaseObject>();
        Structure = GetComponent<Structure>();

        WorldObject.OnSetUpComplete.AddListener(SetUp);
        
        // TurnBaseObject.OnTurnEnd.AddListener(Mine);

        //WorldObject.SetUp();
    }

    void SetUp()
    {
        gameWorldMapManager = GameWorldMapManager.instance;
        resourceManager = GameResourceManager.instance;
        if (!gameWorldMapManager.TryGetResourceTIle(WorldObject.worldPosition, out resourceTile))
        {
            Debug.LogError("Mine structure created not on resource tile");
        }

        resourceTile.SetIsFree(false);
        resourceTile.ResourceIconController.ShowIcon(false);
    }

    private void OnDestroy()
    {
        resourceTile.isFree = true;
    }
}

