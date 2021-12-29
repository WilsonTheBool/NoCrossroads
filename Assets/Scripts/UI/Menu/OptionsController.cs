using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OptionsController : MonoBehaviour
{
    [SerializeField]
    AudioManager AudioManager;

    [SerializeField]
    Slider master_slider;

    [SerializeField]
    Slider music_slider;

    [SerializeField]
    Slider background_slider;

    [SerializeField]
    Slider sfx_slider;

    [SerializeField]
    Toggle fullscreen;

    [SerializeField]
    Text verText;

    [SerializeField]
    GameObject TutorialWindow;

    private void Start()
    {

        AudioManager = AudioManager.instance;

        if(verText != null)
        verText.text = "v." + Application.version;

        master_slider.value = AudioManager.masterAudioVolume;

        music_slider.value = AudioManager.musicVolume;
        background_slider.value = AudioManager.backgroundVolume;
        sfx_slider.value = AudioManager.sfxVolume;

        if(bool.TryParse(PlayerPrefs.GetString("fullscreen"), out bool result))
        {
            fullscreen.isOn = result;
            
        }
        else
        {
            fullscreen.isOn = false;
        }

        if(TutorialWindow != null)
        if (PlayerPrefs.HasKey("isFirstTime"))
        {
            TutorialWindow.SetActive(false);
        }

        Screen.fullScreen = fullscreen.isOn;

        fullscreen.onValueChanged.AddListener(OnFullScreeenValueChanged);
    }

    public void OnFullScreeenValueChanged(bool value)
    {
        Screen.fullScreen = fullscreen.isOn;
    }

    public void TutorialWindow_IsSeen()
    {
        PlayerPrefs.SetInt("isFirstTime", 1);
    }

    public void SaveToPrefs()
    {
        FindObjectOfType<SaveLoad_AudioManger>().SaveToPrefs();

        PlayerPrefs.SetString("fullscreen", fullscreen.isOn.ToString());

    }

    public void SetAudio_Music(float value)
    {
        AudioManager.SetMusicVolume(value);
    }

    public void SetAudio_SFX(float value)
    {
        AudioManager.SetSFXVolume(value);
    }

    public void SetAudio_Master(float value)
    {
        AudioManager.SetMasterVolume(value);
    }

    public void SetAudio_Background(float value)
    {
        AudioManager.SetBackgroundolume(value);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
