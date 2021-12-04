using UnityEngine;
using System.Collections;

public class UpgradableCharacter : MonoBehaviour
{
    public WorldObject WorldObject;

    public AttackingCharacter AttackingCharacter;
    public KillableCharacter KillableCharacter;
    public MovingCharacter MovingCharacter;

    public TerritoryCreator TerritoryCreator;

    public Miner_Structure Structure;

    GameWorldMapManager GameWorldMapManager;

    private void Start()
    {
        GameWorldMapManager = GameWorldMapManager.instance;
    }

    public void UpgradeAttack(int value)
    {
        if(AttackingCharacter != null)
        {
            AttackingCharacter.damage += value;
        }
    }

    public void UpgradeHealth(int value)
    {
        if (KillableCharacter != null)
        {
            KillableCharacter.maxHp += value;
            KillableCharacter.HealSelf(value);
        }
    }

    public void UpgradeMovePoints(int value)
    {
        if(MovingCharacter != null)
        {
            MovingCharacter.maxMovePoints += value;
            MovingCharacter.movePoints += value;
        }
    }

    public void UpgradeTerritoryRange(int value)
    {
        if (TerritoryCreator != null)
        {
            TerritoryCreator.createRadius += value;

            if(gameObject.activeSelf)
            TerritoryCreator.UpdateTerritory(WorldObject.worldPosition, GameWorldMapManager);
        }
    }

    public void UpgradeRrsourceGarthering(float value)
    {
        if (Structure != null)
        {
            Structure.MultWorkEffectiveness(value);
        }
    }
}
