using UnityEngine;
using System.Collections;

public class Leech_ResourceTile : MonoBehaviour
{
    public ResourceTile mainResourceTile;

    public ResourceTile leechTile;

    private void Awake()
    {
        leechTile.maxResourceAmmount = mainResourceTile.maxResourceAmmount;
        leechTile.baseMineAmmount = mainResourceTile.baseMineAmmount;
        leechTile.curentResourceCount = mainResourceTile.curentResourceCount;

        mainResourceTile.OnMine.AddListener(Update_Leech);
        mainResourceTile.OnEmpty.AddListener(EmptyLeech);
        leechTile.OnEmpty.AddListener(EmptyMain);
        leechTile.OnMine.AddListener(Update_Main);
    }

    public void EmptyLeech()
    {
        mainResourceTile.OnEmpty.RemoveListener(EmptyLeech);
        leechTile.OnEmpty.RemoveListener(EmptyMain);
        leechTile.OnEmpty.Invoke();
    }

    public void EmptyMain()
    {
        mainResourceTile.OnEmpty.RemoveListener(EmptyLeech);
        leechTile.OnEmpty.RemoveListener(EmptyMain);
        mainResourceTile.OnEmpty.Invoke();
    }

    public void Update_Main()
    {
        mainResourceTile.curentResourceCount = leechTile.curentResourceCount;
        
    }

    public void Update_Leech()
    {
        leechTile.curentResourceCount = mainResourceTile.curentResourceCount;

    }
}
