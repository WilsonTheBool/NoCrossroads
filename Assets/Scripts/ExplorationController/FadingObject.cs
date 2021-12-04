using System;
using System.Collections.Generic;
using UnityEngine;

public class FadingObject : MonoBehaviour
{
    public Color FadingColor;
    public Color NormalColor;

    SpriteRenderer SpriteRenderer;

    private void Awake()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Fade()
    {
        SpriteRenderer.color = FadingColor;
    }

    public void UnFade()
    {
        SpriteRenderer.color = NormalColor;
    }


}

