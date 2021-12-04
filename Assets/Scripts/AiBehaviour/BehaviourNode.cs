using UnityEngine;
using System.Collections;

public class BehaviourNode : ScriptableObject
{
    public float actionTimeDelay;

    public virtual bool CanActivate(AiBehaviourController controller, AiAgent owner)
    {
        return false;
    }

    public virtual void Activate(AiBehaviourController controller, AiAgent owner)
    {

    }

    public virtual bool GetMovePosition(Vector3Int attackPos, Vector3Int[]visionArea, MovingCharacter data, out Vector3Int bestPos)
    {
        bool isInRange = false;
        float minDistace = float.MaxValue;
        bestPos = new Vector3Int(-1, -1, -1);

        foreach (Vector3Int vec in visionArea)
        {
            float distance = Vector3Int.Distance(vec, attackPos);
            if (distance <= 1)
            {
                bestPos = vec;
                if (data.UnitMovementData.GetPathCost(bestPos) < data.movePoints)
                    isInRange =  true;

                break;
            }
            else
            {
                if (distance < minDistace)
                {
                    minDistace = distance;
                    bestPos = vec;
                }
            }
        }

        bestPos = data.UnitMovementData.GetPathMaxRange(bestPos, data.movePoints);
        
        
        return isInRange;
    }

    public bool GetMovePosition_Move(Vector3Int attackPos, Vector3Int[] visionArea, MovingCharacter data, out Vector3Int bestPos)
    {
        bool isInRange = false;
        float minDistace = float.MaxValue;
        bestPos = new Vector3Int(-1, -1, -1);

        foreach (Vector3Int vec in visionArea)
        {
            float distance = Vector3Int.Distance(vec, attackPos);
            if (distance < 1)
            {
                bestPos = vec;
                if (data.UnitMovementData.GetPathCost(bestPos) <= data.movePoints)
                    isInRange = true;

                break;
            }
            else
            {
                if (distance < minDistace)
                {
                    minDistace = distance;
                    bestPos = vec;
                }
            }
        }

        bestPos = data.UnitMovementData.GetPathMaxRange(bestPos, data.movePoints);


        return isInRange;
    }
}
