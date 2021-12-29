using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;


public class GameSaveLoadController : MonoBehaviour
{
    
    public static GameSaveLoadController instance;

    public Level_SaveData levelSaveData;

    const string gameSaveNameInPrefs = "GameSaveData";

    [HideInInspector]
    public GameWorldMapManager GameWorldMapManager;

    [HideInInspector]
    public PrefabSpawner PrefabSpawner;

    public bool freshDataOnStart;

    public bool isCurentlyLoading;

    public bool HasSaveData()
    {
        return PlayerPrefs.HasKey(gameSaveNameInPrefs);
    }

    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            SetUp();
        }
        else
        {
            Destroy(this.gameObject);
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
        if(!freshDataOnStart)
        LoadLevelDataFromDisc();
        else
        {
           // CreateEmptyLevelData();
        }
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

    public void DeleteSaveData()
    {
        PlayerPrefs.DeleteKey(gameSaveNameInPrefs);
        ClearSaveData();
    }

    public void CreateEmptyLevelData()
    {

        levelSaveData = new Level_SaveData()
        {
            sceneName = SceneManager.GetActiveScene().name,
            playTime = LevelPlayTimerController.instance.GetPlayTimeString(),
            resourceManagerSaveData = GameResourceManager.instance.save.Save(),
            upgradeSaveData = FindObjectOfType<ShopUpgrades_SaveLoadConverter>().Save(),
            ExplorationControllerSaveData = FindObjectOfType<ExplorationController_SaveLoadController>().Save(),
        };

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

        System.Array.Sort(loads, new WorldObjectSave_PriorityComparer());

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
                {
                   // load.owner.RemoveFromWorld();

                    load.owner.GetComponent<CharacterDeathController>().OnDeath_Destroy_Instant();
                }
                    
            }
        }

       
    }

    public void CreateNewObjectsFromLoad()
    {
        GameWorldMapManager = GameWorldMapManager.instance;
        PrefabSpawner = PrefabSpawner.instance;

        allSavedObjs.Sort(new SaveData_PriorityComparer());

        //Create new object no in scene on start
        foreach (WorldObject_SaveData saveData in allSavedObjs)
        {
            var prefab = PrefabSpawner.GetPrefabByTypeName(saveData.worldObjectType);

            WorldObject obj = Instantiate(prefab, GameWorldMapManager.GetTileCenterInWorld(saveData.worldPosition), Quaternion.Euler(0, 0, 0));

            obj.GetComponentInChildren<WorldObject_SaveLoadController>().LoadOverrideFromManager(saveData);

            obj.gameObject.SetActive(true);
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

    private class WorldObjectSave_PriorityComparer : IComparer<WorldObject_SaveLoadController>
    {
        public int Compare(WorldObject_SaveLoadController x, WorldObject_SaveLoadController y)
        {
            return x.priority.CompareTo(y.priority); 
        }
    }

    private class SaveData_PriorityComparer : IComparer<WorldObject_SaveData>
    {
        public int Compare(WorldObject_SaveData x, WorldObject_SaveData y)
        {
            return -x.priority.CompareTo(y.priority);
        }
    }
}
