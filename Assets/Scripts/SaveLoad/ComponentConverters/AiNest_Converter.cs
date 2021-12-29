using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SaveLoad/AI Nest Converter")]
public class AiNest_Converter : Component_Converter
{
    public override void FromJson(string data, in GameObject gameObject)
    {
        Save_Data saveData = JsonUtility.FromJson<Save_Data>(data);

        AiAgentNest agent = gameObject.GetComponent<AiAgentNest>();

        if (agent != null)
        {
            agent.curentTarget = saveData.target;
            agent.turnCount = saveData.turnCount;
        }
    }

    public override string ToJson(GameObject gameObject)
    {
        AiAgentNest agent = gameObject.GetComponent<AiAgentNest>();

        Save_Data save_Data = new Save_Data() { target = agent.curentTarget , turnCount = agent.turnCount};

        return JsonUtility.ToJson(save_Data);
    }

    [System.Serializable]
    private struct Save_Data
    {
        public int turnCount;
        public AiAgentNest.NestTarget target;
    }
}

