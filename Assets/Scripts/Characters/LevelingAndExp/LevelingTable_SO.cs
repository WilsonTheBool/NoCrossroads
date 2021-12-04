using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameObjects/Leveling table data")]
public class LevelingTable_SO : ScriptableObject
{
    public LevelData[] LevelDatas;

    public int maxLevel;

    [Serializable]
    public struct LevelData
    {
        public int level;

        public int ExpToLevel;

        public int AttackIncreace;

        public int HPIncreace;
    }

    public LevelData GetLevelData(int newLevel)
    {
        foreach(LevelData levelData in LevelDatas)
        {
            if(levelData.level == newLevel)
            {
                return levelData;
            }
        }

        Debug.LogError("Cant find leveling data");
        return new LevelData();
    }
}

