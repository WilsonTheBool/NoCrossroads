using UnityEngine;
using UnityEditor;

[CreateAssetMenu(menuName = "SaveLoad/Attacking Character Converter")]
public class Attacking_Converter : Component_Converter
{
    public override void FromJson(string data, in GameObject gameObject)
    {
        Save_Data saveData = JsonUtility.FromJson<Save_Data>(data);

        AttackingCharacter attacking = gameObject.GetComponent<AttackingCharacter>();

        if (attacking != null)
        {
            attacking.damage = saveData.attack;

        }
    }

    public override string ToJson(GameObject gameObject)
    {
        AttackingCharacter attacking = gameObject.GetComponent<AttackingCharacter>();

        Save_Data save_Data = new Save_Data() {attack = attacking.damage};

        return JsonUtility.ToJson(save_Data);
    }

    [System.Serializable]
    private struct Save_Data
    {
        public float attack;
    }
}