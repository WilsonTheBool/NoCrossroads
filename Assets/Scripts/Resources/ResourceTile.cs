using UnityEngine;
using System.Collections;

[RequireComponent(typeof(WorldObject))]
public class ResourceTile : MonoBehaviour
{
    public WorldObject worldObject;

    public ResourceData_SO resource;

    public ResourceIconController ResourceIconController;

    public float baseMineAmmount;

    public bool isFree;

    public UnityEngine.Events.UnityEvent OnFreeStateChanged;

    private void Awake()
    {
        worldObject = GetComponent<WorldObject>();
        ResourceIconController = GetComponent<ResourceIconController>();
        isFree = true;
    }

    private void Start()
    {
        worldObject.SetUp();
    }

    public void SetIsFree(bool value)
    {
        isFree = value;

        OnFreeStateChanged.Invoke();
    }

    public Vector3Int GetPosition()
    {
        return worldObject.worldPosition;
    }

    public float GetMineAmmount(float workEffectiveness)
    {
        return workEffectiveness * baseMineAmmount;
    }

    public void Select()
    {
        ResourceIconController.ShowIcon(isFree);
    }

    public void Deselect()
    {
        ResourceIconController.HideIcon();
    }
}
