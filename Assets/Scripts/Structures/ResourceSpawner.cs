using UnityEngine;
using System.Collections;

public class ResourceSpawner : MonoBehaviour
{
    public ResourceTile ResourcePrefab;

    private void Awake()
    {
        Instantiate(ResourcePrefab, this.transform.position, Quaternion.Euler(0, 0, 0)).GetComponent<WorldObject>().SetUp();
    }
}
