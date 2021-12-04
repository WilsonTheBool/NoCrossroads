using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class AiBehaviourController : GameWorldMap_Dependable
{
    public static AiBehaviourController instance;

    public TargetRule_data_SO[] targetRules;

    public TurnOrderController turnOrderController;

    public NewGameMovementController NewGameMovementController;

    public NoizeGameWorldController NoizeGameWorldController;

    public List<AiAgent> aiAgents;

    public List<AiAgentNest> nests;

    GameWorldMapManager GameWorldMapManager;

    public UnityEvent OnAiTurnStart;
    public UnityEvent OnAiTurnEnd;

    public List<AiTargetData> AllTargetsOnMap = new List<AiTargetData>();

    public Player_AIBehaviourController Player_AIBehaviourController;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
            return;
        }
        else
        {
            instance = this;
        }

       
    }

    public override void SetUp()
    {
        GameWorldMapManager = GameWorldMapManager.instance;
        Player_AIBehaviourController = Player_AIBehaviourController.instance;
        GameWorldMapManager.OnUnitCreate += GameWorldMapManager_OnUnitCreate;
        GameWorldMapManager.OnUnitDeath += GameWorldMapManager_OnUnitDeath;
        turnOrderController.OnTurnEnded += TurnOrderController_OnTurnEnded;
    }

    private void Start()
    {
        
    }

    private void TurnOrderController_OnTurnEnded(object sender, System.EventArgs e)
    {
        StartAITurn();
    }

    List<AiAgent> agentsToDelete = new List<AiAgent>();
    private void GameWorldMapManager_OnUnitDeath(object sender, GameWorldMapManager.UnitEventArgs e)
    {
        AiAgent agent = e.worldObject.GetComponent<AiAgent>();

        if (agent != null && !agent.isPlayer)
        {
            agentsToDelete.Add(agent);
        }
        else
        {
            AiAgentNest nest = e.worldObject.GetComponent<AiAgentNest>();

            if (nest != null)
            {
                nests.Remove(nest);
            }

        }
    }

    private void GameWorldMapManager_OnUnitCreate(object sender, GameWorldMapManager.UnitEventArgs e)
    {
        AiAgent agent = e.worldObject.GetComponent<AiAgent>();

        if(agent != null && !agent.isPlayer)
        {
            aiAgents.Add(agent);
        }
        else
        {
            AiAgentNest nest = e.worldObject.GetComponent<AiAgentNest>();

            if(nest != null)
            {
                nests.Add(nest);
            }

        }
    }

    public void StartAITurn()
    {

        OnAiTurnStart.Invoke();

        StartCoroutine(AiTurnCo());

    }

    private void EndAITurn()
    {
        OnAiTurnEnd.Invoke();

        Player_AIBehaviourController.StartAITurn();

        //turnOrderController.StartPlayerTurn();
    }

    private IEnumerator AiTurnCo()
    {
        if (agentsToDelete.Count > 0)
        {
            foreach (AiAgent ai in agentsToDelete)
            {
                aiAgents.Remove(ai);
            }

            agentsToDelete.Clear();
        }



        GetAllTargetsOnMap();

        yield return null;

       

        foreach(AiAgent aiAgent in aiAgents)
        {
            aiAgent.DoAction();
            yield return new WaitForSeconds (aiAgent.GetActionDelay());
        }

        foreach (AiAgentNest nest in nests)
        {
            nest.DoAction();
            yield return null;
        }

        if (agentsToDelete.Count > 0)
        {
            foreach (AiAgent ai in agentsToDelete)
            {
                aiAgents.Remove(ai);
            }

            agentsToDelete.Clear();
        }

        EndAITurn();
    }

    public bool CanTarget(KillableCharacter killableCharacter)
    {
        foreach(TargetRule_data_SO rule in targetRules)
        {
            if (!rule.CanAttack(killableCharacter))
            {
                return false;
            }
        }

        return true;
    }

    public bool TryGetTargetFromWorldObject(WorldObject world, out AiTargetData data)
    {
        data = new AiTargetData();

        data.WorldObject = world;

        if(world.TryGetComponent<KillableCharacter>(out KillableCharacter killableCharacter) && CanTarget(killableCharacter))
        {
            data.KillableCharacter = killableCharacter;
        }
        else
        {
            return false;
        }

        return true;
    }

    public void OnTargetKilled(AiTargetData data)
    {
        AllTargetsOnMap.Remove(data);
    }

    private void GetAllTargetsOnMap()
    {
        AllTargetsOnMap.Clear();
        foreach (WorldObject worldObject in GameWorldMapManager.allObjectsOnMap)
        {
            if(TryGetTargetFromWorldObject(worldObject, out AiTargetData data))
            {
                AllTargetsOnMap.Add(data);
            }
        }
    }

    public AiTargetData[] GetAllEnemiesInRange(Vector3Int starPos, int range)
    {
        List<AiTargetData> temp = new List<AiTargetData>();
        foreach(AiTargetData data in AllTargetsOnMap)
        {
            if(Vector3Int.Distance(starPos, data.WorldObject.worldPosition) <= range)
            {
                temp.Add(data);
            }
        }

        return temp.ToArray();
    }

    
}

public class AiTargetData
{
    public WorldObject WorldObject;
    public KillableCharacter KillableCharacter;

}

