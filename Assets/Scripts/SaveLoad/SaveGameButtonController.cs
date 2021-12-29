using UnityEngine;
using System.Collections;

public class SaveGameButtonController : MonoBehaviour
{
    public GameSaveLoadController GameSaveLoadController;

    public UnityEngine.Events.UnityEvent OnCanLoad;
    public UnityEngine.Events.UnityEvent OnCanNotLoad;

   

    // Use this for initialization
    void Start()
    {
        GameSaveLoadController = GameSaveLoadController.instance;

        
        if (GameSaveLoadController != null && GameSaveLoadController.HasSaveData())
        {
            OnCanLoad.Invoke();
        }
        else
        {
            OnCanNotLoad.Invoke();
        }
    }

    public void ClearSaveData()
    {
        print("Save data cleared");

        GameSaveLoadController.DeleteSaveData();
    }

    public void SaveGame()
    {
        //GameSaveLoadController = GameSaveLoadController.instance;
        GameSaveLoadController.SaveLevelDataToDisc();
    }


}
