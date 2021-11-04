using UnityEngine;
using System.Collections;


public class GameResourceManager : MonoBehaviour
{
    public static GameResourceManager instance;

    public ResourceHolder[] resourceHolders;

    public UnityEngine.Events.UnityEvent ResourcesChanged;

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
