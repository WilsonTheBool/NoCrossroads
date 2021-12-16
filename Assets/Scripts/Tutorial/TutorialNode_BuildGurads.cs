using System;
using System.Collections.Generic;
using UnityEngine;
public class TutorialNode_BuildGurads : TutorialNode
{
    public string workerTypeName;

    public GameWorldMapManager GameWorldMapManager;

    //public GameObject productionInfoWindow;

    public int maxWorkerNum;
    private int workerNum;

    public UI_UnlockableItem guardItem;

    public override void OnStart()
    {
        base.OnStart();
        guardItem.UnlockItem();
        GameWorldMapManager.OnUnitCreate += GameWorldMapManager_OnUnitCreate;
        infoWindow.SetActive(true);
    }

    private void GameWorldMapManager_OnUnitCreate(object sender, GameWorldMapManager.UnitEventArgs e)
    {
        if (e.worldObject.typeName == workerTypeName)
        {
            workerNum++;

            if (workerNum >= maxWorkerNum)
            {
                isCompleted = true;
                tutorialController.StartNextNode();
            }
        }
    }

    public override void OnEnd()
    {
        base.OnEnd();
        guardItem.LockItem();
        GameWorldMapManager.OnUnitCreate -= GameWorldMapManager_OnUnitCreate;
    }
}

