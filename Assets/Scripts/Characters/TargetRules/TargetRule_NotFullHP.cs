using UnityEngine;
using UnityEditor;

[CreateAssetMenu(menuName = "GameObjects/Targer rules/Not full hp")]
public class TargetRule_NotFullHP : TargetRule_data_SO
{
    public override bool CanAttack(KillableCharacter killableCharacter)
    {
        if (killableCharacter != null && killableCharacter.hp < killableCharacter.maxHp)
        {
            return true;
        }

        return false;
    }
}