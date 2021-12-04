using UnityEngine;
using System.Collections;

public class UnitUpgradeController : MonoBehaviour
{
    [SerializeField]
    string unitTypeName;

    PrefabSpawner PrefabSpawner;

    GameWorldMapManager GameWorldMapManager;

    private void Start()
    {
        PrefabSpawner = PrefabSpawner.instance;
        GameWorldMapManager = GameWorldMapManager.instance;
    }

    public void Upgrade_Health(int value)
    {
        WorldObject prefab = PrefabSpawner.GetPrefabByTypeName(unitTypeName);
        prefab.GetComponent<UpgradableCharacter>().UpgradeHealth(value);

        foreach(WorldObject world in GameWorldMapManager.GetAllObjectsByTypeName(unitTypeName))
        {
            world.GetComponent<UpgradableCharacter>().UpgradeHealth(value);
        }
    }

    public void Upgrade_Attack(int value)
    {
        WorldObject prefab = PrefabSpawner.GetPrefabByTypeName(unitTypeName);
        prefab.GetComponent<UpgradableCharacter>().UpgradeAttack(value);

        foreach (WorldObject world in GameWorldMapManager.GetAllObjectsByTypeName(unitTypeName))
        {
            world.GetComponent<UpgradableCharacter>().UpgradeAttack(value);
        }
    }

    public void Upgrade_TerritoryRange(int value)
    {
        WorldObject prefab = PrefabSpawner.GetPrefabByTypeName(unitTypeName);
        prefab.GetComponent<UpgradableCharacter>().UpgradeTerritoryRange(value);

        foreach (WorldObject world in GameWorldMapManager.GetAllObjectsByTypeName(unitTypeName))
        {
            world.GetComponent<UpgradableCharacter>().UpgradeTerritoryRange(value);
        }
    }

    public void Upgrade_ResourceGarthering(float value)
    {
        WorldObject prefab = PrefabSpawner.GetPrefabByTypeName(unitTypeName);
        prefab.GetComponent<UpgradableCharacter>().UpgradeRrsourceGarthering(value);

        foreach (WorldObject world in GameWorldMapManager.GetAllObjectsByTypeName(unitTypeName))
        {
            world.GetComponent<UpgradableCharacter>().UpgradeRrsourceGarthering(value);
        }
    }
}
