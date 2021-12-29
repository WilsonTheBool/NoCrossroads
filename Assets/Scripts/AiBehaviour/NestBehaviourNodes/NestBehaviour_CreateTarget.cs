using UnityEngine;
using System.Collections;

public class NestBehaviour_CreateTarget: NestBehaviourNode
{
    public int CreateTurnDelay;

    public int DestroyTurnDelay;

    public int TargetMaxAgentCount;

    [HideInInspector]
    public NoizeGameWorldController NoizeGameWorldController;

    
    public TurnTimerController controller;

    TurnTimerController.TurnTimer CreateTimer;
    TurnTimerController.TurnTimer DestroyTimer;

    public AiAgentNest nest;

    public bool CanDefenderJoin;

    private void Start()
    {
        NoizeGameWorldController = FindObjectOfType<NoizeGameWorldController>();
        controller = TurnTimerController.instance;

        //CreateTimer = controller.GetTimer(CreateTurnDelay);
        //DestroyTimer = controller.GetTimer(CreateTurnDelay + DestroyTurnDelay);

        //CreateTimer.OnEnd += CreateTimer_OnEnd;
        //CreateTimer.OnStart += CreateTimer_OnStart;

       // DestroyTimer.OnEnd += DestroyTimer_OnEnd;
    }

    private void DestroyTimer_OnEnd()
    {
        RemoveTarget();
        DestroyTimer.OnEnd -= DestroyTimer_OnEnd;
        DestroyTimer = null;
    }

    public override void TickAction(int turnCount)
    {
        if(turnCount % CreateTurnDelay == 0 && turnCount > 0)
        {
            CreateTimer_OnEnd();
            DestroyTimer = new TurnTimerController.TurnTimer(DestroyTurnDelay, controller);
            DestroyTimer.OnEnd += DestroyTimer_OnEnd;
        }

       if(DestroyTimer != null)
        DestroyTimer.TurnTimer_OnTick();
    }

    private void AddNewTarget(Vector3Int pos)
    {
        nest.curentTarget = new AiAgentNest.NestTarget() { targetPos = pos, maxAgents = TargetMaxAgentCount, canDefendersJoin = CanDefenderJoin, isCreated = true };
    }

    private void RemoveTarget()
    {
        nest.curentTarget = null;
    }

    private void CreateTimer_OnStart()
    {
        
    }

    private void CreateTimer_OnEnd()
    {
        var target = NoizeGameWorldController.GetNearestNoizeStructure(nest.WorldObject.worldPosition);
        
        if(target != null)
        {
            if (Vector3.Distance(target.gameObject.transform.position, nest.gameObject.transform.position) <= nest.nestAgroRange)
            {
                AddNewTarget(target.WorldObject.worldPosition);
            }
        }
        
    }
}
