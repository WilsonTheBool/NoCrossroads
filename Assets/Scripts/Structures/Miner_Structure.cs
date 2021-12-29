using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(WorldObject), typeof(TurnBaseObject), typeof(Structure))]
public  class Miner_Structure: MonoBehaviour
{
    public WorldObject WorldObject;


    GameResourceManager resourceManager;

    public ResourceTile resourceTile;

    GameWorldMapManager gameWorldMapManager;

    public float workEffectiveness = 1;

    public ResourceSpawner ResourceSpawner;

    public UnityEvent OnResourceTileEmpty;
    public UnityEvent OnMine;

    public void Free_Resource()
    {
        resourceTile.SetIsFree(true);
    }

    public void Mine()
    {
        float value = resourceTile.Mine(resourceTile.GetMineAmmount(workEffectiveness));
        resourceManager.AddResource(resourceTile.resource, value);
        OnMine.Invoke();
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


        WorldObject.OnSetUpComplete.AddListener(SetUp);
        
        // TurnBaseObject.OnTurnEnd.AddListener(Mine);

        //WorldObject.SetUp();
    }

    void SetUp()
    {
        gameWorldMapManager = GameWorldMapManager.instance;
        resourceManager = GameResourceManager.instance;
        if(ResourceSpawner != null)
        {
            resourceTile = ResourceSpawner.Spawn();
        }
        else
        if (!gameWorldMapManager.TryGetResourceTIle(WorldObject.worldPosition, out resourceTile))
        {
            Debug.LogError("Mine structure created not on resource tile");
        }

        resourceTile.SetIsFree(false);
        resourceTile.ResourceIconController.SetIcon(false);
        resourceTile.OnEmpty.AddListener(OnEmpty);
    }

    void OnEmpty()
    {
        OnResourceTileEmpty.Invoke();
    }

    private void OnDestroy()
    {
        resourceTile.isFree = true;
    }
}

