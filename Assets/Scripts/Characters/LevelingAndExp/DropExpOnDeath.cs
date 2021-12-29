using UnityEngine;
using System.Collections;

public class DropExpOnDeath : MonoBehaviour
{
    public WorldObject WorldObject;

    public int expDropAmmount;

    public int expDropRange;

    GameWorld_ExpGainController GameWorld_ExpGainController;

    public KillableCharacter KillableCharacter;

    public float expMultPerCharacter = 0.15f;

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
            float expToEach = expDropAmmount * (1 + expMultPerCharacter * (levelingCharacters.Length - 1)) / levelingCharacters.Length;

            foreach (LevelingCharacter levelingCharacter in levelingCharacters)
            {
                levelingCharacter.GainExp(expToEach);
            }

        }
       
    }
}
