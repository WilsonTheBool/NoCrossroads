using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Ai system/BehaviourNodes/Node_AttackNearest")]
public class BehaviourNode_AttackNearest : BehaviourNode
{
    [SerializeField]
    int visionRnage;

    public TargetSelectModule TargetSelectModule;

    public override bool CanActivate(AiBehaviourController controller, AiAgent owner)
    {
        return controller.GetAllEnemiesInRange(owner.WorldObject.worldPosition, visionRnage).Length != 0;
    }

    public override void Activate(AiBehaviourController controller, AiAgent owner)
    {
        owner.MovingCharacter.UnitMovementData.SetUp(controller.NewGameMovementController, owner.WorldObject.worldPosition, owner.MovingCharacter.movePoints);

        Vector3Int[] movingArea = owner.MovingCharacter.UnitMovementData.GetMovementArea();

        AiTargetData target = TargetSelectModule.GetTarget(controller.GetAllEnemiesInRange(owner.WorldObject.worldPosition, visionRnage), owner);

        if (GetMovePosition(target.WorldObject.worldPosition, movingArea, out Vector3Int movePos))
        {
            owner.MovingCharacter.Move(movePos, 0);
            owner.AttackingCharacter.Attack(target.KillableCharacter);
        }
        else
        {
            owner.MovingCharacter.Move(movePos, 0);
        }
        
        
    }



    
}
