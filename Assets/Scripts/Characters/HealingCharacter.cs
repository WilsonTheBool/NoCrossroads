using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class HealingCharacter : MonoBehaviour
{

    public int healPower;

    public TargetRule_data_SO[] targetRules;

    [HideInInspector]
    public GameWorldMapManager GameWorldMapManager;

    public UnityEvent OnHealStart;
    public UnityEvent OnHealEnd;

    private void Start()
    {
        GameWorldMapManager = GameWorldMapManager.instance;
    }

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

    public void Heal(KillableCharacter killableCharacter)
    {
        OnHealStart.Invoke();
        killableCharacter.HealSelf(healPower);
        OnHealEnd.Invoke();
    }

    public void Heal(Vector3Int pos)
    {
        foreach (WorldObject worldObject in GameWorldMapManager.GetAllWorldObjectsOnPosition(pos))
        {
            if (worldObject.TryGetComponent<KillableCharacter>(out KillableCharacter killableCharacter))
            {
                OnHealStart.Invoke();
                killableCharacter.HealSelf(healPower);
                OnHealEnd.Invoke();
            }
        }
    }
}
