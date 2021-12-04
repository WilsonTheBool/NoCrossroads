using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Ai system/BehaviourNodes/Node_walkRandomly")]
public class BehaviourNode_WalkRandomly : BehaviourNode
{
    public float walkProb;

    public int maxWalkDistance;

    public override bool CanActivate(AiBehaviourController controller, AiAgent owner)
    {
        return Random.Range(0f, 1f) <= walkProb;
    }

    public override void Activate(AiBehaviourController controller, AiAgent owner)
    {
        owner.MovingCharacter.UnitMovementData.SetUp(controller.NewGameMovementController, owner.WorldObject.worldPosition, maxWalkDistance);
        Vector3Int[] moveArea = owner.MovingCharacter.UnitMovementData.GetMovementArea();

        Vector3Int ranPos = moveArea[Random.Range(0, moveArea.Length)];

        owner.MovingCharacter.Move(ranPos, owner.MovingCharacter.movePoints);
    }
}
