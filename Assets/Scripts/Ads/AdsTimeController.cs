using UnityEngine;
using System.Collections;

public class AdsTimeController : MonoBehaviour
{
    public static AdsTimeController instance;

    public float adsTimerInSeconds;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
            SetTimer();
        }
        else
        {
            Destroy(this);
        }
    }

    private void OnDestroy()
    {
        if(instance == this)
        {
            PlayerPrefs.SetFloat("AdsTimer", adsTimerInSeconds);
            PlayerPrefs.Save();
        }
    }

    private void SetTimer()
    {
        if (PlayerPrefs.HasKey("AdsTimer"))
            adsTimerInSeconds = PlayerPrefs.GetFloat("AdsTimer");
        else
            adsTimerInSeconds = 0;
    }

    

    private void FixedUpdate()
    {
        adsTimerInSeconds += Time.fixedDeltaTime;
        
    }

    public void OnShowAds()
    {
        adsTimerInSeconds = 0;
    }

}
