using UnityEngine;
using System.Collections;

public class DropExpOnDeath : MonoBehaviour
{
    public WorldObject WorldObject;

    public int expDropAmmount;

    public int expDropRange;

    GameWorld_ExpGainController GameWorld_ExpGainController;

    public KillableCharacter KillableCharacter;

    private void Start()
    {
        GameWorld_ExpGainController = GameWorld_ExpGainController.instance;
        KillableCharacter.OnDeath.AddListener(DropExp);
    }

    private void DropExp()
    {
        LevelingCharacter[] levelingCharacters = GameWorld_ExpGainController.GetAllCharactersInRange(WorldObject.worldPosition, expDropRange);

        if (levelingCharacters.Length > 0)
        {
            float expToEach = expDropAmmount / levelingCharacters.Length;

            foreach (LevelingCharacter levelingCharacter in levelingCharacters)
            {
                levelingCharacter.GainExp(expToEach);
            }

        }
       
    }
}
