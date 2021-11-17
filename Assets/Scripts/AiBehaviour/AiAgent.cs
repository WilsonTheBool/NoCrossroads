using UnityEngine;
using System.Collections;

public class AiAgent : MonoBehaviour
{
    public MovingCharacter MovingCharacter;

    public AttackingCharacter AttackingCharacter;

    public WorldObject WorldObject;

    public KillableCharacter killable;

    [HideInInspector]
    public AiBehaviourController AiBehaviourController;

    public BehaviourNode[] behaviourNodes;

    public AiAgentNest nest;

    public AiAgentNest.NestTarget curentTarget;

    public float GetActionDelay()
    {
        foreach (BehaviourNode node in behaviourNodes)
        {
            if (node.CanActivate(AiBehaviourController, this))
            {
               
                return node.actionTimeDelay;
            }
        }

        return 0;
    }

    private void Start()
    {
        AiBehaviourController = AiBehaviourController.instance;
        
    }

    public void DoAction()
    {
        foreach(BehaviourNode node in behaviourNodes)
        {
            if(node.CanActivate(AiBehaviourController, this))
            {
                node.Activate(AiBehaviourController, this);
                return;
            }
        }
    }
}
