using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Ai system/BehaviourNodes/Node_Wait")]
public class BehavioutNode_Wait : BehaviourNode
{
    public override bool CanActivate(AiBehaviourController controller, AiAgent owner)
    {
        return true;
    }

    
}
