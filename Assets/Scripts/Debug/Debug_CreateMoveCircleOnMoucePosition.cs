using UnityEngine;
using System.Collections;

public class Debug_CreateMoveCircleOnMoucePosition : MonoBehaviour
{
    public int speed;

    private GameUnitMovementController GameUnitMovementController;
    private SpecialTilemapManager SpecialTilemapManager;

    private Camera cam;

    private void Start()
    {
        GameUnitMovementController = FindObjectOfType<GameUnitMovementController>();
        SpecialTilemapManager = FindObjectOfType<SpecialTilemapManager>();
        cam = FindObjectOfType<Camera>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 worldMousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
            worldMousePosition -= new Vector3(0, 0, worldMousePosition.z);

            Vector3Int mousePos = SpecialTilemapManager.specialTilemap.WorldToCell(worldMousePosition);

            var vecs = GameUnitMovementController.GetMovementCircle(mousePos, speed);

            SpecialTilemapManager.ClearTilemap();

            foreach (Vector3Int pos in vecs)
            {
                SpecialTilemapManager.DrawTile_CanPlaceTile(pos);
            }
            
        }   
    }
}
