using System;
using System.Collections.Generic;
using UnityEngine;
namespace Assets.Scripts.Debug
{
    public class Debug_FlipTurnOnPress : MonoBehaviour
    {
        public bool isPlayerTurn = true;

        public TurnOrderController TurnOrderController;

        private void Start()
        {
            TurnOrderController.OnTurnStarted += TurnOrderController_OnTurnStarted;
        }

        private void TurnOrderController_OnTurnStarted(object sender, EventArgs e)
        {
            isPlayerTurn = true;
        }

        public void SwapTurn()
        {
            
            if (isPlayerTurn)
            {
                TurnOrderController.EndPlayerTurn();
                isPlayerTurn = !isPlayerTurn;
            }

        }
    }
}
