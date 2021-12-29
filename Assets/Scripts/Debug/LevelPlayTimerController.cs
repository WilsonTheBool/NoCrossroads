using UnityEngine;
using System.Collections;
using System;

public class LevelPlayTimerController : MonoBehaviour
{
    public static LevelPlayTimerController instance;

    public GameSaveLoadController GameSaveLoadController;

    private double loadedPlayTimeInSeconds;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(this);

        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        GameSaveLoadController = GameSaveLoadController.instance;

        if(GameSaveLoadController != null)
        {
            LoadPlayTime();
        }
    }

    private void LoadPlayTime()
    {
        if (GameSaveLoadController.isCurentlyLoading)
        {
            loadedPlayTimeInSeconds = TimeSpan.Parse(GameSaveLoadController.levelSaveData.playTime).TotalSeconds;
        }
        else
        {
            loadedPlayTimeInSeconds = 0;
        }
    }

    public string GetPlayTimeString()
    {
        double totalGameTime = loadedPlayTimeInSeconds + System.Math.Round(Time.timeSinceLevelLoadAsDouble);

        TimeSpan time = TimeSpan.FromSeconds(totalGameTime);

        return time.ToString("c");
    }
}
