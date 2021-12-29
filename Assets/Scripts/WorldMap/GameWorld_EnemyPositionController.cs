using UnityEngine;
using System.Collections.Generic;

public class GameWorld_EnemyPositionController : GameWorldMap_Dependable
{
    GameWorldMapManager GameWorldMapManager;
    public override void SetUp()
    {
        GameWorldMapManager = GameWorldMapManager.instance;
        GameWorldMapManager.GameWorld_EnemyPositionController = this;

        GameWorldMapManager.OnUnitCreate += GameWorldMapManager_OnUnitCreate;
        GameWorldMapManager.OnUnitDeath += GameWorldMapManager_OnUnitDeath;
    }

    public List<WorldObject> monsters = new List<WorldObject>();

    private void GameWorldMapManager_OnUnitDeath(object sender, GameWorldMapManager.UnitEventArgs e)
    {
        if (monsters.Contains(e.worldObject))
        {
            monsters.Remove(e.worldObject);
        }
    }

    private void GameWorldMapManager_OnUnitCreate(object sender, GameWorldMapManager.UnitEventArgs e)
    {

        AiAgent agent = e.worldObject.GetComponent<AiAgent>();

        if (agent != null && !agent.isPlayer)
        {
            AttackingCharacter attackingCharacter = e.worldObject.GetComponent<AttackingCharacter>();
            if (attackingCharacter != null)
            {
                monsters.Add(e.worldObject);
            }
        }
    }

    public bool IsInRangeOfMonster(int range, Vector3Int pos)
    {
        foreach (WorldObject worldObject in monsters)
        {
            if (Vector3Int.Distance(worldObject.worldPosition, pos) <= range)
            {
                return true;
            }
        }
        return false;
    }
}
