using System;
using System.Collections.Generic;
using UnityEngine;
namespace Assets.Scripts.Debug
{
    public class Debug_FlipTurnOnPress : MonoBehaviour
    {
        public bool isPlayerTurn;

        public TurnOrderController TurnOrderController;
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (isPlayerTurn)
                {
                    TurnOrderController.EndPlayerTurn();
                    isPlayerTurn = !isPlayerTurn;
                }
                else
                {
                    TurnOrderController.StartPlayerTurn();
                    isPlayerTurn = !isPlayerTurn;
                }
            }

        }
    }
}
