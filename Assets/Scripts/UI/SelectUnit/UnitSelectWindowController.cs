using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UnitSelectWindowController : MonoBehaviour
{
    public Image iconImage;

    public TMPro.TMP_Text hpText;

    public TMPro.TMP_Text attackText;

    public TMPro.TMP_Text moveText;

    public TMPro.TMP_Text nameText;

    public void SetUpWindow(SelectableObject unit)
    {
        iconImage.sprite = unit.data.unitIcon;

        nameText.text = unit.data.unitName;

        KillableCharacter killableCharacter = unit.GetComponent<KillableCharacter>();

        if(killableCharacter != null)
        {
            hpText.text = killableCharacter.hp.ToString() + "/" + killableCharacter.maxHp.ToString();
        }


    }

}
