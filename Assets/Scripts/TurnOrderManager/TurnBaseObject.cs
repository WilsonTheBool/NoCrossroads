using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class TurnBaseObject : MonoBehaviour
{
    [SerializeField]
    private TurnOrderController controller;

    public UnityEvent OnNewTurn;

    public UnityEvent OnTurnEnd;

    private void Start()
    {
        controller.OnTurnStarted += Controller_OnTurnStarted;
        controller.OnTurnEnded += Controller_OnTurnEnded;
    }

    private void Controller_OnTurnEnded(object sender, System.EventArgs e)
    {
        OnTurnEnd.Invoke();
    }

    private void Controller_OnTurnStarted(object sender, System.EventArgs e)
    {
        OnNewTurn?.Invoke();
    }
}
