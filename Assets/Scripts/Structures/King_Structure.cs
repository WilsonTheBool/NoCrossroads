using UnityEngine;
using System.Collections;

[RequireComponent(typeof(WorldObject), typeof(TerritoryCreator), typeof(Structure))]
public class King_Structure : MonoBehaviour
{

    [HideInInspector]
    public WorldObject WorldObject;

    GameWorldMapManager GameWorldMapManager;

    TerritoryCreator terrirtoryCreator;

    private void Start()
    {
        WorldObject = GetComponent<WorldObject>();
        WorldObject.SetUp();

        GameWorldMapManager = GameWorldMapManager.instance;

        terrirtoryCreator = GetComponent<TerritoryCreator>();

        terrirtoryCreator.CreateTerritory(WorldObject.worldPosition, GameWorldMapManager);
    }
}
