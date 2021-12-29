using UnityEngine;
using System.Collections;

public class CharacterDeathController : MonoBehaviour
{
    [SerializeField]
    GameObject smokePrefab;

    [SerializeField]
    WorldObject WorldObject;

    [SerializeField]
    SpriteRenderer sprite;
    [SerializeField]
    SpriteRenderer WeaponSprite;

    [SerializeField]
    float destroyDelay;

    [SerializeField]
    float smokeDelay;

    public void OnDeath_Destroy()
    {
        WorldObject.RemoveFromWorld();

        BoxCollider boxCollider = GetComponent<BoxCollider>();
        if(boxCollider != null)
        {
            boxCollider.enabled = false;
        }

        StartCoroutine(DeathCo());
    }

    public void OnDeath_Destroy_Instant()
    {
        WorldObject.RemoveFromWorld();

        Destroy(this.gameObject);
    }

    IEnumerator DeathCo()
    {
        
        Destroy(this.gameObject, destroyDelay);

        yield return new WaitForSeconds(smokeDelay);
        Instantiate(smokePrefab, this.transform);
        sprite.enabled = false;

        if(WeaponSprite != null)
        {
            WeaponSprite.enabled = false;
        }
    }
}
