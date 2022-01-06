using UnityEngine;
using System.Collections;

public class AdsInvoker : MonoBehaviour
{
    public float AdsDelaySeconds;

    private AdsTimeController AdsTimeController;

    private void Start()
    {
        AdsTimeController = AdsTimeController.instance;

        TryShowAds();
    }

    private void TryShowAds()
    {
        if(AdsTimeController.adsTimerInSeconds >= AdsDelaySeconds)
        {
            AdsTimeController.OnShowAds();
            ShowAds();
            print("Ads Shown");
        }
    }

    public void ShowAds()
    {
        CrazyGames.CrazyAds.Instance.beginAdBreak();
    }
}
