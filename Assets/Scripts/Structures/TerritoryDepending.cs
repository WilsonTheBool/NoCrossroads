using UnityEngine;
using System.Collections;

public class TerritoryDepending : MonoBehaviour
{
    public WorldObject WorldObject;

    public KillableCharacter KillableCharacter;

    private GameWorldTerritoryController GameWorldTerritoryController;

    private TerritoryCreator TerritoryCreator;

    public bool isPlayer;

    
    private void Awake()
    {
        WorldObject.OnSetUpComplete.AddListener(SetUp);
    }

    private void SetUp()
    {
        GameWorldTerritoryController = GameWorldTerritoryController.instance;

        TerritoryCreator = GameWorldTerritoryController.GetClosestTerritoryCreatorInRange(WorldObject.worldPosition, isPlayer);
        TerritoryCreator.WorldObject.OnRemoveFromWorld.AddListener(OnCreatorDeath);

    }

    private void OnCreatorDeath()
    {
        TerritoryCreator newCreator = GameWorldTerritoryController.GetClosestTerritoryCreatorInRange(WorldObject.worldPosition, isPlayer);

        if(newCreator != null)
        {
            TerritoryCreator = newCreator;
        }
        else
        {
            KillableCharacter.Die();
        }
    }
}
