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

    private void Start()
    {
        verText.text = "v. " + Application.version;

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

        Screen.fullScreen = fullscreen.isOn;

        fullscreen.onValueChanged.AddListener(OnFullScreeenValueChanged);
    }

    public void OnFullScreeenValueChanged(bool value)
    {
        Screen.fullScreen = fullscreen.isOn;
    }

    public void SaveToPrefs()
    {
        PlayerPrefs.SetString("fullscreen", fullscreen.isOn.ToString());
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
