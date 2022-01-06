using UnityEngine;
using System.Collections;

public class GameOverController : GameWorldMap_Dependable
{
    public GameObject VictoryScreen;

    private int Count;

    GameWorldMapManager gameWorldMapManager;

    public Canvas mainCanvas;

    public override void SetUp()
    {
        gameWorldMapManager = GameWorldMapManager.instance;
        gameWorldMapManager.OnUnitCreate += GameWorldMapManager_OnUnitCreate;
        gameWorldMapManager.OnUnitDeath += GameWorldMapManager_OnUnitDeath;
    }

    private void GameWorldMapManager_OnUnitDeath(object sender, GameWorldMapManager.UnitEventArgs e)
    {
        if (e.worldObject.TryGetComponent<AiAgentNest>(out AiAgentNest nest))
        {
            Count--;

            if(Count <= 0)
            {
                ShowVictoryScreen();
            }
        }
    }

    private Canvas FindMainCanvas()
    {
        foreach(Canvas canvas in FindObjectsOfType<Canvas>())
        {
            if (canvas.CompareTag("MainCanvas"))
            {
                return canvas;
            }
        }

        return null;
    }

    private void ShowVictoryScreen()
    {
        if(mainCanvas == null)
        {
            mainCanvas = FindMainCanvas();
        }

        Instantiate(VictoryScreen, mainCanvas.transform);
    }

    private void GameWorldMapManager_OnUnitCreate(object sender, GameWorldMapManager.UnitEventArgs e)
    {
        if(e.worldObject.TryGetComponent<AiAgentNest>(out AiAgentNest nest))
        {
            Count++;
        }
    }
}
