using System;
using System.Collections.Generic;
using UnityEngine;
public class GameWorld_ExpGainController : GameWorldMap_Dependable
{
    GameWorldMapManager GameWorldMapManager;

    public List<LevelingCharacter> levelingCharacters;

    public static GameWorld_ExpGainController instance;
    private void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }
    private void Awake()
    {
        if(instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }

        levelingCharacters = new List<LevelingCharacter>();
    }

    private void Start()
    {


        
    }

    public override void SetUp()
    {
        GameWorldMapManager = GameWorldMapManager.instance;
        GameWorldMapManager.OnUnitCreate += GameWorldMapManager_OnUnitCreate;
        GameWorldMapManager.OnUnitDeath += GameWorldMapManager_OnUnitDeath;
    }

    private void GameWorldMapManager_OnUnitDeath(object sender, GameWorldMapManager.UnitEventArgs e)
    {
        if (e.worldObject.TryGetComponent<LevelingCharacter>(out LevelingCharacter levelingCharacter))
        {
            levelingCharacters.Remove(levelingCharacter);
        }
    }

    private void GameWorldMapManager_OnUnitCreate(object sender, GameWorldMapManager.UnitEventArgs e)
    {
        if (e.worldObject.TryGetComponent<LevelingCharacter>(out LevelingCharacter levelingCharacter))
        {
            levelingCharacters.Add(levelingCharacter);
        }
    }

    public LevelingCharacter[] GetAllCharactersInRange(Vector3Int pos, int range)
    {
        List<LevelingCharacter> found = new List<LevelingCharacter>();
        foreach(LevelingCharacter levelingCharacter in levelingCharacters)
        {
            if(Vector3Int.Distance(levelingCharacter.WorldObject.worldPosition, pos) <= range)
            {
                found.Add(levelingCharacter);
            }
        }

        return found.ToArray();
    }
}

