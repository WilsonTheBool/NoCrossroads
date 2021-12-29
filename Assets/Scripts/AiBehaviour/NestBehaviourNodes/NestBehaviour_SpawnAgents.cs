using System.Collections.Generic;
using UnityEngine;
public class NestBehaviour_SpawnAgents : NestBehaviourNode
{

    public int maxAgentNumber;

    public int maxDefendersNumber;

    [SerializeField]
    private int defendersCount;

    public int AgentSpawnDelay;

    public TileSpawnRule_SO[] spawnRules;

    public AiAgentNest nest;
    [SerializeField]
    int agentCount;

    GameWorldMapManager GameWorldMapManager;


    [SerializeField]
    AiAgentSpawnController spawnController;


    TurnTimerController controller;

    TurnTimerController.TurnTimer CreateTimer;

    private void Start()
    {
        GameWorldMapManager = GameWorldMapManager.instance;

        GameWorldMapManager.OnUnitCreate += GameWorldMapManager_OnUnitCreate;

        controller = TurnTimerController.instance;
        //CreateTimer = new TurnTimerController.TurnTimer(AgentSpawnDelay, controller);
        //CreateTimer.OnEnd += CreateTimer_OnEnd;
    }

    private void GameWorldMapManager_OnUnitCreate(object sender, GameWorldMapManager.UnitEventArgs e)
    {
        AiAgent aiAgent = e.worldObject.GetComponent<AiAgent>();

        if (aiAgent != null && !aiAgent.curentTarget.isCreated)
            foreach (Vector3Int pos in nest.nestTerritory)
            {
                if (pos == e.worldObject.worldPosition)
                {
                    OnAgentAdd(aiAgent);
                }
            }
    }

    public override void TickAction(int turnCount)
    {
        if (turnCount % AgentSpawnDelay == 0)
        {
            CreateTimer_OnEnd();
           
        }
    }

    private void CreateTimer_OnEnd()
    {
        SpawnAgent();
       
        //CreateTimer.Reset();
    }

    public void RemoveAgentFromNest(AiAgent aiAgent)
    {
        KillableCharacter killableCharacter = aiAgent.GetComponent<KillableCharacter>();

        if (!aiAgent.isDefender)
        {
            OnAgentDeath();
            killableCharacter.OnDeath.RemoveListener(OnAgentDeath);
        }
        else
        { 
            OnDefenderDeath();
            killableCharacter.OnDeath.RemoveListener(OnDefenderDeath);
        }
        
    }

    public void SpawnAgent()
    {
        if (IsSpawnAvailable() && TryGetRandomTerritory(out Vector3Int pos))
        {
            if(agentCount < maxAgentNumber - maxDefendersNumber)
            {
                SpawnAgent(pos);
            }
            else
            {
                if (defendersCount < maxDefendersNumber)
                    SpawnDefender(pos);
                else
                    SpawnAgent(pos);
            }

            //if(defendersCount < maxDefendersNumber)
            //{
            //    SpawnDefender(pos);
            //}
            //else
            //{
            //    SpawnAgent(pos);
            //}

            
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

    private void OnAgentAdd(AiAgent agent)
    {
        if (agent.isDefender)
        {
            agent.killable.OnDeath.AddListener(OnAgentDeath);
            agent.killable.OnDeath.AddListener(OnDefenderDeath);
            agent.nest = this.nest;
            agentCount++;
            defendersCount++;
        }
        else
        {
            agent.killable.OnDeath.AddListener(OnAgentDeath);
            agent.nest = this.nest;
            agentCount++;
        }
        
    }

    private void SpawnAgent(Vector3Int pos)
    {
        AiAgent agent = Instantiate(spawnController.GetRandomAgentPrefab(), GameWorldMapManager.GetTileCenterInWorld(pos), Quaternion.Euler(0, 0, 0));
    }

    private void SpawnDefender(Vector3Int pos)
    {
        AiAgent agent = Instantiate(spawnController.GetRandomDefenderPrefab(), GameWorldMapManager.GetTileCenterInWorld(pos), Quaternion.Euler(0, 0, 0));
    }

    private void OnDestroy()
    {

    }
}

