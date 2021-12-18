using UnityEngine;
using System.Collections.Generic;


public class GameSaveLoadController : MonoBehaviour
{
    public static GameSaveLoadController instance;

    public Level_SaveData levelSaveData;

    const string gameSaveNameInPrefs = "GameSaveData";

    public GameWorldMapManager GameWorldMapManager;

    public PrefabSpawner PrefabSpawner;

    public bool HasSaveData()
    {
        return levelSaveData != null;
    }

    public void Awake()
    {
        if(true)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            SetUp();
        }
        //else
        //{
        //    instance = this;
        //    DontDestroyOnLoad(this.gameObject);
        //    SetUp();
        //}

    }

    private void SetUp()
    {
        LoadLevelDataFromDisc();
    }

    public void SaveLevelDataToDisc()
    {
        CreateEmptyLevelData();
        string data = JsonUtility.ToJson(levelSaveData, true);
        PlayerPrefs.SetString(gameSaveNameInPrefs, data);
        PlayerPrefs.Save();
        print(data);

    }

    public void LoadLevelDataFromDisc()
    {
        if (PlayerPrefs.HasKey(gameSaveNameInPrefs))
        {
            levelSaveData = JsonUtility.FromJson<Level_SaveData>(PlayerPrefs.GetString(gameSaveNameInPrefs));
            
        }
    }

    public void ClearSaveData()
    {
        levelSaveData = null;
    }

    public void CreateEmptyLevelData()
    {
        levelSaveData = new Level_SaveData() { sceneName = "Save test", playTime = "a lot"};
        var objs = FindObjectsOfType<WorldObject_SaveLoadController>(false);
        List<WorldObject_SaveData> savedatas = new List<WorldObject_SaveData>();
        foreach(WorldObject_SaveLoadController obj in objs)
        {
            savedatas.Add(obj.CreateSaveData());
        }

        levelSaveData.allSavedObjects = savedatas.ToArray();
    }

    List<WorldObject_SaveData> allSavedObjs;

    //for all objects in scene on start
    public void LoadAllWorldObjects() 
    {
        allSavedObjs = new List<WorldObject_SaveData>(levelSaveData.allSavedObjects);

        WorldObject_SaveLoadController[] loads = FindObjectsOfType<WorldObject_SaveLoadController>(false);

        //update object in scene on start
        foreach(WorldObject_SaveLoadController load in loads)
        {
            
            if(TryFindObjectSaveData(load.owner, out WorldObject_SaveData data))
            {
                load.LoadOverrideFromManager(data);
                allSavedObjs.Remove(data);
            }
            else
            {
                if (load.removeOnStartIfNoData)
                    if (load.transform.parent != null)
                    Destroy(load.transform.parent.gameObject);
                else
                    Destroy(load.gameObject);
            }
        }

       
    }

    public void CreateNewObjectsFromLoad()
    {
        //Create new object no in scene on start
        foreach (WorldObject_SaveData saveData in allSavedObjs)
        {
            var prefab = PrefabSpawner.GetPrefabByTypeName(saveData.worldObjectType);

            WorldObject obj = Instantiate(prefab, GameWorldMapManager.GetTileCenterInWorld(saveData.worldPosition), Quaternion.Euler(0, 0, 0));

            obj.GetComponentInChildren<WorldObject_SaveLoadController>().LoadOverrideFromManager(saveData);
        }
    }

    private bool TryFindObjectSaveData(WorldObject worldObject, out WorldObject_SaveData data)
    {
        foreach(WorldObject_SaveData saveData in allSavedObjs)
        {
            if(saveData.worldPosition == worldObject.worldPosition && saveData.worldObjectType == worldObject.typeName)
            {
                data = saveData;
                return true;
            }
        }

        data = null;
        return false;
    }
}
