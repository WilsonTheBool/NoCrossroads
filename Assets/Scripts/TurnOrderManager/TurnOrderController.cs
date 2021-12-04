using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "GameObjects/TurnOrderController")]
public class TurnOrderController : ScriptableObject
{

    public void StartPlayerTurn()
    {
        OnTurnStarted?.Invoke(this, null);
    }

    public void EndPlayerTurn()
    {
        OnTurnEnded?.Invoke(this, null);
    }

    public event System.EventHandler OnTurnStarted;
    public event System.EventHandler OnTurnEnded;
}
