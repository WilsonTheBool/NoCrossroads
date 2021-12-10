using UnityEngine;
using System.Collections;

public class TutorialNode_BuildWorkers : TutorialNode
{
    public string workerTypeName;

    public GameWorldMapManager GameWorldMapManager;

    //public GameObject productionInfoWindow;

    public int maxWorkerNum;
    private int workerNum;

    public override void OnStart()
    {
        base.OnStart();
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
        GameWorldMapManager.OnUnitCreate -= GameWorldMapManager_OnUnitCreate;
    }
}
