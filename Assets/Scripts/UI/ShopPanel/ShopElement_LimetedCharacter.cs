using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class ShopElement_LimetedCharacter : GameWorldMap_Dependable
{

    public string typeName;

    public int maxCount;

    private int curentCount;

    public UnityEvent OnClose;
    public UnityEvent OnOpen;


    public override void SetUp()
    {
        GameWorldMapManager.instance.OnUnitCreate += Instance_OnUnitCreate;
        GameWorldMapManager.instance.OnUnitDeath += Instance_OnUnitDeath;
    }

    private void Instance_OnUnitDeath(object sender, GameWorldMapManager.UnitEventArgs e)
    {
        

        if(e.worldObject.typeName == typeName)
        {
            if (curentCount == maxCount)
            {
                OnOpen?.Invoke();
            }

            curentCount--;
        }
    }

    private void Instance_OnUnitCreate(object sender, GameWorldMapManager.UnitEventArgs e)
    {
        if (e.worldObject.typeName == typeName)
        {
            curentCount++;

            if (curentCount == maxCount)
            {
                OnClose?.Invoke();
            }
        }
    }
}
