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

    public bool GetMovePosition(Vector3Int attackPos, Vector3Int[] movingArea, out Vector3Int bestPos)
    {

        float minDistace = float.MaxValue;
        bestPos = new Vector3Int(-1, -1, -1);

        foreach (Vector3Int vec in movingArea)
        {
            float distance = Vector3Int.Distance(vec, attackPos);
            if (distance <= 1)
            {
                bestPos = vec;
                return true;
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


        return false;
    }
}
