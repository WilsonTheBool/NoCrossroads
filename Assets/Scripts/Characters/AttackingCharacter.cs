using UnityEngine;
using System.Collections;

public class AttackingCharacter : MonoBehaviour
{
    public TargetRule_data_SO[] targetRules;

    public float damage;

    public bool CanTarget(KillableCharacter killableCharacter)
    {
        if(targetRules.Length == 0)
        {
            return false;
        }

        foreach(TargetRule_data_SO rule in targetRules)
        {
            if (!rule.CanAttack(killableCharacter))
            {
                return false;
            }
        }

        return true;
    }

    public void DealDamage(KillableCharacter ch)
    {
        ch.TakeDamage(damage);
    }

}
