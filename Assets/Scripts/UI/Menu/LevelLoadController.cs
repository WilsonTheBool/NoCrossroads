using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelLoadController : MonoBehaviour
{
    public GameSaveLoadController GameSaveLoadController;

    public string LevelName;

    private void Start()
    {
        GameSaveLoadController = GameSaveLoadController.instance;
    }

    public void LoadLevel_NewGame()
    {
        GameSaveLoadController.isCurentlyLoading = false;

        SceneManager.LoadScene(LevelName);
    }

    public void LoadLevel_Continue()
    {
        if(GameSaveLoadController != null && GameSaveLoadController.HasSaveData())
        {
            GameSaveLoadController.isCurentlyLoading = true;

            SceneManager.LoadScene(GameSaveLoadController.levelSaveData.sceneName);
        }

        
    }
}
