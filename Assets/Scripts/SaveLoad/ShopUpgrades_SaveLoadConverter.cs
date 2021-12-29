using UnityEngine;
using System.Collections;

public class ShopUpgrades_SaveLoadConverter : MonoBehaviour
{

    public GameSaveLoadController GameSaveLoadController;

    private void Awake()
    {
        if (GameSaveLoadController == null)
        {
            GameSaveLoadController = GameSaveLoadController.instance;
        }

        if (GameSaveLoadController != null && GameSaveLoadController.isCurentlyLoading)
        {
            Load();
        }
    }

    public void Load()
    {
        UI_UnlockableItem[] unlockableItems = FindObjectsOfType<UI_UnlockableItem>();

        

        if (GameSaveLoadController.isCurentlyLoading)
        {
            ShopUpgradeSaveData saveData = GameSaveLoadController.levelSaveData.upgradeSaveData;

            foreach(var data in saveData.lockedObjsSaveData)
            {
                foreach(UI_UnlockableItem item in unlockableItems)
                {
                    if(item.name == data.objectName)
                    {
                        if (data.isLocked)
                        {
                            item.LockItem();
                        }
                        else
                        {
                            item.UnlockItem();
                        }
                  
                    }
                }
            }

        }
    }

    public ShopUpgradeSaveData Save()
    {
        UI_UnlockableItem[] unlockableItems = FindObjectsOfType<UI_UnlockableItem>();


        ShopUpgradeSaveData saveData = GameSaveLoadController.levelSaveData.upgradeSaveData;

        ShopUpgradeSaveData.LockedSaveData[] datas = new ShopUpgradeSaveData.LockedSaveData[unlockableItems.Length];

        for (int i = 0; i < datas.Length; i++)
        {
            datas[i] = new ShopUpgradeSaveData.LockedSaveData()
            {
                objectName = unlockableItems[i].name,
                isLocked = unlockableItems[i].isLocked
            };
        }

        saveData.lockedObjsSaveData = datas;
        return saveData;
    }

    [System.Serializable]
    public struct ShopUpgradeSaveData
    {
        public LockedSaveData[] lockedObjsSaveData;

        [System.Serializable]
        public struct LockedSaveData
        {
            public string objectName;
            public bool isLocked;
        }
    }
}
