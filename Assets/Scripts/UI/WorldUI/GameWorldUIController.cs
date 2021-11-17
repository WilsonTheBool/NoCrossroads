using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "World UI/Ui Controller")]
public class GameWorldUIController : ScriptableObject
{
    Canvas gameWorldCanvas;

    public Canvas GetWorldCanvas()
    {
        return gameWorldCanvas;
    }

    public void SetWorldCanvas(Canvas canvas)
    {
        gameWorldCanvas = canvas;
    }
}
