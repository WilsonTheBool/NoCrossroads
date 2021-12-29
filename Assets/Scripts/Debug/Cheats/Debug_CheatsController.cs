using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Debug_CheatsController : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this);

    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            GiveResources(100);
        }

        if (Input.GetKeyDown(KeyCode.F2))
        {
            HideShowMainCanvas();
        }

        if (Input.GetKeyDown(KeyCode.F3))
        {
            HideShowWorldCanvas();
        }

        if (Input.GetKeyDown(KeyCode.F4))
        {
            ExploreAll();
        }

        if (Input.GetKeyDown(KeyCode.F5))
        {
            GiveEveryOneExp(100);
        }
    }

    public void GiveResources(int ammount)
    {
        GameResourceManager gameResourceManager = GameResourceManager.instance;

        foreach(var holder in gameResourceManager.resourceHolders)
        {
            gameResourceManager.AddResource(holder.data, ammount);
        }
    }

    public void HideShowMainCanvas()
    {
        Canvas [] canvases = FindObjectsOfType<Canvas>(true);

        foreach(Canvas canvas in canvases)
        {
            if(canvas.renderMode != RenderMode.WorldSpace)
            {
                if (canvas.gameObject.activeSelf)
                {
                    canvas.gameObject.SetActive(false);
                }
                else
                {
                    canvas.gameObject.SetActive(true);
                }
            }
        }
    }

    public void ExploreAll()
    {
        GameWorld_ExplorationController gameWorld_ExplorationController = GameWorld_ExplorationController.instance;

        gameWorld_ExplorationController.ExploreAll();
    }

    public void GiveEveryOneExp(int ammount)
    {
        LevelingCharacter[] levelingCharacters = FindObjectsOfType<LevelingCharacter>();

        foreach(LevelingCharacter levelingCharacter in levelingCharacters)
        {
            if(levelingCharacter.canLevelUp)
            levelingCharacter.GainExp(ammount);
        }
    }

    public void HideShowWorldCanvas()
    {
        Canvas[] canvases = FindObjectsOfType<Canvas>(true);

        foreach (Canvas canvas in canvases)
        {
            if (canvas.renderMode == RenderMode.WorldSpace)
            {
                if (canvas.gameObject.activeSelf)
                {
                    canvas.gameObject.SetActive(false);
                }
                else
                {
                    canvas.gameObject.SetActive(true);
                }
            }
        }
    }
}
