using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Ai system/TargetSelectNode")]
public class TargetSelectModule : ScriptableObject
{

    public float distanceCost;

    public float hpCost;



    public virtual AiTargetData GetTarget(AiTargetData[] targets, AiAgent owner)
    {
        float maxValue = float.MinValue;
        AiTargetData bestTarget = null;

        foreach(AiTargetData targetData in targets)
        {
            float value = CalculateValue(targetData, owner);

            if(value > maxValue)
            {
                maxValue = value;
                bestTarget = targetData;
            }
        }

        return bestTarget;
    }

    public virtual float CalculateValue(AiTargetData data, AiAgent owner)
    {
        float totalValue = 0;

        MovingCharacter movingCharacter = owner.MovingCharacter;
        AttackingCharacter attacking = owner.AttackingCharacter;

        int pathCost = movingCharacter.UnitMovementData.GetPathCost_Attack(data.WorldObject.worldPosition);

        if(pathCost <= movingCharacter.movePoints)
        {
            totalValue += 100;

        }

        if(data.KillableCharacter.hp <= attacking.damage)
        {
            totalValue += 70;
        }

        totalValue -= data.KillableCharacter.hp;
        totalValue -= Vector3Int.Distance(data.WorldObject.worldPosition, owner.WorldObject.worldPosition) * distanceCost;

        //Debug.Log("TargetData: " + owner.name);
        //Debug.Log("Target: " + data.WorldObject.name + ": " + totalValue);
        return totalValue;
    }
}
