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

        CreateTimer = controller.GetTimer(CreateTurnDelay);
        DestroyTimer = controller.GetTimer(CreateTurnDelay + DestroyTurnDelay);

        CreateTimer.OnEnd += CreateTimer_OnEnd;
        CreateTimer.OnStart += CreateTimer_OnStart;

        DestroyTimer.OnEnd += DestroyTimer_OnEnd;
    }

    private void DestroyTimer_OnEnd()
    {
        RemoveTarget();
        CreateTimer.Reset();
        DestroyTimer.Reset();
    }

    public override void TickAction()
    {
        CreateTimer.TurnTimer_OnTick();
        DestroyTimer.TurnTimer_OnTick();
    }

    private void AddNewTarget(Vector3Int pos)
    {
        nest.curentTarget = new AiAgentNest.NestTarget() { targetPos = pos, maxAgents = TargetMaxAgentCount , canDefendersJoin = CanDefenderJoin };
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
