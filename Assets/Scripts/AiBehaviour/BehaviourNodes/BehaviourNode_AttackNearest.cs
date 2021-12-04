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
        owner.MovingCharacter.UnitMovementData.SetUp(controller.NewGameMovementController, owner.WorldObject.worldPosition, UnitMovementData.UnitsVisionRange);

        Vector3Int[] visionArea = owner.MovingCharacter.UnitMovementData.GetMovementArea();
        Vector3Int[] movingArea = owner.MovingCharacter.UnitMovementData.GetMovementArea(owner.MovingCharacter.movePoints);

        AiTargetData target = TargetSelectModule.GetTarget(controller.GetAllEnemiesInRange(owner.WorldObject.worldPosition, visionRnage), owner);

        if (GetMovePosition(target.WorldObject.worldPosition, visionArea, owner.MovingCharacter, out Vector3Int movePos))
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
