using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameWorldUI_ResourceBar : MonoBehaviour
{
    [SerializeField]
    Image healthBarImage;

    private Miner_Structure miner;

    [SerializeField]
    float colorChangePercent;

    [SerializeField]
    Color greenColor;

    [SerializeField]
    Color redColor;

    [SerializeField]
    GameWorldUI_MatchPosition matchPosition;

    public void SetUp(Miner_Structure miner, KillableCharacter killableCharacter)
    {
        //miner.OnMine.AddListener(UpdateBar);
       
        miner.OnResourceTileEmpty.AddListener(DeleteBar);
        killableCharacter.OnDeath.AddListener(DeleteBar);

        this.miner = miner;

        matchPosition.SetOwner(miner.transform);
    }

    private void DeleteBar()
    {
        Destroy(this.gameObject);
    }

    private void FixedUpdate()
    {
        UpdateBar();
    }
    public void UpdateBar()
    {

        healthBarImage.fillAmount = miner.resourceTile.curentResourceCount / miner.resourceTile.maxResourceAmmount;
        healthBarImage.fillAmount = Mathf.Clamp01(healthBarImage.fillAmount);

        if (healthBarImage.fillAmount > colorChangePercent)
        {
            healthBarImage.color = greenColor;
        }
        else
        {
            healthBarImage.color = redColor;
        }



    }
}
