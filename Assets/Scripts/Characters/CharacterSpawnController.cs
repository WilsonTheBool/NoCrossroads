using UnityEngine;
using System.Collections;

public class CharacterSpawnController : MonoBehaviour
{
    [SerializeField]
    GameObject smokePrefab;

    [SerializeField]
    float destroySmokeDelay;

    public void OnSpawn()
    {
        StartCoroutine(DeathCo());
    }

    IEnumerator DeathCo()
    {
        GameObject smoke = Instantiate(smokePrefab, this.transform);

        yield return new WaitForSeconds(destroySmokeDelay);

        Destroy(smoke);
    }

    public void Start()
    {
        OnSpawn();
    }
}
