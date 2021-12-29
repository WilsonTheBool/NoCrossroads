using UnityEngine;
using System.Collections;

[System.Serializable]
public class Level_SaveData
{
    public string sceneName;

    public string playTime;

    public WorldObject_SaveData[] allSavedObjects;

    public GameResourceManager_SaveLoadController.ResourceManagerSaveData resourceManagerSaveData;

    public ShopUpgrades_SaveLoadConverter.ShopUpgradeSaveData upgradeSaveData;

    public ExplorationController_SaveLoadController.ExplorationControllerSaveData ExplorationControllerSaveData;
}
