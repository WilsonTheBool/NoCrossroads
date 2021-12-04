using System.Collections.Generic;
using UnityEngine;
public class NestBehaviour_SpawnAgents : MonoBehaviour
{

    public int maxAgentNumber;

    public int maxDefendersNumber;
    private int defendersCount;

    public int AgentSpawnDelay;

    public TileSpawnRule_SO[] spawnRules;

    public AiAgentNest nest;

    int agentCount;

    GameWorldMapManager GameWorldMapManager;


    [SerializeField]
    AiAgentSpawnController spawnController;


    TurnTimerController controller;

    TurnTimerController.TurnTimer CreateTimer;

    private void Start()
    {
        GameWorldMapManager = GameWorldMapManager.instance;
        controller = TurnTimerController.instance;
        CreateTimer = new TurnTimerController.TurnTimer(AgentSpawnDelay, controller);
        CreateTimer.OnEnd += CreateTimer_OnEnd;
    }

    private void CreateTimer_OnEnd()
    {
        SpawnAgent();
        CreateTimer.Reset();
    }

    public void SpawnAgent()
    {
        if (IsSpawnAvailable() && TryGetRandomTerritory(out Vector3Int pos))
        {
            if(defendersCount < maxDefendersNumber)
            {
                SpawnDefender(pos);
            }
            else
            {
                SpawnAgent(pos);
            }

            
        }
    }

    public bool IsSpawnAvailable()
    {
        return agentCount < maxAgentNumber;
    }

    private bool TryGetRandomTerritory(out Vector3Int randomPos)
    {
        List<Vector3Int> available = new List<Vector3Int>();
        foreach (Vector3Int pos in nest.nestTerritory)
        {
            if (CanSpawnAgent(pos))
            {
                available.Add(pos);
            }
        }

        if (available.Count > 0)
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
        foreach (TileSpawnRule_SO rule in spawnRules)
        {
            if (!rule.CanSpawnTile(pos, GameWorldMapManager))
            {
                return false;
            }
        }

        return true;
    }




    private void OnAgentDeath()
    {
        agentCount--;
    }

    private void OnDefenderDeath()
    {
        defendersCount--;
    }

    private void SpawnAgent(Vector3Int pos)
    {
        AiAgent agent = Instantiate(spawnController.GetRandomAgentPrefab(), GameWorldMapManager.GetTileCenterInWorld(pos), Quaternion.Euler(0, 0, 0));
        agent.killable.OnDeath.AddListener(OnAgentDeath);
        agent.nest = this.nest;
        agentCount++;
    }

    private void SpawnDefender(Vector3Int pos)
    {
        AiAgent agent = Instantiate(spawnController.GetRandomDefenderPrefab(), GameWorldMapManager.GetTileCenterInWorld(pos), Quaternion.Euler(0, 0, 0));
        agent.killable.OnDeath.AddListener(OnAgentDeath);
        agent.killable.OnDeath.AddListener(OnDefenderDeath);
        agent.nest = this.nest;
        agentCount++;
        defendersCount++;
    }

    private void OnDestroy()
    {
        CreateTimer.OnEnd -= CreateTimer_OnEnd;
        CreateTimer = null;

    }
}

