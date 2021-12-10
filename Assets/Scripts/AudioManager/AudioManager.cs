using UnityEngine;
using System.Collections.Generic;


public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource MusicChanel;

    public AudioSource BackgroundSFXChanel;

    public AudioSource[] effectChanels;

    public AudioData audioData;

    public UnityEngine.Events.UnityEvent SettingsChanged;

    public float sameAudioClipPLayDelay = 0.1f;

    public float masterAudioVolume;
    public float musicVolume = 1;
    public float backgroundVolume = 1;
    public float sfxVolume = 1;

    public void Play_Audio_Music(string name)
    {
        if(audioData.TryGetAudioClipByName(name, out AudioClip clip))
        {
            MusicChanel.PlayOneShot(clip);
        }
    }

    public void Play_Audio_BackgroundSFX(string name)
    {
        if (audioData.TryGetAudioClipByName(name, out AudioClip clip))
        {
            BackgroundSFXChanel.PlayOneShot(clip);
        }
    }

    public void Play_Audio_Effect(string name)
    {
        if (CanPlayClip(name))
            if (audioData.TryGetAudioClipByName(name, out AudioClip clip))
            {
                if (TryGetFreeChanel(effectChanels, out AudioSource free))
                {
                    SetLastPlayer(name);
                    free.PlayOneShot(clip);
                }
                else
                {
                    Debug.LogError("Cant Find Free Chanel");
                }
            }
    }

    private bool CanPlayClip(string clipName)
    {
        if (GetTimeLastPlayed(clipName) >= sameAudioClipPLayDelay)
        {
            return true;
        }

        return false;
    }

   

    private bool TryGetFreeChanel(AudioSource[] audioSources, out AudioSource freeChanel)
    {
        foreach(AudioSource source in audioSources)
        {
            if (!source.isPlaying)
            {
                freeChanel = source;
                return true;
            }
        }

        freeChanel = null;
        return false;
    }

    private void SetVolume()
    {
        MusicChanel.volume = Mathf.Clamp01(masterAudioVolume * musicVolume);
        BackgroundSFXChanel.volume = Mathf.Clamp01(masterAudioVolume * backgroundVolume);

        foreach(AudioSource audioSource in effectChanels)
        {
            audioSource.volume = Mathf.Clamp01(masterAudioVolume * sfxVolume);
        }
    }

    public void SetMasterVolume(System.Single value)
    {
        masterAudioVolume = value;
        SetVolume();
    }

    public void SetSFXVolume(System.Single value)
    {
        sfxVolume = value;
        SetVolume();
    }

    public void SetMusicVolume(System.Single value)
    {
        musicVolume = value;
        SetVolume();
    }


    public void SetBackgroundolume(System.Single value)
    {
        backgroundVolume = value;
        SetVolume();
    }
    private void Awake()
    {
        if(instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            SetVolume();
        }
    }

    private void Update()
    {
        float t = Time.deltaTime;

        foreach(AudioClipData data in audioClipDatas)
        {
            data.timeLastPlayed += t;
        }
    }

    private void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }

    List<AudioClipData> audioClipDatas = new List<AudioClipData>();

    float GetTimeLastPlayed(string clipName)
    {
        foreach(AudioClipData data in audioClipDatas)
        {
            if(data.clipName == clipName)
            {
                return data.timeLastPlayed;
            }
        }

        return float.MaxValue;
    }

    void SetLastPlayer(string clipName)
    {
        foreach (AudioClipData data in audioClipDatas)
        {
            if (data.clipName == clipName)
            {
                data.timeLastPlayed = 0;
                return;
            }
        }

        audioClipDatas.Add(new AudioClipData() { clipName = clipName, timeLastPlayed = 0 });
    }



    private class AudioClipData
    {
        public string clipName;
        public float timeLastPlayed;


    }
}
