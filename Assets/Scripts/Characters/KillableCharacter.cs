using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class KillableCharacter : MonoBehaviour
{
    Animator animator;

    public float maxHp;
    public float hp;

    public UnityEvent OnDeath;
    public DamageEvent OnTakeDamage;
    public DamageEvent OnHealSelf;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Die()
    {
        if(animator != null)
        animator.SetTrigger("Death");

        OnDeath?.Invoke();
        
    }

    public void HealToFull()
    {
        OnHealSelf.Invoke(new DamageEventArgs() { damage = maxHp - hp });
        hp = maxHp;
    }

    public void TakeDamage(float ammount, AttackingCharacter attacker)
    {
        hp -= ammount;

        if(hp <= 0)
        {
            OnTakeDamage?.Invoke(new DamageEventArgs() { damage = ammount, isCountered = true, attacker = attacker});
            Die();
        }
        else
        {
            OnTakeDamage?.Invoke(new DamageEventArgs() {damage = ammount, isCountered = true, attacker = attacker });

            if(animator != null)
            {
                animator.SetTrigger("Hit");
            }
        }

    }

    public void TakeDamage_NoRetaliation(float ammount, AttackingCharacter attacker)
    {
        hp -= ammount;

        if (hp <= 0)
        {
            OnTakeDamage?.Invoke(new DamageEventArgs() { damage = ammount, isCountered = false, attacker = attacker });
            Die();
        }
        else
        {
            OnTakeDamage?.Invoke(new DamageEventArgs() { damage = ammount, isCountered = false, attacker = attacker });

            if (animator != null)
            {
                animator.SetTrigger("Hit");
            }
        }
    }

    public void HealSelf(float ammount)
    {
        if(hp != maxHp && ammount > 0)
        {
            hp += ammount;

            if (hp > maxHp)
            {
                hp = maxHp;
            }

            OnHealSelf?.Invoke(new DamageEventArgs() { damage = ammount });
        }
       
    }

    [System.Serializable]
    public class DamageEvent: UnityEvent<DamageEventArgs>
    {

    }

    [System.Serializable]
    public class DamageEventArgs
    {
        public float damage;

        public bool isCountered;

        public AttackingCharacter attacker;
    }
}
