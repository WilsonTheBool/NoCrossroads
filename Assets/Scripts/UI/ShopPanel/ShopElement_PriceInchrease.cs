using UnityEngine;
using System.Collections;

public class ShopElement_PriceInchrease : GameWorldMap_Dependable
{
    GameWorldMapManager GameWorldMapManager;

    [SerializeField]
    ShopElement ShopElement;

    [SerializeField]
    int priceIncreaceValue;

    [SerializeField]
    string unitTypeName;


    public override void SetUp()
    {
        GameWorldMapManager = GameWorldMapManager.instance;
        GameWorldMapManager.OnUnitCreate += GameWorldMapManager_OnUnitCreate;
        GameWorldMapManager.OnUnitDeath += GameWorldMapManager_OnUnitDeath;
    }

    private void GameWorldMapManager_OnUnitDeath(object sender, GameWorldMapManager.UnitEventArgs e)
    {
        if(e.worldObject.typeName == unitTypeName)
        {
            ShopElement?.DicreacePrice(priceIncreaceValue);
        }
    }

    private void GameWorldMapManager_OnUnitCreate(object sender, GameWorldMapManager.UnitEventArgs e)
    {
        if (e.worldObject.typeName == unitTypeName)
        {
            ShopElement?.IncreacePrice(priceIncreaceValue);
        }
    }
}
