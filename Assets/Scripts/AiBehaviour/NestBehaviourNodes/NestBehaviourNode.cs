using UnityEngine;
using System.Collections;

public class NestBehaviourNode : MonoBehaviour
{
    public AiAgentNest nest;

    [HideInInspector]
    public AiBehaviourController AiBehaviourController;

    public virtual bool TryActivate()
    {
        return false;
    }

    protected virtual bool CanActivate()
    {
        return false;
    }

    protected virtual void Activate()
    {

    }


}
