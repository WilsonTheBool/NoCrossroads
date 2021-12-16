using UnityEngine;
using System.Collections;

public class AutoSetStatsOnLevel : MonoBehaviour
{
    public int Level;

    [SerializeField]
    KillableCharacter killableCharacter;

    [SerializeField]
    AttackingCharacter AttackingCharacter;

    [SerializeField]
    LevelingCharacter LevelingCharacter;

    [SerializeField]
    DropExpOnDeath DropExpOnDeath;

    [SerializeField]
    LevelingTable_SO LevelingTable_SO;

    private void Awake()
    {
        var data = LevelingTable_SO.GetLevelData(Level);

        if(killableCharacter != null)
        {
            killableCharacter.maxHp = data.HPIncreace;
            killableCharacter.hp = data.HPIncreace; 
        }

        if(AttackingCharacter != null)
        {
            AttackingCharacter.damage = data.AttackIncreace;
        }

        if(LevelingCharacter != null)
        {
            LevelingCharacter.curentLevel = Level;
        }

        if(DropExpOnDeath != null)
        {
            DropExpOnDeath.expDropAmmount = data.ExpToLevel;
        }
    }
}
