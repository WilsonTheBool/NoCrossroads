using UnityEngine;
using System.Collections;

public class BehaviourNode_ReturnToNest : BehaviourNode
{
    public float maxDistanceFromNest;

    public override void Activate(AiBehaviourController controller, AiAgent owner)
    {
        owner.MovingCharacter.UnitMovementData.SetUp(controller.NewGameMovementController, owner.WorldObject.worldPosition, owner.MovingCharacter.movePoints);

        Vector3Int[] movingArea = owner.MovingCharacter.UnitMovementData.GetMovementArea();

        GetMovePosition(owner.nest.WorldObject.worldPosition, movingArea, out Vector3Int movePos);

        owner.MovingCharacter.Move(movePos, 0);

    }

    public override bool CanActivate(AiBehaviourController controller, AiAgent owner)
    {
        return owner.nest != null && Vector3Int.Distance(owner.WorldObject.worldPosition, owner.nest.WorldObject.worldPosition) > maxDistanceFromNest;
    }
}
