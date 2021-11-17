using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class WorldObject : MonoBehaviour
{
    public bool canMove;

    public Vector3Int worldPosition;

    public UnityEvent OnPositionChnaged;

    public GameWorldMapManager mapManager;

    public bool blockMovement;

    public void SetUp()
    {
        if (mapManager == null)
        {
            mapManager = GameWorldMapManager.instance;
        }

        worldPosition = mapManager.GetTilePosition(this.transform.position);
        mapManager.AddWorldObject(this);
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
        mapManager.RemoveWorldObject(this);
    }

    private void OnDestroy()
    {
        
    }
}
