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

    public float maxResourceAmmount;

    public float curentResourceCount;

    public UnityEngine.Events.UnityEvent OnEmpty;
    public UnityEngine.Events.UnityEvent OnMine;


    private void Awake()
    {
        worldObject = GetComponent<WorldObject>();
        ResourceIconController = GetComponent<ResourceIconController>();
        OnEmpty.AddListener(Deselect);
        isFree = true;
    }

    public virtual float Mine(float value)
    {
        if(curentResourceCount - value > 0)
        {
            curentResourceCount -= value;

            OnMine.Invoke();
            return value;
        }
        else
        {
            float returnValue = value + (curentResourceCount - value);

            curentResourceCount -= value;

            if (curentResourceCount <= 0)
            {
                OnEmpty.Invoke();
            }
            OnMine.Invoke();
            return returnValue;
        }

    }

    private void Start()
    {
        //worldObject.SetUp();
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

    public virtual float GetMineAmmount(float workEffectiveness)
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
