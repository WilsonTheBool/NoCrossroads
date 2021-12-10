using UnityEngine;
using UnityEngine.Tilemaps;
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
        if(LandTilemapManager.landTIlemap == null)
        {
            foreach (Tilemap tilemap in FindObjectsOfType<Tilemap>())
            {
                if (tilemap.CompareTag("LandTilemap"))
                {
                    LandTilemapManager.landTIlemap = tilemap;
                }
            }
        }

        var map = LandTilemapManager.landTIlemap;

        MapSize = map.cellBounds.size;
        MapOffset = map.cellBounds.position;

        allLandPositions = LandTilemapManager.GetAllLandPositions();
    }
}
