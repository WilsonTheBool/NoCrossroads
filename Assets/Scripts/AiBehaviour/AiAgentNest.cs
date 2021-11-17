using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class AiAgentNest : MonoBehaviour
{
    
    public WorldObject WorldObject;

    public KillableCharacter killable;

    public Vector3Int[] nestTerritory;

    public TerritoryCreator territoryCreator;

    public TileSpawnRule_SO[] spawnRules;

    public AiBehaviourController behaviourController;

    GameWorldMapManager GameWorldMapManager;

    public int maxAgentNumber;


    [SerializeField]
    AiAgentSpawnController spawnController;

    [SerializeField]
    int nestRange;

    int agentCount;

    public UnityEvent OnNestAttacked;

    public UnityEvent OnNestCreate;
    public UnityEvent OnNestDestroy;

    public NestTarget curentTarget;

    private void Start()
    {
        GameWorldMapManager = GameWorldMapManager.instance;
        behaviourController = AiBehaviourController.instance;
        WorldObject.SetUp();
        CreateTerrittory();
    }

    public void DoAction()
    {
        SpawnAgent();
    }

    public void CreateTerrittory()
    {
        territoryCreator.createRadius = nestRange;
        territoryCreator.CreateTerritory(WorldObject.worldPosition, GameWorldMapManager, out nestTerritory);
    }

    public void SpawnAgent()
    {
        if(IsSpawnAvailable() && TryGetRandomTerritory(out Vector3Int pos))
        {
            SpawnAgent(pos);
        }
    }

    public bool IsSpawnAvailable()
    {
        return agentCount < maxAgentNumber;
    }

    private bool TryGetRandomTerritory(out Vector3Int randomPos)
    {
        List<Vector3Int> available = new List<Vector3Int>();
        foreach(Vector3Int pos in nestTerritory)
        {
            if (CanSpawnAgent(pos))
            {
                available.Add(pos);
            }
        }

        if(available.Count > 0)
        {
            randomPos = available[Random.Range(0, available.Count)];
            return true;
        }
        else
        {
            randomPos = Vector3Int.zero;
            return false;
        }
    }

    private bool CanSpawnAgent(Vector3Int pos)
    {
        foreach(TileSpawnRule_SO rule in spawnRules)
        {
            if(!rule.CanSpawnTile(pos, GameWorldMapManager))
            {
                return false;
            }
        }

        return true;
    }

    private void SpawnAgent(Vector3Int pos)
    {
        AiAgent agent = Instantiate(spawnController.GetRandomAgentPrefab(), GameWorldMapManager.GetTileCenterInWorld(pos), Quaternion.Euler(0, 0, 0));
        agent.killable.OnDeath.AddListener(OnAgentDeath);
        agent.nest = this;
        agentCount++;
    }

    private void OnAgentDeath()
    {
        agentCount--;
    }

    public class NestTarget
    {
        public Vector3Int targetPos;

        public int maxAgents;

        int curentAgents;

        public bool CanAddAgent()
        {
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
