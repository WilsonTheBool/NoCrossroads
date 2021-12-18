using UnityEngine;
using System.Collections;

public class ResourceSpawner : MonoBehaviour
{
    public ResourceTile ResourcePrefab;

    public WorldObject WorldObject;



    public ResourceTile Spawn()
    {
        ResourceTile tile =  Instantiate(ResourcePrefab, this.transform.position, Quaternion.Euler(0, 0, 0));

        WorldObject.OnRemoveFromWorld.AddListener(tile.EmptyTile);

        return tile;
    }
}
