using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class WorldObject : MonoBehaviour
{
    public string typeName;

    public bool canMove;

    public Vector3Int worldPosition;

    public UnityEvent OnPositionChnaged;

    public GameWorldMapManager mapManager;

    public bool blockMovement;

    public bool pathableForPlayer;
    public bool pathableForEnemy;

    public UnityEvent OnSetUpComplete;
    public UnityEvent OnRemoveFromWorld;

    private bool isSetUpComplete = false;

    private void Start()
    {
        if (GameWorldMapManager.instance != null && !isSetUpComplete && GameWorldMapManager.instance.isSetUpComplete)
        {
            SetUp();
        }
    }

    public void SetUp()
    {
        if (mapManager == null)
        {
            mapManager = GameWorldMapManager.instance;
        }

        worldPosition = mapManager.GetTilePosition(this.transform.position);
        OnSetUpComplete.Invoke();

        mapManager.AddWorldObject(this);

        isSetUpComplete = true;
    }

    public void ChangePosition(Vector3Int newPos)
    {
        if (canMove)
        {
            worldPosition = newPos;
            OnPositionChnaged?.Invoke();
        }
    }

    public void RemoveFromWorld()
    {
        if(mapManager != null)
        {
            mapManager.RemoveWorldObject(this);
            OnRemoveFromWorld.Invoke();
        }
        
    }

    private void OnDestroy()
    {
        
    }
}
