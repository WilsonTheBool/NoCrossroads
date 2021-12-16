using UnityEngine;
using System.Collections;

public class CharacterUIController : MonoBehaviour
{
    [SerializeField]
    GameWorldUIController GameWorldUIController;

    [SerializeField]
    GameWorldUI_HealthBar HpBar_prefab;

    [SerializeField]
    TMPro.TMP_Text damageText_prefab;

    [SerializeField]
    TMPro.TMP_Text exp_text_prefab;

    [SerializeField]
    TMPro.TMP_Text levelUpText;


    [SerializeField]
    TMPro.TMP_Text heal_text_prefab;

    [SerializeField]
    KillableCharacter owner;

    GameWorldUI_HealthBar healthBar;

    private void Start()
    {
       
    }

    public void DeleteHealthBar()
    {
        if (healthBar != null)
        Destroy(healthBar.gameObject);

        healthBar = null;
    }

    public void Spawn_HPBar()
    {
        healthBar = Instantiate(HpBar_prefab, this.transform.position, Quaternion.Euler(0, 0, 0), GameWorldUIController.GetWorldCanvas().transform);
        healthBar.SetUp(owner);
    }

    public GameWorldUI_ResourceBar GameWorldUI_ResourceBar;

    public void Spawn_resourceBar()
    {
         Instantiate(GameWorldUI_ResourceBar, this.transform.position, Quaternion.Euler(0, 0, 0),
             GameWorldUIController.GetWorldCanvas().transform).SetUp(owner.GetComponent<Miner_Structure>(), owner);
    }

    public void Spawn_DamageText(KillableCharacter.DamageEventArgs args)
    {
        Instantiate(damageText_prefab, this.transform.position, Quaternion.Euler(0, 0, 0), GameWorldUIController.GetWorldCanvas().transform).text = args.damage.ToString();
    }

    public void Spawn_ExpText(int value)
    {
        Instantiate(exp_text_prefab, this.transform.position, Quaternion.Euler(0, 0, 0), GameWorldUIController.GetWorldCanvas().transform).text = value.ToString() + " exp";
    }

    public void Spawn_HealText(KillableCharacter.DamageEventArgs args)
    {
        Instantiate(heal_text_prefab, this.transform.position, Quaternion.Euler(0, 0, 0), GameWorldUIController.GetWorldCanvas().transform).text = args.damage.ToString();
    }

    public void Spawn_LevelUp()
    {
        Instantiate(levelUpText, this.transform.position, Quaternion.Euler(0, 0, 0), GameWorldUIController.GetWorldCanvas().transform);
    }
}
