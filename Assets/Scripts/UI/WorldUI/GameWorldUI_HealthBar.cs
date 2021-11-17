using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameWorldUI_HealthBar : MonoBehaviour
{
    [SerializeField]
    Image healthBarImage;

    private KillableCharacter KillableCharacter;

    [SerializeField]
    float colorChangePercent;

    [SerializeField]
    Color greenColor;

    [SerializeField]
    Color redColor;

    [SerializeField]
    GameWorldUI_MatchPosition matchPosition;

    public void SetUp(KillableCharacter killable)
    {
        killable.OnTakeDamage.AddListener(UpdateBar);
        killable.OnHealSelf.AddListener(UpdateBar);

        this.KillableCharacter = killable;

        matchPosition.SetOwner(killable.transform);
    }

    

    public void UpdateBar(KillableCharacter.DamageEventArgs damage)
    {
        if(KillableCharacter != null)
        {
            if(KillableCharacter.hp == KillableCharacter.maxHp)
            {
                healthBarImage.enabled = false;
            }
            else
            {
                healthBarImage.enabled = true;
            }

            healthBarImage.fillAmount = KillableCharacter.hp / KillableCharacter.maxHp;
            healthBarImage.fillAmount = Mathf.Clamp01(healthBarImage.fillAmount);

            if(healthBarImage.fillAmount > colorChangePercent)
            {
                healthBarImage.color = greenColor;
            }
            else
            {
                healthBarImage.color = redColor;
            }
        }

    }
}
