using UnityEngine;
using System.Collections;


public class PrefabSpawner : MonoBehaviour
{
    public static PrefabSpawner instance;

    [SerializeField]
    WorldObject[] prefabs;

    public WorldObject GetPrefabByTypeName(string typeName)
    {
        foreach(WorldObject worldObject in prefabs)
        {
            if(worldObject.typeName == typeName)
            {
                return worldObject;
            }
        }

        return null;
    }

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

    public WorldObject InstansiatePPrefab(string typeName ,Transform parent)
    {
        WorldObject worldObject = GetPrefabByTypeName(typeName);

        if(worldObject != null)
        {
            var instance =  Instantiate(worldObject, parent);
            instance.gameObject.SetActive(true);
            return instance;
        }
        return null;
    }

    public WorldObject InstansiatePPrefab(string typeName, Vector3 position)
    {
        WorldObject worldObject = GetPrefabByTypeName(typeName);

        if (worldObject != null)
        {
            var instance = Instantiate(worldObject, position, Quaternion.Euler(0, 0, 0));
            instance.gameObject.SetActive(true);
            return instance;
        }
        return null;
    }

    public WorldObject InstansiatePPrefab(string typeName, Transform parent, Vector3 position)
    {
        WorldObject worldObject = GetPrefabByTypeName(typeName);

        if (worldObject != null)
        {
            var instance = Instantiate(worldObject, position, Quaternion.Euler(0,0,0), parent);
            instance.gameObject.SetActive(true);
            return instance;
        }

        return null;
    }
}
