using UnityEngine;
using System.Collections;

public class SaveLoad_AudioManger : MonoBehaviour
{
    [SerializeField]
    AudioManager AudioManager;

    public bool LoadFromPrefsOnStart;

    private void Awake()
    {
        if(AudioManager == null)
        {
            AudioManager = FindObjectOfType<AudioManager>();
        }

        if(LoadFromPrefsOnStart)
        LoadFromPrefs();
    }

    public void LoadFromPrefs()
    {
        float master = PlayerPrefs.GetFloat("master_volume", -1f);
        float music = PlayerPrefs.GetFloat("music_volume", -1f);
        float background = PlayerPrefs.GetFloat("background_volume", -1f);
        float sfx = PlayerPrefs.GetFloat("sfx_volume", -1f);

        if(master != -1)
        AudioManager.SetMasterVolume(master);

        if (music != -1)
            AudioManager.SetMusicVolume(music);

        if (background != -1)
            AudioManager.SetBackgroundolume(background);

        if (sfx != -1)
            AudioManager.SetSFXVolume(sfx);
    }

    public void SaveToPrefs()
    {
        PlayerPrefs.SetFloat("master_volume", AudioManager.masterAudioVolume);

        PlayerPrefs.SetFloat("music_volume", AudioManager.musicVolume);
        PlayerPrefs.SetFloat("background_volume", AudioManager.backgroundVolume);
        PlayerPrefs.SetFloat("sfx_volume", AudioManager.sfxVolume);

        PlayerPrefs.Save();
    }
}
