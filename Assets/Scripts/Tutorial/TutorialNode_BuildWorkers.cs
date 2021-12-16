using UnityEngine;
using System.Collections;

public class TutorialNode_BuildWorkers : TutorialNode
{
    public string workerTypeName;

    public GameWorldMapManager GameWorldMapManager;

    //public GameObject productionInfoWindow;

    public int maxWorkerNum;
    private int workerNum;

    public UI_UnlockableItem workerItem;

    public override void OnStart()
    {
        base.OnStart();
        workerItem.UnlockItem();
        GameWorldMapManager.OnUnitCreate += GameWorldMapManager_OnUnitCreate;
        infoWindow.SetActive(true);
    }

    private void GameWorldMapManager_OnUnitCreate(object sender, GameWorldMapManager.UnitEventArgs e)
    {
        if (e.worldObject.typeName == workerTypeName)
        {
            workerNum++;

            if(workerNum >= maxWorkerNum)
            {
                isCompleted = true;
                tutorialController.StartNextNode();
            }
        }
    }

    public override void OnEnd()
    {
        base.OnEnd();
        workerItem.LockItem();
        GameWorldMapManager.OnUnitCreate -= GameWorldMapManager_OnUnitCreate;
    }
}
