using UnityEngine;
using System.Collections;

public class MaterialSwapController : MonoBehaviour
{

    public Material defaultMaterial;
    public Material swapMaterial;

    public SpriteRenderer SpriteRenderer;
    public SpriteRenderer weaponSprite;

    public void Swap_To_New()
    {
        SpriteRenderer.material = swapMaterial;

        if(weaponSprite != null)
        {
            weaponSprite.material = swapMaterial;
        }
    }

    public void Swap_To_Default()
    {
        SpriteRenderer.material = defaultMaterial;

        if (weaponSprite != null)
        {
            weaponSprite.material = defaultMaterial;
        }
    }
}
