using UnityEngine;
using System.Collections.Generic;

public class NoizeGameWorldController : MonoBehaviour
{
    GameWorldMapManager GameWorldMapManager;

    private List<NoizeStructure> noizeStructures = new List<NoizeStructure>();

    private void Start()
    {
        GameWorldMapManager = GameWorldMapManager.instance;
        GameWorldMapManager.OnUnitCreate += GameWorldMapManager_OnUnitCreate;
        GameWorldMapManager.OnUnitDeath += GameWorldMapManager_OnUnitDeath;
    }

    public NoizeStructure GetNearestNoizeStructure(Vector3Int pos)
    {
        float minDistance = float.MaxValue;
        NoizeStructure best = null;
        foreach(NoizeStructure structure in noizeStructures)
        {
            float dis = Vector3Int.Distance(pos, structure.WorldObject.worldPosition) - structure.noizeLevel;
            if (dis < minDistance)
            {
                minDistance = dis;
                best = structure;
            }
        }

        return best;
    }

    private void GameWorldMapManager_OnUnitDeath(object sender, GameWorldMapManager.UnitEventArgs e)
    {


        if (e.worldObject.TryGetComponent(out NoizeStructure noizeStructure))
        {
            noizeStructures.Remove(noizeStructure);
        }
    }

    private void GameWorldMapManager_OnUnitCreate(object sender, GameWorldMapManager.UnitEventArgs e)
    {
        if (e.worldObject.TryGetComponent(out NoizeStructure noizeStructure))
        {
            noizeStructures.Add(noizeStructure);
            print("noize structure add: " + noizeStructure.name);
        }
    }
}
