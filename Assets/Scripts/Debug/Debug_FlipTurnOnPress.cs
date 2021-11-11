using System;
using System.Collections.Generic;
using UnityEngine;
namespace Assets.Scripts.Debug
{
    public class Debug_FlipTurnOnPress : MonoBehaviour
    {
        public bool isPlayerTurn;

        public TurnOrderController TurnOrderController;

        public void SwapTurn()
        {
            if (isPlayerTurn)
            {
                TurnOrderController.EndPlayerTurn();
                isPlayerTurn = !isPlayerTurn;

                SwapTurn();
            }
            else
            {
                TurnOrderController.StartPlayerTurn();
                isPlayerTurn = !isPlayerTurn;
            }
        }
    }
}
