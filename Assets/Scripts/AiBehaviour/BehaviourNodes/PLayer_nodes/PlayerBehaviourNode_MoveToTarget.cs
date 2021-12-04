using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Ai system/BehaviourNodes/Player/Move to target")]
public class PlayerBehaviourNode_MoveToTarget : BehaviourNode
{
    public override void Activate(AiBehaviourController ai_controller, AiAgent owner)
    {
        Player_AIBehaviourController controller = ai_controller.Player_AIBehaviourController;
        owner.MovingCharacter.UnitMovementData.SetUp(controller.NewGameMovementController, owner.WorldObject.worldPosition, owner.MovingCharacter.movePoints);

        Vector3Int[] movingArea = owner.MovingCharacter.UnitMovementData.GetMovementArea();

        GetMovePosition_Move(owner.curentTarget.targetPos, movingArea, owner.MovingCharacter, out Vector3Int movePos);

        owner.MovingCharacter.Move(movePos, 0);
    }

    public override bool CanActivate(AiBehaviourController ai_controller, AiAgent owner)
    {
        Player_AIBehaviourController controller = ai_controller.Player_AIBehaviourController;
        if (owner.curentTarget != null)
        {
            return true;
        }

        return false;
    }
}
