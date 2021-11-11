using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class SelectableObject : MonoBehaviour
{
    public UnitData_SO data;

    public SelectModule[] selectModules;

    public UnityEvent OnSelect;
    public UnityEvent OnDeselect;

    public bool isPlayerSide;

    public void Select_Start(GameInputData data, SelectModule.SelectEventArgs selectEventArgs)
    {
        foreach(SelectModule module in selectModules)
        {
            module.OnSelect_Start(data, selectEventArgs);
        }

        OnSelect.Invoke();
    }

    public void Select_End(GameInputData data, SelectModule.SelectEventArgs selectEventArgs)
    {
        foreach (SelectModule module in selectModules)
        {
            module.OnSelect_End(data, selectEventArgs);
        }

        OnDeselect.Invoke();
    }

    public void Select_Update(GameInputData data, SelectModule.SelectEventArgs selectEventArgs)
    {
        foreach (SelectModule module in selectModules)
        {
            module.OnSelect_Update(data, selectEventArgs);
        }
    }

    public void Seleect_Accept(GameInputData data, SelectModule.SelectEventArgs selectEventArgs)
    {
        foreach (SelectModule module in selectModules)
        {
            module.OnSelect_AcceptPressed(data, selectEventArgs);
        }
    }
}
