using UnityEngine;
using UnityEditor;


[CreateAssetMenu(menuName = "SaveLoad/ResourceTile Converter")]
public class ResourceTile_Converter : Component_Converter
{
    public override void FromJson(string data, in GameObject gameObject)
    {
        ResourceTile resourceTile = gameObject.GetComponent<ResourceTile>();

        SaveData saveData = JsonUtility.FromJson<SaveData>(data);

        resourceTile.curentResourceCount = saveData.resourceAmount;
    }

    public override string ToJson(GameObject gameObject)
    {
        ResourceTile resourceTile = gameObject.GetComponent<ResourceTile>();

        SaveData saveData = new SaveData()
        {
            resourceAmount = resourceTile.curentResourceCount,
        };

        return JsonUtility.ToJson(saveData);
    }

    [System.Serializable]
    private struct SaveData
    {
       public float resourceAmount;
    }
}