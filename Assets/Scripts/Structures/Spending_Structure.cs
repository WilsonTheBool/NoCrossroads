using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class Spending_Structure : MonoBehaviour
{
    public GameResourceManager.ResourceHolder[] resourcesSpendPerTurn;

    GameResourceManager resourceManager;

    public UnityEvent OnCantEat;

    private void Start()
    {
        resourceManager = GameResourceManager.instance;

    }

    public bool CanEat()
    {
        foreach(GameResourceManager.ResourceHolder holder in resourcesSpendPerTurn)
        {
            if(!resourceManager.HasMoreThanXResource(holder.data, holder.value))
            {
                
                return false;
            }
        }

        return true;
    }

    public void TryEat()
    {
        if (CanEat())
        {
            Eat();
        }
        else
        {
            OnCantEat?.Invoke();
        }
    }

    private void Eat()
    {
        foreach (GameResourceManager.ResourceHolder holder in resourcesSpendPerTurn)
        {
            resourceManager.RemoveResource(holder.data, holder.value);
        }
    }
}
