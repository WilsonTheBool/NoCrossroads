using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class AttackingCharacter : MonoBehaviour
{
    public TargetRule_data_SO[] targetRules;

    public float damage;

    public bool isRanged;
    public bool canRetaliate;

    public GameWorldMapManager GameWorldMapManager;

    public UnityEvent OnAttackStart;
    public UnityEvent OnAttackEnd;


    public bool CanTarget(KillableCharacter killableCharacter)
    {
        if (targetRules.Length == 0)
        {
            return false;
        }

        foreach (TargetRule_data_SO rule in targetRules)
        {
            if (!rule.CanAttack(killableCharacter))
            {
                return false;
            }
        }

        return true;
    }

    public void Attack(Vector3Int pos)
    {
        foreach (WorldObject worldObject in GameWorldMapManager.GetAllWorldObjectsOnPosition(pos))
        {
            if (worldObject.TryGetComponent<KillableCharacter>(out KillableCharacter killableCharacter))
            {
                OnAttackStart.Invoke();
                DealDamage(killableCharacter);
                OnAttackEnd.Invoke();
            }
        }
    }

    public void Attack(KillableCharacter killableCharacter)
    {
        OnAttackStart.Invoke();
        DealDamage(killableCharacter);
        OnAttackEnd.Invoke();
    } 

    private void DealDamage(KillableCharacter ch)
    {
        ch.TakeDamage(damage, this);
    }

    private void Start()
    {
        GameWorldMapManager = GameWorldMapManager.instance;
    }

    public void TryToRetaliate(KillableCharacter.DamageEventArgs args)
    {
        if (args.isCountered && !args.attacker.isRanged && canRetaliate)
        {
            var killable = args.attacker.GetComponent<KillableCharacter>();
            if(killable != null)
            {
                killable.TakeDamage_NoRetaliation(damage, this);
            }
        }
    }
}
