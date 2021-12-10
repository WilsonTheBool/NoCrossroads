using UnityEngine;
using System.Collections;

public class ResourceSpawner : MonoBehaviour
{
    public ResourceTile ResourcePrefab;

    public ResourceTile Spawn()
    {
        return Instantiate(ResourcePrefab, this.transform.position, Quaternion.Euler(0, 0, 0));
    }
}
