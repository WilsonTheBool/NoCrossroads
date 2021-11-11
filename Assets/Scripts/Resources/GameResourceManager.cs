using UnityEngine;
using System.Collections;


public class GameResourceManager : MonoBehaviour
{
    public static GameResourceManager instance;

    public ResourceHolder[] resourceHolders;

    public ResourceHolder[] resourceChangePerTurn;

    public UnityEngine.Events.UnityEvent ResourcesChanged;

    private GameWorldMapManager GameWorldMapManager;


    private void Awake()
    {
        if(instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        GameWorldMapManager = GameWorldMapManager.instance;
        GameWorldMapManager.OnUnitCreate += GameWorldMapManager_OnUnitCreate;
        GameWorldMapManager.OnUnitDeath += GameWorldMapManager_OnUnitDeath;
    }

    private void GameWorldMapManager_OnUnitDeath(object sender, GameWorldMapManager.UnitEventArgs e)
    {
        if (e.worldObject != null)
        {
            Spending_Structure spending_Structure = e.worldObject.GetComponent<Spending_Structure>();
            if (spending_Structure != null)
            {
                foreach (ResourceHolder holder in spending_Structure.resourcesSpendPerTurn)
                {
                    ResourceHolder found = TryFindHolder_PerTurn(holder.data);

                    found.value += holder.value;
                }
            }

            Miner_Structure miner_Structure = e.worldObject.GetComponent<Miner_Structure>();
            if (miner_Structure != null)
            {
                ResourceHolder holder = miner_Structure.GetMineAmmount();
                ResourceHolder found = TryFindHolder_PerTurn(holder.data);

                found.value -= holder.value;
            }
        }
    }

    private void GameWorldMapManager_OnUnitCreate(object sender, GameWorldMapManager.UnitEventArgs e)
    {
        if(e.worldObject != null)
        {
            Spending_Structure spending_Structure = e.worldObject.GetComponent<Spending_Structure>();
            if(spending_Structure != null)
            {
                foreach(ResourceHolder holder in spending_Structure.resourcesSpendPerTurn)
                {
                    AddResourcePerTurn(holder.data, -holder.value);
                }
            }

            Miner_Structure miner_Structure = e.worldObject.GetComponent<Miner_Structure>();
            if(miner_Structure != null)
            {
                ResourceHolder holder = miner_Structure.GetMineAmmount();
                AddResourcePerTurn(holder.data, holder.value);
            }
        }
    }

    public float GetResourceCount(ResourceData_SO res)
    {
        ResourceHolder holder = TryFindHolder(res);

        if(holder != null)
        {
            return holder.value;
        }
        else
        {
            return -1;
        }
    }

    public float GetResourcePerTurn(ResourceData_SO res)
    {
        ResourceHolder holder = TryFindHolder_PerTurn(res);

        if (holder != null)
        {
            return holder.value;
        }
        else
        {
            return 0;
        }
    }

    public void AddResourcePerTurn(ResourceData_SO resource, float ammount)
    {
        foreach (ResourceHolder holder in resourceChangePerTurn)
        {
            if (holder.data == resource)
            {
                holder.Add(ammount);
                ResourcesChanged?.Invoke();
            }
        }
    }

    public void AddResource(ResourceData_SO resource, float ammount)
    {
        foreach(ResourceHolder holder in resourceHolders)
        {
            if(holder.data == resource)
            {
                holder.Add(ammount);
                ResourcesChanged?.Invoke();
            }
        }
    }

    public void RemoveResource(ResourceData_SO resource, float ammount)
    {
        foreach (ResourceHolder holder in resourceHolders)
        {
            if (holder.data == resource)
            {
                holder.Remove(ammount);
                ResourcesChanged?.Invoke();
            }
        }
    }

    public bool HasMoreThanXResource(ResourceData_SO resource, float ammount)
    {
        foreach (ResourceHolder holder in resourceHolders)
        {
            if (holder.data == resource)
            {
                return (holder.value >= ammount);

            }
        }

        return false;
    }

    private ResourceHolder TryFindHolder(ResourceData_SO res)
    {
        foreach(ResourceHolder holder in resourceHolders)
        {
            if(holder.data == res)
            {
                return holder;
            }
        }

        return null;
    }

    private ResourceHolder TryFindHolder_PerTurn(ResourceData_SO res)
    {
        foreach (ResourceHolder holder in resourceChangePerTurn)
        {
            if (holder.data == res)
            {
                return holder;
            }
        }

        return null;
    }

    [System.Serializable] public class ResourceHolder
    {
        public ResourceData_SO data;
        public float value;

        public void Add(float ammount)
        {
            value += ammount;
        }

        public void Remove(float ammount)
        {
            value -= ammount;
        }
    }
}
