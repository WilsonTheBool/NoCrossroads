using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ai system/BehaviourNodes/Player/Node_AttackNearest")]
public class PLayerBehaviourNode_AttackNearest : BehaviourNode
{
    [SerializeField]
    int visionRnage;

    public TargetSelectModule TargetSelectModule;

    public override bool CanActivate(AiBehaviourController ai_controller, AiAgent owner)
    {
        Player_AIBehaviourController controller = ai_controller.Player_AIBehaviourController;
        return controller.GetAllEnemiesInRange(owner.WorldObject.worldPosition, visionRnage).Length != 0;
    }

    public override void Activate(AiBehaviourController ai_controller, AiAgent owner)
    {
        Player_AIBehaviourController controller = ai_controller.Player_AIBehaviourController;

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

