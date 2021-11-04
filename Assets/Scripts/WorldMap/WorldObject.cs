using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class WorldObject : MonoBehaviour
{
    public bool canMove;

    public Vector3Int worldPosition;

    public UnityEvent OnPositionChnaged;

    public GameWorldMapManager mapManager;

    private void Start()
    {
        if (mapManager == null)
        {
            mapManager = GameWorldMapManager.instance;
        }

        worldPosition = mapManager.GetTilePosition(this.transform.position);
    }

    public void SetUp()
    {
        Start();
    }

    public void ChangePosition(Vector3Int newPos)
    {
        if (canMove)
        {
            worldPosition = newPos;
            OnPositionChnaged?.Invoke();
        }
    }
}
