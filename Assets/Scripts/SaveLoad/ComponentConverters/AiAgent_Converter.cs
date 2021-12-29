using UnityEngine;
using UnityEditor;

[CreateAssetMenu(menuName = "SaveLoad/AI Agent Converter")]
public class AiAgent_Converter : Component_Converter
{
    public override void FromJson(string data, in GameObject gameObject)
    {
        Save_Data saveData = JsonUtility.FromJson<Save_Data>(data);

        AiAgent agent = gameObject.GetComponent<AiAgent>();

        if (agent != null)
        {
            agent.curentTarget = saveData.target;

        }
    }

    public override string ToJson(GameObject gameObject)
    {
        AiAgent agent = gameObject.GetComponent<AiAgent>();

        Save_Data save_Data = new Save_Data() { target = agent.curentTarget };

        return JsonUtility.ToJson(save_Data);
    }

    [System.Serializable]
    private struct Save_Data
    {
        public AiAgentNest.NestTarget target;
    }
}