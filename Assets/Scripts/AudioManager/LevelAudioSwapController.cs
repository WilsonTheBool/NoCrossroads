using UnityEngine;
using System.Collections;

public class LevelAudioSwapController : MonoBehaviour
{
    public AudioClip musicSwapClip;
    public AudioClip backgroundSwapClip;

    private AudioManager AudioManager;
    private void Start()
    {
        AudioManager = AudioManager.instance;

        if(AudioManager != null)
        {
            if(musicSwapClip != null)
                AudioManager.Play_Audio_Music(musicSwapClip);

            if (backgroundSwapClip != null)
                AudioManager.Play_Audio_BackgroundSFX(backgroundSwapClip);
        }
    }
}
