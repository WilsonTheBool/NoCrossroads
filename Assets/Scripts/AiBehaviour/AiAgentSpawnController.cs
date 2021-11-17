using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Ai system/AiAgentSpawnController")]
public class AiAgentSpawnController : ScriptableObject
{
    
    public AiAgent[] agentPrefabs;

    public AiAgent GetRandomAgentPrefab()
    {
        return agentPrefabs[Random.Range(0, agentPrefabs.Length)];
    }
}
