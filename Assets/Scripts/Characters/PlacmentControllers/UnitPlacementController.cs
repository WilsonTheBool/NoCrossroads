using UnityEngine;
using System.Collections;

public class UnitPlacementController : MonoBehaviour
{
    public PlacableObject PlacableObject;

    public virtual void OnPlacementUpdate(GameInputData data, SpecialTilemapManager specialTilemapManager, GameWorldMapManager gameWorldMapManager)
    {
    }

    public virtual void OnPlacementStart(SpecialTilemapManager specialTilemapManager, GameWorldMapManager gameWorldMapManager)
    {

    }

    public virtual void OnPlacementEnd(SpecialTilemapManager specialTilemapManager, GameWorldMapManager gameWorldMapManager)
    {
    }
}
