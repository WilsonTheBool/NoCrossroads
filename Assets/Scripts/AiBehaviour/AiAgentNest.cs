using UnityEngine;
using System;
using UnityEngine.Events;
using System.Collections.Generic;

public class AiAgentNest : MonoBehaviour
{
    
    public WorldObject WorldObject;

    public KillableCharacter killable;

    public Vector3Int[] nestTerritory;

    public TerritoryCreator territoryCreator;

    public AiBehaviourController behaviourController;

    GameWorldMapManager GameWorldMapManager;

    private NestBehaviourNode[] behaviourNodes;

    public NestBehaviour_SpawnAgents spawnAgents;

    [SerializeField]
    int nestRange;

    
    public float nestAgroRange;


    public UnityEvent OnNestAttacked;

    public UnityEvent OnNestCreate;
    public UnityEvent OnNestDestroy;

    public NestTarget curentTarget;

    [HideInInspector]
    public int turnCount;


    public TurnOrderController TurnOrderController;

    private void Awake()
    {
        territoryCreator.createRadius = nestRange;
        WorldObject.OnSetUpComplete.AddListener(OnWorldObjectSetUpComplete);
        behaviourNodes = GetComponents<NestBehaviourNode>();

        spawnAgents = GetComponent<NestBehaviour_SpawnAgents>();
       
        TurnOrderController.OnTurnStarted += TurnOrderController_OnTurnStarted;
    }

    private void TurnOrderController_OnTurnStarted(object sender, EventArgs e)
    {
        turnCount++;
    }

    private void OnWorldObjectSetUpComplete()
    {
        GameWorldMapManager = GameWorldMapManager.instance;
        behaviourController = AiBehaviourController.instance;

        CreateTerrittory();
    }

    public void DoAction()
    {
        foreach(NestBehaviourNode node in behaviourNodes)
        {
            node.TickAction(turnCount);
        }
    }

    public void CreateTerrittory()
    {
        territoryCreator.createRadius = nestRange;
        territoryCreator.CreateTerritory(WorldObject.worldPosition, GameWorldMapManager, out nestTerritory);

    }



    [System.Serializable]
    public class NestTarget
    {
        public bool isCreated;

        public Vector3Int targetPos;

        public int maxAgents;

        public int curentAgents;

        public bool canDefendersJoin = false;

        public bool CanAddAgent()
        {
            return curentAgents < maxAgents;
        }

        public bool CanAddAgent(AiAgent aiAgent)
        {
            if(!canDefendersJoin && aiAgent.isDefender)
            {
                return false;
            }

            return curentAgents < maxAgents;
        }

        public void AddAgent()
        {
            curentAgents++;
        }

        public void RemoveAgent()
        {
            curentAgents--; 
        }

    }
}
