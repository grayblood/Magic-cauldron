using System;
using UnityEngine;

public class SoundCollectionScript : MonoBehaviour
{
    public static SoundCollectionScript i;
    private void Awake()
    {
        if (i != null && i != this)
        {
            Destroy(this);
        }
        else
        {
            i = this;
        }
    }
    
    public SoundAudioClip[] audioClipArray;
    [System.Serializable]
    public class SoundAudioClip
    {
        public Audio_Controller.Sounds sound;
        public AudioClip audioClip;
    }
}
