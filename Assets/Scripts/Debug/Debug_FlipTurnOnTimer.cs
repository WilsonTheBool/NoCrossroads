using UnityEngine;
using System.Collections;

public class Debug_FlipTurnOnTimer : MonoBehaviour
{
    public float swapTimer;

    public bool isPlayer = true;

    public TurnOrderController TurnOrderController;

    void Start()
    {
        TurnOrderController.OnTurnStarted += TurnOrderController_OnTurnStarted;
        StartCoroutine(SwapTurnCo());
    }

    private void TurnOrderController_OnTurnStarted(object sender, System.EventArgs e)
    {
        StartCoroutine(SwapTurnCo());
    }

    private void OnDestroy()
    {
        TurnOrderController.OnTurnStarted -= TurnOrderController_OnTurnStarted;
        StopAllCoroutines();
    }

    IEnumerator SwapTurnCo()
    {
        yield return new WaitForSeconds(swapTimer);

        TurnOrderController.EndPlayerTurn();
    }
}
