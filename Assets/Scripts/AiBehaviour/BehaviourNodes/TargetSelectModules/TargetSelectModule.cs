using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Ai system/TargetSelectNode")]
public class TargetSelectModule : ScriptableObject
{

    public float distanceCost;

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
        return -Vector3Int.Distance(data.WorldObject.worldPosition, owner.WorldObject.worldPosition) * distanceCost;
    }
}
