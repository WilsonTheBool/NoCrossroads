using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Ai system/BehaviourNodes/Move to nest target")]
public class BehavioutNode_MoveToTarget : BehaviourNode
{
    public override void Activate(AiBehaviourController controller, AiAgent owner)
    {
        owner.MovingCharacter.UnitMovementData.SetUp(controller.NewGameMovementController, owner.WorldObject.worldPosition, owner.MovingCharacter.movePoints);

        Vector3Int[] movingArea = owner.MovingCharacter.UnitMovementData.GetMovementArea();

        GetMovePosition(owner.curentTarget.targetPos, movingArea, out Vector3Int movePos);

        owner.MovingCharacter.Move(movePos, 0);
    }

    public override bool CanActivate(AiBehaviourController controller, AiAgent owner)
    {
        if (owner.curentTarget != null)
        {
            return true;
        }
        else
        {
            if (owner.nest != null && owner.nest.curentTarget != null && owner.nest.curentTarget.CanAddAgent())
            {
                Debug.Log("Add agent:" + owner.name);
                owner.curentTarget = owner.nest.curentTarget;
                owner.curentTarget.AddAgent();
                return true;
            }
        }

        return false;
    }
}

