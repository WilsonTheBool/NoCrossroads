using UnityEngine;
using UnityEditor;

[CreateAssetMenu(menuName = "SaveLoad/Terrirtory Character Converter")]
public class TerritoryCreator_Converter : Component_Converter
{
    public override void FromJson(string data, in GameObject gameObject)
    {
        Save_Data saveData = JsonUtility.FromJson<Save_Data>(data);

        TerritoryCreator creator = gameObject.GetComponent<TerritoryCreator>();

        if (creator != null)
        {
            creator.createRadius = saveData.createRange;

        }
    }

    public override string ToJson(GameObject gameObject)
    {
        TerritoryCreator creator = gameObject.GetComponent<TerritoryCreator>();

        Save_Data save_Data = new Save_Data() { createRange = creator.createRadius };

        return JsonUtility.ToJson(save_Data);
    }

    [System.Serializable]
    private struct Save_Data
    {
        public int createRange;
    }
}