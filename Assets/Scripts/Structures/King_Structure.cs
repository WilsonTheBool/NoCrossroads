using UnityEngine;
using System.Collections;

[RequireComponent(typeof(WorldObject), typeof(TerritoryCreator), typeof(Structure))]
public class King_Structure : MonoBehaviour
{

    [HideInInspector]
    public WorldObject WorldObject;

    GameWorldMapManager GameWorldMapManager;

    TerritoryCreator terrirtoryCreator;

    private void Awake()
    {
        WorldObject = GetComponent<WorldObject>();

        terrirtoryCreator = GetComponent<TerritoryCreator>();

        WorldObject.OnSetUpComplete.AddListener(SetUp);

        //WorldObject.SetUp();
    }

    private void SetUp()
    {
        GameWorldMapManager = GameWorldMapManager.instance;
        terrirtoryCreator.CreateTerritory(GameWorldMapManager.GetTilePosition(this.transform.position), GameWorldMapManager);
    }
}
