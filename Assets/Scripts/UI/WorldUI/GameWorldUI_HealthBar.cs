using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameWorldUI_HealthBar : MonoBehaviour
{
    [SerializeField]
    Image healthBarImage;

    private KillableCharacter KillableCharacter;

    private LevelingCharacter LevelingCharacter;

    [SerializeField]
    float colorChangePercent;

    [SerializeField]
    Color greenColor;

    [SerializeField]
    Color redColor;

    [SerializeField]
    GameWorldUI_MatchPosition matchPosition;

    [SerializeField]
    TMPro.TMP_Text levelText;

    [SerializeField]
    GameObject levelHolder;

    public void SetUp(KillableCharacter killable)
    {
        killable.OnTakeDamage.AddListener(UpdateBar);
        killable.OnHealSelf.AddListener(UpdateBar);
        killable.OnDeath.AddListener(DeleteBar);

        this.KillableCharacter = killable;

        matchPosition.SetOwner(killable.transform);

        if(killable.TryGetComponent<LevelingCharacter>(out LevelingCharacter))
        {
            LevelingCharacter.OnNewLevel.AddListener(UpdateLevel);

            levelHolder.gameObject.SetActive(true);

            levelText.text = LevelingCharacter.curentLevel.ToString();
        }
        else
        {
            levelHolder.SetActive(false);
        }
    }

    private void DeleteBar()
    {
        Destroy(this.gameObject);
    }

    public void UpdateLevel(int level)
    {
        if (LevelingCharacter != null)
        {
            levelText.text = level.ToString();
        }
    }

    public void UpdateBar(KillableCharacter.DamageEventArgs damage)
    {
        if(KillableCharacter != null)
        {
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
