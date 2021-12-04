using UnityEngine;
using System.Collections.Generic;

public class TerritoryCreator : MonoBehaviour
{
    [HideInInspector]
    public WorldObject WorldObject;

    public TileSpawnRule_SO[] territorySpawnRules;

    public int createRadius;

    public bool isEnemy;

    List<Vector3Int> territory;



    private void Start()
    {
        WorldObject = GetComponent<WorldObject>();
    }

    public void AddTerritory(Vector3Int pos)
    {
        territory.Add(pos);
    }

    public Vector3Int[] GetAllTerritory()
    {
        return territory.ToArray();
    }

    public void UpdateTerritory(Vector3Int startPos, GameWorldMapManager GameWorldMapManager)
    {
        GameWorldMapManager.ClearTerritory(territory.ToArray());
        CreateTerritory(startPos, GameWorldMapManager);

    }

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

        territory = truePosition;
        GameWorldMapManager.AddPlayerTerritory(truePosition.ToArray());
    }

    public void CreateTerritory(Vector3Int startPos, GameWorldMapManager GameWorldMapManager, out Vector3Int[] territory)
    {
        Vector3Int[] positions = MathAdd.GetAllPositionInCircle(startPos, createRadius);

        List<Vector3Int> truePosition = new List<Vector3Int>();

        foreach (Vector3Int pos in positions)
        {
            if (TileSpawnRule_SO.TrueForAllRules(pos, GameWorldMapManager, territorySpawnRules))
            {
                truePosition.Add(pos);
            }
        }

        
        territory = truePosition.ToArray();
        this.territory = truePosition;

        if (isEnemy)
        {
            GameWorldMapManager.AddEnemyTerritory(truePosition.ToArray());
        }
        else
        GameWorldMapManager.AddPlayerTerritory(truePosition.ToArray());
    }

}
