using System;
using System.Collections.Generic;
using UnityEngine;
public class GameWorldCanvasSetter : MonoBehaviour
{
    [SerializeField]
    Canvas thisCanvas;

    [SerializeField]
    GameWorldUIController GameWorldUIController;

    private void Awake()
    {
        GameWorldUIController.SetWorldCanvas(thisCanvas);
    }
}

