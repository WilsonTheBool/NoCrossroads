using UnityEngine;
using UnityEditor;

[CreateAssetMenu(menuName = "SaveLoad/Killable Converter")]
public class Killable_Converter : Component_Converter
{
    public override void FromJson(string data, in GameObject gameObject)
    {
        Save_Data saveData = JsonUtility.FromJson<Save_Data>(data);

        KillableCharacter killableCharacter = gameObject.GetComponent<KillableCharacter>();
        
        if(killableCharacter != null)
        {
            killableCharacter.maxHp = saveData.maxHp;
            killableCharacter.hp = saveData.Hp;
        }
    }

    public override string ToJson(GameObject gameObject)
    {
        KillableCharacter killable = gameObject.GetComponent<KillableCharacter>();
        Save_Data save_Data = new Save_Data() { Hp = killable.hp, maxHp = killable.maxHp };

        return JsonUtility.ToJson(save_Data);
    }

    [System.Serializable]
    private struct Save_Data
    {
        public float maxHp;

        public float Hp;
    }
}