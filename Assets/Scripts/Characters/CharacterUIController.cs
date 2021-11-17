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
    KillableCharacter owner;

    GameWorldUI_HealthBar healthBar;

    private void Start()
    {
        Spawn_HPBar();
    }

    public void DeleteHealthBar()
    {
        if (healthBar != null)
        Destroy(healthBar.gameObject);
    }

    public void Spawn_HPBar()
    {
        healthBar = Instantiate(HpBar_prefab, this.transform.position, Quaternion.Euler(0,0,0), GameWorldUIController.GetWorldCanvas().transform);
        healthBar.SetUp(owner);
    }

    public void Spawn_DamageText(KillableCharacter.DamageEventArgs args)
    {
        Instantiate(damageText_prefab, this.transform.position, Quaternion.Euler(0, 0, 0), GameWorldUIController.GetWorldCanvas().transform).text = args.damage.ToString();
    }
}
