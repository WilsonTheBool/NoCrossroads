using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class ShopItemPlacerController : MonoBehaviour
{
    public InputHandler inputHandler; 

    GameWorldMapManager gameWorldMapManager;
    SpecialTilemapManager SpecialTilemapManager;

    ShopElement ShopElement;
    public PlacableObject curentItem;

    public SpriteRenderer cursorObject;

    private Sprite itemSprite;

    private void Start()
    {
        gameWorldMapManager = GameWorldMapManager.instance;
        SpecialTilemapManager = gameWorldMapManager.SpecialTilemapManager;

        DisableInputModule();
    }

    public void StartPlacment(ShopElement element)
    {
        ShopElement = element;
        EnableInputModule();
        curentItem = element.itemPrefab.GetComponent<PlacableObject>();
        itemSprite = element.itemPrefab.GetComponent<SpriteRenderer>().sprite;

        //Clear all ui

        //Draw tiles (can/can not spawn)
        foreach (Vector3Int pos in gameWorldMapManager.playerTerritory)
        {
            if (curentItem.CanSpawn(pos, gameWorldMapManager))
            {
                SpecialTilemapManager.DrawTile_CanPlaceTile(pos);
            }
            else
            {
                SpecialTilemapManager.DrawTile_NoPlaceTile(pos);
            }
        }

        gameWorldMapManager.TerritoryTilemapManager.territoryTilemap.gameObject.SetActive(false);
        gameWorldMapManager.LandTilemapManager.landTIlemap.color = new Color(0.8f, 0.8f, 0.8f);
        //follow mouse object placment
        ShowCursorObject();
    }

    public void DisableInputModule()
    {
        inputHandler.enabled = false;
    }

    public void EnableInputModule()
    {
        inputHandler.enabled = true;
    }

    public void ConfirmPlacement(GameInputData inputData)
    {

        Vector3Int pos = inputData.tileMousePosition;

        if (curentItem.CanSpawn(pos, gameWorldMapManager))
        {
            ShopElement.ConfirmBuy();
            ShopElement.SpawnItem(pos);
        }
        else
        {
            //play error sound or smth
        }

        EndSelectPlace();
    }

    public void UpdatePlacement(GameInputData data)
    {
        if(data.oldTileMousePosition != data.tileMousePosition)
        {
            if (curentItem.CanSpawn(data.tileMousePosition, gameWorldMapManager))
                cursorObject.transform.position = gameWorldMapManager.GetTileCenterInWorld(data.tileMousePosition);
            else
                cursorObject.transform.position = new Vector3(-1, -1, -100);
        }
        
    }

    private void EndSelectPlace()
    {
        gameWorldMapManager.SpecialTilemapManager.ClearTilemap();
        gameWorldMapManager.TerritoryTilemapManager.territoryTilemap.gameObject.SetActive(true);
        gameWorldMapManager.LandTilemapManager.landTIlemap.color = new Color(1, 1, 1);
        DisableInputModule();
        HideCursorObject();
    }

    public void CancelPlacement(GameInputData inputData)
    {
        EndSelectPlace();
    }

    private void HideCursorObject()
    {
        cursorObject?.gameObject.SetActive(false);
    }

    private void ShowCursorObject()
    {
        cursorObject?.gameObject.SetActive(true);
        cursorObject.sprite = itemSprite;
    }

    
}


