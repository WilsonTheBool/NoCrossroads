using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class TurnBaseObject : MonoBehaviour
{
    [SerializeField]
    private TurnOrderController controller;

    public UnityEvent OnNewTurn;
    public UnityEvent OnNewTurn_Late;

    public UnityEvent OnTurnEnd;
    public UnityEvent OnTurnEnd_Late;

    private void Start()
    {
        controller.OnTurnStarted += Controller_OnTurnStarted;
        controller.OnTurnEnded += Controller_OnTurnEnded;
    }

    private void Controller_OnTurnEnded(object sender, System.EventArgs e)
    {
        OnTurnEnd.Invoke();
        OnTurnEnd_Late.Invoke();
    }

    private void Controller_OnTurnStarted(object sender, System.EventArgs e)
    {
        OnNewTurn?.Invoke();
        OnNewTurn_Late?.Invoke();
    }
}
