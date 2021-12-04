using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Audio manager/Audio data")]
public class AudioData : ScriptableObject
{
    public AudioHolder[] audioHolders;

    public bool TryGetAudioClipByName(string name, out AudioClip clip)
    {
        foreach(AudioHolder holder in audioHolders)
        {
            if(holder.name == name)
            {
                clip = holder.AudioClip;
                return true;
            }
        }

        clip = null;
        return false;
    }

    [System.Serializable]
    public class AudioHolder
    {
        public AudioClip AudioClip;
        public string name;
    }
}
