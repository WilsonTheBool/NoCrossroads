using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UnitSelectWindowController : MonoBehaviour
{
    public Image iconImage;

    public TMPro.TMP_Text hpText;

    public TMPro.TMP_Text attackText;
    public TMPro.TMP_Text HealText;

    public TMPro.TMP_Text nameText;

    public TMPro.TMP_Text expText;
    public TMPro.TMP_Text lvlText;



    public void SetUpWindow(SelectableObject unit)
    {
        iconImage.sprite = unit.data.unitIcon;

        nameText.text = unit.data.unitName;

        KillableCharacter killableCharacter = unit.GetComponent<KillableCharacter>();

        if(killableCharacter != null)
        {
            hpText.text = killableCharacter.hp.ToString() + "/" + killableCharacter.maxHp.ToString();
        }
        else
        {
            hpText.text = "0";
        }

        AttackingCharacter attackingCharacter = unit.GetComponent<AttackingCharacter>();
        HealingCharacter healing = unit.GetComponent<HealingCharacter>();

        if (attackingCharacter != null)
        {
            attackText.transform.parent.gameObject.SetActive(true);
            HealText.transform.parent.gameObject.SetActive(false);
            attackText.text = attackingCharacter.damage.ToString();
        }
        else
        {
            if(healing != null)
            {
                attackText.transform.parent.gameObject.SetActive(false);
                HealText.transform.parent.gameObject.SetActive(true);
                HealText.text = healing.healPower.ToString();
            }
            else
            {
                attackText.transform.parent.gameObject.SetActive(false);
                HealText.transform.parent.gameObject.SetActive(false);
            }
        }

        LevelingCharacter levelingCharacter = unit.GetComponent<LevelingCharacter>();

        if(levelingCharacter != null)
        {
            expText.text = "exp: " + Mathf.RoundToInt(levelingCharacter.curentExp).ToString() + "/" + Mathf.RoundToInt(levelingCharacter.newLevelExp).ToString();
            lvlText.text = "lvl " + levelingCharacter.curentLevel;
        }
        else
        {
            expText.text = "";
            lvlText.text = "";
        }
    }

}
