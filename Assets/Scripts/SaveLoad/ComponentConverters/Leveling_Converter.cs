using UnityEngine;
using UnityEditor;

[CreateAssetMenu(menuName = "SaveLoad/Leveling Character Converter")]
public class Leveling_Converter : Component_Converter
{
    public override void FromJson(string data, in GameObject gameObject)
    {
        Save_Data saveData = JsonUtility.FromJson<Save_Data>(data);

        LevelingCharacter levelingCharacter = gameObject.GetComponent<LevelingCharacter>();

        if (levelingCharacter != null)
        {
            levelingCharacter.curentExp = saveData.curentExp;
            levelingCharacter.newLevelExp = saveData.maxExp;
            levelingCharacter.curentLevel = saveData.level;
        }
    }

    public override string ToJson(GameObject gameObject)
    {
        LevelingCharacter levelingCharacter = gameObject.GetComponent<LevelingCharacter>();
        Save_Data save_Data = new Save_Data() 
        {
            curentExp = levelingCharacter.curentExp, 
            maxExp = levelingCharacter.newLevelExp, 
            level = levelingCharacter.curentLevel
        };

        return JsonUtility.ToJson(save_Data);
    }

    [System.Serializable]
    private struct Save_Data
    {
        public float curentExp;

        public float maxExp;

        public int level;
    }
}