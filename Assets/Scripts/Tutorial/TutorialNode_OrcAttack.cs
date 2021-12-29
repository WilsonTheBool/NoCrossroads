using UnityEngine;
using System.Collections;

public class TutorialNode_OrcAttack : TutorialNode
{
    public MovingCharacter knight;

    public KillableCharacter[] orcsToKill;

    public WorldObject[] finishTiles;

    public int OrcNumberToKill;
    private int curentOrcKillCount;

    public string killOrcsObjectivetext;

    public TurnOrderController TurnOrderController;

    public bool isPlayerTurn;

    public override void OnStart()
    {
        base.OnStart();
        TurnOrderController.OnTurnStarted += TurnOrderController_OnTurnStarted1;
        TurnOrderController.OnTurnEnded += TurnOrderController_OnTurnEnded;
        foreach (KillableCharacter orcToKill in orcsToKill)
        {
            orcToKill.OnDeath.AddListener(OnOrcDeath);
        }
        
        knight.OnMove += Knight_OnMove;

        //infoWindow?.SetActive(true);
    }

    private void TurnOrderController_OnTurnEnded(object sender, System.EventArgs e)
    {
        isPlayerTurn = false;
    }

    private void TurnOrderController_OnTurnStarted1(object sender, System.EventArgs e)
    {
        isPlayerTurn = true;
    }

    private void Knight_OnMove(object sender, MovingCharacter.MoveEventArg e)
    {
        foreach (WorldObject tile in finishTiles)
        {
            if (tile.worldPosition == e.newPos)
            {
                SpawnOrcs();

                TurnOrderController.EndPlayerTurn();

                curentObjective.text = killOrcsObjectivetext;

            }
        }
    }

    public void SpawnOrcs()
    {
        foreach(WorldObject world in finishTiles)
        {
            world.GetComponent<CharacterDeathController>().OnDeath_Destroy();
        }

        foreach(KillableCharacter killableCharacter in orcsToKill)
        {
            killableCharacter.gameObject.SetActive(true);
        }
    }

    private void OnOrcDeath()
    {
        curentOrcKillCount++;
        
        if(curentOrcKillCount >= OrcNumberToKill)
        {
            TurnOrderController.EndPlayerTurn();
            TurnOrderController.OnTurnStarted += TurnOrderController_OnTurnStarted;
        }
       
    }

    private void TurnOrderController_OnTurnStarted(object sender, System.EventArgs e)
    {
        isCompleted = true;
        tutorialController.StartNextNode();
    }

    public override void OnEnd()
    {
        base.OnEnd();
        knight.OnMove -= Knight_OnMove;
        TurnOrderController.OnTurnEnded -= TurnOrderController_OnTurnEnded;
        TurnOrderController.OnTurnStarted -= TurnOrderController_OnTurnStarted;
        TurnOrderController.OnTurnStarted -= TurnOrderController_OnTurnStarted1;
    }
}
