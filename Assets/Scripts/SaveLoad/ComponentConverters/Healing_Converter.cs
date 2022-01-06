using UnityEngine;
using UnityEditor;

[CreateAssetMenu(menuName = "SaveLoad/Healing Character Converter")]
public class Healing_Converter : Component_Converter
{
    public override void FromJson(string data, in GameObject gameObject)
    {
        Save_Data saveData = JsonUtility.FromJson<Save_Data>(data);

        HealingCharacter attacking = gameObject.GetComponent<HealingCharacter>();

        if (attacking != null)
        {
            attacking.healPower = saveData.healPower;

        }
    }

    public override string ToJson(GameObject gameObject)
    {
        HealingCharacter attacking = gameObject.GetComponent<HealingCharacter>();

        Save_Data save_Data = new Save_Data() { healPower = attacking.healPower };

        return JsonUtility.ToJson(save_Data);
    }

    [System.Serializable]
    private struct Save_Data
    {
        public int healPower;
    }
}