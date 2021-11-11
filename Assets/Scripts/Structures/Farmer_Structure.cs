using UnityEngine;
using System.Collections;

[RequireComponent(typeof(WorldObject), typeof(TerritoryCreator), typeof(Structure))]
public class Farmer_Structure : MonoBehaviour
{
    public WorldObject WorldObject;

    TurnBaseObject TurnBaseObject;

    Structure Structure;

    GameResourceManager resourceManager;

    public ResourceTile farmPrefab;
    private ResourceTile resourceTile;

    GameWorldMapManager gameWorldMapManager;

    public void Mine()
    {
        resourceManager.AddResource(resourceTile.resource, resourceTile.GetMineAmmount(Structure.workEffectivenessValue));
    }

    private void Awake()
    {
        WorldObject = GetComponent<WorldObject>();
        TurnBaseObject = GetComponent<TurnBaseObject>();
        Structure = GetComponent<Structure>();
        gameWorldMapManager = GameWorldMapManager.instance;
        resourceManager = GameResourceManager.instance;

        resourceTile = Instantiate(farmPrefab, this.transform.position, Quaternion.Euler(0,0,0));
        resourceTile.SetIsFree(false);
        resourceTile.ResourceIconController.ShowIcon(false);
        // TurnBaseObject.OnTurnEnd.AddListener(Mine);

    }

    private void OnDestroy()
    {
        resourceTile.isFree = true;
    }
}
