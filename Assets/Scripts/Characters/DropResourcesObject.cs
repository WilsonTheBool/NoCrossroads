using UnityEngine;
using System.Collections;

public class DropResourcesObject : MonoBehaviour
{
    public GameResourceManager.ResourceHolder[] resourcesToDrop;

    public GameResourceManager GameResourceManager;

    private void Start()
    {
        GameResourceManager = GameResourceManager.instance;
    }

    public void DropResources()
    {
        foreach(GameResourceManager.ResourceHolder resource in resourcesToDrop)
        {
            GameResourceManager.AddResource(resource.data, resource.value);
        }
        
    }
}
