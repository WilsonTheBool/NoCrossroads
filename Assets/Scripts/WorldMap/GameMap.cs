using UnityEngine;
using System.Collections;

public class GameMap : MonoBehaviour
{
    public Vector3Int MapSize;

    public Vector3Int MapOffset;

    public LandTilemapManager LandTilemapManager;

    public Vector3Int[] allLandPositions;

    private void Awake()
    {

        if (LandTilemapManager == null)
            LandTilemapManager = FindObjectOfType<LandTilemapManager>();

        SetUp();
    }

    private void SetUp()
    {
        var map = LandTilemapManager.landTIlemap;

        MapSize = map.cellBounds.size;
        MapOffset = map.cellBounds.position;

        allLandPositions = LandTilemapManager.GetAllLandPositions();
    }
}
