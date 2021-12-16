using UnityEngine;
using UnityEditor;

[CreateAssetMenu(menuName = "Ai system/BehaviourNodes/Player/Return to position")]
public class PlayerBehaviourNode_ReturnToPosition : BehaviourNode
{
    public float maxDistanceFromPosition;

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
        if (owner.curentTarget != null && Vector3Int.Distance(owner.WorldObject.worldPosition, owner.curentTarget.targetPos) > maxDistanceFromPosition)
        {
            return true;
        }

        return false;
    }
}