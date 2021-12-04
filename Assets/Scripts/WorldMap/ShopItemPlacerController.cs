using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class ShopItemPlacerController : MonoBehaviour
{
    public UI_CanvasGroupsController UI_CanvasGroupsController;

    public InputHandler inputHandler; 

    GameWorldMapManager gameWorldMapManager;
    SpecialTilemapManager SpecialTilemapManager;

    ShopElement ShopElement;
    public PlacableObject curentItem;

    public SpriteRenderer cursorObject;

    private Sprite itemSprite;

    public UnityEvent OnStartPlacement;
    public UnityEvent OnEndPlacement;

    public string CantPlaceItem_error_text;

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

        gameWorldMapManager.TerritoryTilemapManager.territoryTilemap.gameObject.SetActive(false);
        gameWorldMapManager.LandTilemapManager.landTIlemap.color = new Color(0.8f, 0.8f, 0.8f);
        //follow mouse object placment
        ShowCursorObject();

        curentItem.UnitPlacementController.OnPlacementStart(SpecialTilemapManager, gameWorldMapManager);

        OnStartPlacement.Invoke();
    }

    public void SetUi_OnStart()
    {
        UI_CanvasGroupsController.CanvasGroupFlags flags = new UI_CanvasGroupsController.CanvasGroupFlags() { setDefault = true };
        UI_CanvasGroupsController.SetActiveWindows(flags, false);
    }

    public void SeUi_OnEnd()
    {
        UI_CanvasGroupsController.CanvasGroupFlags flags = new UI_CanvasGroupsController.CanvasGroupFlags() { setDefault = true };
        UI_CanvasGroupsController.SetActiveWindows(flags, true);
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
            EndSelectPlace();
        }
        else
        {
            OnCanNotPlaceItem.Invoke();
        }

        
    }

    public UnityEvent OnCanNotPlaceItem;

    public void UpdatePlacement(GameInputData data)
    {
        if(data.oldTileMousePosition != data.tileMousePosition)
        {
            if (curentItem.CanSpawn(data.tileMousePosition, gameWorldMapManager))
                cursorObject.transform.position = gameWorldMapManager.GetTileCenterInWorld(data.tileMousePosition);
            else
                cursorObject.transform.position = new Vector3(-1000, -1000, -1000);

            curentItem.UnitPlacementController.OnPlacementUpdate(data, SpecialTilemapManager, gameWorldMapManager);
        }
        
    }

    private void EndSelectPlace()
    {

        gameWorldMapManager.TerritoryTilemapManager.territoryTilemap.gameObject.SetActive(true);
        gameWorldMapManager.LandTilemapManager.landTIlemap.color = new Color(1, 1, 1);

        DisableInputModule();
        HideCursorObject();

        curentItem.UnitPlacementController.OnPlacementEnd(SpecialTilemapManager, gameWorldMapManager);
        OnEndPlacement.Invoke();
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


