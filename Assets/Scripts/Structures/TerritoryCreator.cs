using UnityEngine;
using System.Collections.Generic;

public class TerritoryCreator : MonoBehaviour
{
    public TileSpawnRule_SO[] territorySpawnRules;

    public int createRadius;

    public void CreateTerritory(Vector3Int startPos, GameWorldMapManager GameWorldMapManager)
    {
        Vector3Int[] positions = MathAdd.GetAllPositionInCircle(startPos, createRadius);

        List<Vector3Int> truePosition = new List<Vector3Int>();

        foreach(Vector3Int pos in positions)
        {
            if(TileSpawnRule_SO.TrueForAllRules(pos, GameWorldMapManager, territorySpawnRules))
            {
                truePosition.Add(pos);
            }
        }

        GameWorldMapManager.AddPlayerTerritory(truePosition.ToArray());
    }


    
}
