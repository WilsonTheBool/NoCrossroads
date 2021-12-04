using UnityEngine;
using System.Collections;

public class HiddenObject : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer[] spritesToHide;

    public void Hide()
    {
        foreach(SpriteRenderer spriteRenderer in spritesToHide)
        {
            spriteRenderer.enabled = false;
        }
    }

    public void UnHide()
    {
        foreach (SpriteRenderer spriteRenderer in spritesToHide)
        {
            spriteRenderer.enabled = true;
        }
    }
}
