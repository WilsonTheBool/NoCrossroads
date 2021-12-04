using UnityEngine;
using System.Collections;

public class CharacterAudioController : MonoBehaviour
{

    AudioManager AudioManager;

    public string audio_name_onHit;
    public string audio_name_onDeath;
    public string audio_name_onMove;

    private void Start()
    {
        AudioManager = AudioManager.instance;
    }

    public void PlayAudio(string name)
    {
        AudioManager.Play_Audio_Effect(name);
    }

    public void PlayAudio_Hit()
    {
        PlayAudio(audio_name_onHit);
    }

    public void PlayAudio_Death()
    {
        PlayAudio(audio_name_onDeath);
    }

    public void PlayAudio_Move()
    {
        PlayAudio(audio_name_onMove);
    }

}
