using UnityEngine;
using System.Collections;

public class ExplorationController_SaveLoadController : MonoBehaviour
{

    public GameSaveLoadController GameSaveLoadController;
    public void Load()
    {
        GameWorld_ExplorationController owner = GameWorld_ExplorationController.instance;

        if (GameSaveLoadController == null)
        {
            GameSaveLoadController = GameSaveLoadController.instance;
        }

        if (GameSaveLoadController.isCurentlyLoading)
        {
            ExplorationControllerSaveData saveData = GameSaveLoadController.levelSaveData.ExplorationControllerSaveData;

            int size_0 = owner.isExploresArray.GetLength(0);
            int size_1 = owner.isExploresArray.GetLength(1);

            for (int i = 0; i < size_0; i++)
            {
                for (int j = 0; j < size_1; j++)
                {
                    if(saveData.isExploredArray[i*size_1 +j])
                    owner.Explore(new Vector3Int(i, j, 0));
                }
            }

        }
    }

    public ExplorationControllerSaveData Save()
    {
        GameWorld_ExplorationController owner = GameWorld_ExplorationController.instance;

        if (GameSaveLoadController == null)
        {
            GameSaveLoadController = GameSaveLoadController.instance;
        }

        ExplorationControllerSaveData saveData = GameSaveLoadController.levelSaveData.ExplorationControllerSaveData;

        int size_0 = owner.isExploresArray.GetLength(0);
        int size_1 = owner.isExploresArray.GetLength(1);
        bool[] array = new bool[size_1 * size_0];

        for(int i = 0; i < size_0; i++)
        {
            for (int j = 0; j < size_1; j++)
            {
                array[i * size_1 + j] = owner.isExploresArray[i, j];
            }
        }

        saveData.isExploredArray = array;
        return saveData;
    }

    [System.Serializable]
    public struct ExplorationControllerSaveData
    {
        public bool[] isExploredArray;
    }
}
