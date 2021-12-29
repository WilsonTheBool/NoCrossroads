using UnityEngine;
using System.Collections;

public class GameResourceManager_SaveLoadController : MonoBehaviour
{
    public GameSaveLoadController GameSaveLoadController;
    public void Load()
    {
        GameResourceManager owner = GameResourceManager.instance;

        if(GameSaveLoadController == null)
        {
            GameSaveLoadController = GameSaveLoadController.instance;
        }

        if (GameSaveLoadController != null && GameSaveLoadController.isCurentlyLoading)
        {
            ResourceManagerSaveData saveData = GameSaveLoadController.levelSaveData.resourceManagerSaveData;

            foreach(var resourceData in saveData.resourceAmmount)
            {
                foreach(GameResourceManager.ResourceHolder holder in owner.resourceHolders)
                {
                    if (holder.data.resourceName == resourceData.resourceName)
                    {
                        holder.value = resourceData.value;
                    }
                }
            }

            
        }
    }

    public ResourceManagerSaveData Save()
    {
        GameResourceManager owner = GameResourceManager.instance;

        if (GameSaveLoadController == null)
        {
            GameSaveLoadController = GameSaveLoadController.instance;
        }

        ResourceManagerSaveData saveData = GameSaveLoadController.levelSaveData.resourceManagerSaveData;

        ResourceManagerSaveData.resourceSaveData[] datas = new ResourceManagerSaveData.resourceSaveData[owner.resourceHolders.Length];

        for(int i = 0; i< datas.Length; i++)
        {
            datas[i] = new ResourceManagerSaveData.resourceSaveData()
            {
                resourceName = owner.resourceHolders[i].data.resourceName,
                value = owner.resourceHolders[i].value,
            };
        }

        saveData.resourceAmmount = datas;
        return saveData;
    }

    [System.Serializable]
    public struct ResourceManagerSaveData
    {
        public resourceSaveData[] resourceAmmount;

        [System.Serializable]
        public struct resourceSaveData
        {
            public string resourceName;
            public float value;
        }
    }
}
