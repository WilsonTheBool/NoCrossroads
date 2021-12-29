using UnityEngine;
using UnityEditor;

[CreateAssetMenu(menuName = "SaveLoad/Moving Character Converter")]
public class Moving_Converter : Component_Converter
{
    public override void FromJson(string data, in GameObject gameObject)
    {
        Save_Data saveData = JsonUtility.FromJson<Save_Data>(data);

        MovingCharacter moving = gameObject.GetComponent<MovingCharacter>();

        if (moving != null)
        {
            moving.movePoints = saveData.movePoints;
            moving.maxMovePoints = saveData.maxMovePoints;

        }
    }

    public override string ToJson(GameObject gameObject)
    {
        MovingCharacter moving = gameObject.GetComponent<MovingCharacter>();

        Save_Data save_Data = new Save_Data() 
        {
            movePoints = moving.movePoints,
            maxMovePoints = moving.maxMovePoints,
        };

        return JsonUtility.ToJson(save_Data);
    }

    [System.Serializable]
    private struct Save_Data
    {
        public int movePoints;
        public int maxMovePoints;
    }
}