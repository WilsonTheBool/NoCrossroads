using UnityEngine;
using System.Collections;

public class CreateSelfTargetOnCreate : MonoBehaviour
{

    private AiAgent aiAgent;

    private WorldObject WorldObject;

    private void Awake()
    {
        WorldObject = GetComponent<WorldObject>();
        aiAgent = GetComponent<AiAgent>();
        aiAgent.WorldObject.OnSetUpComplete.AddListener(CreateTarget);
    }

    void CreateTarget()
    {
        aiAgent.curentTarget = new AiAgentNest.NestTarget() { targetPos = aiAgent.WorldObject.worldPosition, maxAgents = 1 };
    }
}
