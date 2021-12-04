using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Ai system/AiAgentSpawnController")]
public class AiAgentSpawnController : ScriptableObject
{
    
    public AgentHolder[] agentPrefabs;

    public AgentHolder[] defenderPrefabs;

    public AiAgent GetRandomDefenderPrefab()
    {
        float sumWeight = 0;
        foreach (AgentHolder holder in defenderPrefabs)
        {
            sumWeight += holder.weight;
        }

        float ran = Random.Range(0, sumWeight);
        float sum = 0;
        foreach (AgentHolder holder in defenderPrefabs)
        {
            sum += holder.weight;

            if (sum >= ran)
            {
                return holder.aiAgent;
            }
        }


        return null;
    }

    public AiAgent GetRandomAgentPrefab()
    {
        float sumWeight = 0;
        foreach(AgentHolder holder in agentPrefabs)
        {
            sumWeight += holder.weight;
        }

        float ran = Random.Range(0, sumWeight);
        float sum = 0;
        foreach (AgentHolder holder in agentPrefabs)
        {
            sum += holder.weight;

            if(sum >= ran)
            {
                return holder.aiAgent;
            }
        }


        return null;
    }

    [System.Serializable]
    public class AgentHolder 
    {
        public AiAgent aiAgent;
        public float weight;
    }

}
