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

        StartCoroutine(DeathCo());
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
