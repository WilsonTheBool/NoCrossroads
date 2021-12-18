using UnityEngine;
using System.Collections;

public class WorldObject_SaveLoadController : MonoBehaviour
{
    public bool removeOnStartIfNoData;

    public WorldObject owner;
    public Component_Converter[] component_Converters;


    public WorldObject_SaveData CreateSaveData()
    {
        WorldObject_SaveData data = new WorldObject_SaveData();
        data.worldPosition = owner.worldPosition;
        data.component_SaveDatas = new Component_SaveData[component_Converters.Length];
        data.worldObjectType = owner.typeName;
        for(int i = 0; i < component_Converters.Length; i++)
        {
            data.component_SaveDatas[i] = new Component_SaveData()
            {
                converterType = component_Converters[i].GetType().Name,
                data = component_Converters[i].ToJson(owner.gameObject)
            };
        }

        return data;
    }

    public void LoadOverrideFromManager(WorldObject_SaveData data)
    {
        owner.worldPosition = data.worldPosition;



        foreach(Component_SaveData cData in data.component_SaveDatas)
        {
            SetComponentData(cData);
        }
    }

    private void SetComponentData(Component_SaveData data)
    {
        foreach(Component_Converter component_Converter in component_Converters)
        {
            if (System.Type.GetType(data.converterType) == component_Converter.GetType())
            {
                component_Converter.FromJson(data.data, owner.gameObject);
            }
        }
    }
}
