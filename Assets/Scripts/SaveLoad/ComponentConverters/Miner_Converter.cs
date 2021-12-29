using UnityEngine;
using UnityEditor;

[CreateAssetMenu(menuName = "SaveLoad/Miner Character Converter")]
public class Miner_Converter : Component_Converter
{
    public override void FromJson(string data, in GameObject gameObject)
    {
        Save_Data saveData = JsonUtility.FromJson<Save_Data>(data);

        Miner_Structure creator = gameObject.GetComponent<Miner_Structure>();

        if (creator != null)
        {
            creator.workEffectiveness = saveData.workEffective;

        }
    }

    public override string ToJson(GameObject gameObject)
    {
        Miner_Structure creator = gameObject.GetComponent<Miner_Structure>();

        Save_Data save_Data = new Save_Data() { workEffective = creator.workEffectiveness };

        return JsonUtility.ToJson(save_Data);
    }

    [System.Serializable]
    private struct Save_Data
    {
        public float workEffective;
    }
}