using UnityEngine;
using System.Collections;

public class KillableCharacter : MonoBehaviour
{
    Animator animator;

    public float maxHp;
    public float hp;

    public UnityEngine.Events.UnityEvent OnDeath;
    public UnityEngine.Events.UnityEvent OnTakeDamage;
    public UnityEngine.Events.UnityEvent OnHealSelf;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Diel()
    {
        if(animator != null)
        animator.SetTrigger("Death");

        OnDeath?.Invoke();
    }

    public void DestroyThis()
    {
        Destroy(this);
    }

    public void TakeDamage(float ammount)
    {
        hp -= ammount;

        if(hp <= 0)
        {
            Diel();
        }
        else
        {
            OnTakeDamage?.Invoke();

            if(animator != null)
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

            OnHealSelf?.Invoke();
        }
       
    }
}
