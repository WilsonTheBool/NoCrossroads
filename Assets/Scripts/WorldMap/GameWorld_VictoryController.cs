using UnityEngine;
using System.Collections;

public class GameWorld_VictoryController : MonoBehaviour
{
    public GameObject victoryScreeen;

    int count;

    private void Start()
    {
        GameWorldMapManager.instance.OnUnitDeath += Instance_OnUnitDeath;

        count = FindObjectsOfType<AiAgentNest>().Length;
    }

    private void Instance_OnUnitDeath(object sender, GameWorldMapManager.UnitEventArgs e)
    {
        if(e.worldObject.TryGetComponent<AiAgentNest>(out AiAgentNest aiAgentNest))
        {
            count--;
            if(count <= 0)
            {
                ShowVictoryScreen();
            }
        }
    }

    public void ShowVictoryScreen()
    {
        victoryScreeen.SetActive(true);
    }
}
