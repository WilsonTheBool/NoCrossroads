using UnityEngine;
using System.Collections;

public class TargetRule_data_SO : ScriptableObject
{
    public virtual bool CanAttack(KillableCharacter killableCharacter)
    {
        return false;
    }
}
