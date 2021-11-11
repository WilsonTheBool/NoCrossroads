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

        GameWorldMapManager = GameWorldMapManager.instance;

        terrirtoryCreator = GetComponent<TerritoryCreator>();

        terrirtoryCreator.CreateTerritory(GameWorldMapManager.GetTilePosition(this.transform.position), GameWorldMapManager);

        WorldObject.SetUp();
    }
}
