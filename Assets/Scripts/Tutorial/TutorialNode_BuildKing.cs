using UnityEngine;
using System.Collections;

public class TutorialNode_BuildKing : TutorialNode
{
    public string kingTypeName;

    public GameWorldMapManager GameWorldMapManager;

    public GameObject placementInfoWindow;

    public ShopItemPlacerController ShopItemPlacerController;

    public UnitSelectController UnitSelectController;

    public UI_UnlockableItem kingItem;

    public override void OnStart()
    {
        base.OnStart();
        kingItem.UnlockItem();
        //UnitSelectController.RemoveSelected(new GameInputData() { tileMousePosition = new Vector3Int(0,0,0)});

        GameWorldMapManager.OnUnitCreate += GameWorldMapManager_OnUnitCreate;
        ShopItemPlacerController.OnStartPlacement.AddListener(Show_PlacementTip);
        infoWindow.SetActive(true);
    }

    private void GameWorldMapManager_OnUnitCreate(object sender, GameWorldMapManager.UnitEventArgs e)
    {
        if(e.worldObject.typeName == kingTypeName)
        {
            isCompleted = true;
            tutorialController.StartNextNode();
        }
    }

    private void Show_PlacementTip()
    {
        placementInfoWindow.SetActive(true);
        ShopItemPlacerController.OnStartPlacement.RemoveListener(Show_PlacementTip);
    }

    public override void OnEnd()
    {
        base.OnEnd();
        kingItem.LockItem();
        GameWorldMapManager.OnUnitCreate -= GameWorldMapManager_OnUnitCreate;
    }
}
