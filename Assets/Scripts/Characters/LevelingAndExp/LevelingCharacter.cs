using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class LevelingCharacter : MonoBehaviour
{

    public LevelingTable_SO levelingTable;

    public UpgradableCharacter upgradableCharacter;

    public WorldObject WorldObject;

    public int curentLevel;
    public int maxLevel;

    public float curentExp;
    public float newLevelExp;

    public UnityEvent<int> OnExpGain;
    public UnityEvent<int> OnNewLevel;

    public bool canLevelUp = false;

    private void Start()
    {
        var data = levelingTable.GetLevelData(curentLevel + 1);
        maxLevel = levelingTable.maxLevel;
        newLevelExp = data.ExpToLevel;

    }
    public void GainExp(float value)
    {
        curentExp += value;

        OnExpGain.Invoke(Mathf.RoundToInt(value));

        while(curentExp >= newLevelExp)
        {
            if (curentExp >= newLevelExp)
            {
                OnNewLevelTreshholdReached();



                OnNewLevel.Invoke(curentLevel);
            }
        }



    }

    private void TryUpgrade(LevelingTable_SO.LevelData levelData)
    {
        if(levelData.AttackIncreace > 0)
        {
            if(TryGetComponent<HealingCharacter>(out HealingCharacter healingCharacter))
            {
                upgradableCharacter.UpgradeHeal(levelData.AttackIncreace);
            }
            else
            {
                upgradableCharacter.UpgradeAttack(levelData.AttackIncreace);
            }
                
        }

        if (levelData.HPIncreace  > 0)
        {
            upgradableCharacter.UpgradeHealth(levelData.HPIncreace);
        }

        
    }

    private void OnNewLevelTreshholdReached()
    {
        if(curentLevel >= maxLevel)
        {
            curentExp = 0;
            newLevelExp = 99999;
        }
        else
        {
            curentLevel++;
            var data = levelingTable.GetLevelData(curentLevel);
            var data_up = levelingTable.GetLevelData(curentLevel + 1);
            TryUpgrade(data);
            newLevelExp = data_up.ExpToLevel;
        }
    }



}
