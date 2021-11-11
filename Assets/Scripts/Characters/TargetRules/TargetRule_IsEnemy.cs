using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "GameObjects/Targer rules/Is Enemy")]
public class TargetRule_IsEnemy : TargetRule_data_SO
{
    public override bool CanAttack(KillableCharacter killableCharacter)
    {
        if(killableCharacter != null)
        {
            var ch = killableCharacter.GetComponent<SelectableObject>();
            if (ch != null && !ch.isPlayerSide)
            {
                return true;
            }
        }

        return false;
    }
}
