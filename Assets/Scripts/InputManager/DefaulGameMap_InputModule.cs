using UnityEngine;
using System.Collections;

public class DefaulGameMap_InputModule : MonoBehaviour
{

    [SerializeField]
    GameObject selectTile;

    [SerializeField]
    TileSpawnRule_SO[] selectTileRules;

    GameWorldMapManager GameWorldMapManager;

    private void Start()
    {
        GameWorldMapManager = GameWorldMapManager.instance;
    }

    public void DrawSelectTile(GameInputData data)
    {
        if (data.isOverUI)
        {
            selectTile.transform.position = new Vector3(-1, -1, -100);
        }
        else
        {
            foreach (TileSpawnRule_SO rule in selectTileRules)
            {
                if (!rule.CanSpawnTile(data.tileMousePosition, GameWorldMapManager))
                {
                    return;
                }
            }

            selectTile.transform.position = GameWorldMapManager.GetTileCenterInWorld(data.tileMousePosition);
        }
    }

    ResourceTile lastTile;
    public void UpdateResourceIcon(GameInputData data)
    {
        if (!data.isOverUI)
        {
            if (data.oldTileMousePosition != data.tileMousePosition)
            {
                if (GameWorldMapManager.TryGetResourceTIle(data.oldTileMousePosition, out ResourceTile tile))
                {
                    lastTile = null;
                    tile.Deselect();
                }


                if (GameWorldMapManager.TryGetResourceTIle(data.tileMousePosition, out tile))
                {
                    lastTile = tile;
                    tile.Select();
                }

            }
        }
        else
        {
            if(lastTile != null)
            {
                lastTile.Deselect();
            }
        }
        
    }

    public void OnFocus()
    {
        selectTile.SetActive(true);
    }

    public void OnLostFocus()
    {
        selectTile.SetActive(false);

        if(lastTile != null)
        {
            lastTile.Deselect();
        }
    }
}
