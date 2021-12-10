using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using System.Collections;

public class Player_AIBehaviourController : GameWorldMap_Dependable
{
    public static Player_AIBehaviourController instance;

    public TargetRule_data_SO[] targetRules;

    public TurnOrderController turnOrderController;

    public NewGameMovementController NewGameMovementController;

    public List<AiAgent> aiAgents;

    GameWorldMapManager GameWorldMapManager;

    public UnityEvent OnAiTurnStart;
    public UnityEvent OnAiTurnEnd;

    public List<AiTargetData> AllTargetsOnMap = new List<AiTargetData>();

    private void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }

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
        GameWorldMapManager.OnUnitCreate += GameWorldMapManager_OnUnitCreate;
        GameWorldMapManager.OnUnitDeath += GameWorldMapManager_OnUnitDeath;
    }


    List<AiAgent> agentsToDelete = new List<AiAgent>();
    private void GameWorldMapManager_OnUnitDeath(object sender, GameWorldMapManager.UnitEventArgs e)
    {
        AiAgent agent = e.worldObject.GetComponent<AiAgent>();

        if (agent != null && agent.isPlayer)
        {
            agentsToDelete.Add(agent);
        }
       
    }

    private void GameWorldMapManager_OnUnitCreate(object sender, GameWorldMapManager.UnitEventArgs e)
    {
        AiAgent agent = e.worldObject.GetComponent<AiAgent>();

        if (agent != null && agent.isPlayer)
        {
            aiAgents.Add(agent);
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

        turnOrderController.StartPlayerTurn();
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



        

        yield return null;



        foreach (AiAgent aiAgent in aiAgents)
        {
            GetAllTargetsOnMap();

            aiAgent.DoAction();
            yield return new WaitForSeconds(aiAgent.GetActionDelay());

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
        foreach (TargetRule_data_SO rule in targetRules)
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

        if (world.TryGetComponent<KillableCharacter>(out KillableCharacter killableCharacter) && CanTarget(killableCharacter))
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
            if (TryGetTargetFromWorldObject(worldObject, out AiTargetData data))
            {
                AllTargetsOnMap.Add(data);
            }
        }
    }

    public AiTargetData[] GetAllEnemiesInRange(Vector3Int starPos, int range)
    {
        List<AiTargetData> temp = new List<AiTargetData>();
        foreach (AiTargetData data in AllTargetsOnMap)
        {
            if (Vector3Int.Distance(starPos, data.WorldObject.worldPosition) <= range)
            {
                temp.Add(data);
            }
        }

        return temp.ToArray();
    }


}

