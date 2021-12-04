using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class RenderObject : MonoBehaviour
{
    GameWorld_ExplorationController GameWorld_ExplorationController;
    GameWorldMapManager GameWorldMapManager;

    [HideInInspector]
    public WorldObject WorldObject;

    public UnityEvent OnFadeStart;
    public UnityEvent OnFadeEnd;

    private void Start()
    {
        GameWorld_ExplorationController = GameWorld_ExplorationController.instance;
        GameWorldMapManager = GameWorldMapManager.instance;

        WorldObject = GetComponent<WorldObject>();

        //if (GameWorld_ExplorationController != null &&
        //        GameWorld_ExplorationController.IsExploredFromGlobal(GameWorldMapManager.mainTilemap.WorldToCell(transform.position)))
        //{
        //    OnFadeEnd?.Invoke();
        //}
        //else
        //{
        //    OnFadeStart?.Invoke();
        //}
    }
}
