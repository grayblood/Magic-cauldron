using System.Collections.Generic;
using UnityEngine;

public static class Audio_Controller
{
    
    public enum Sounds
    {
        C_Explosion,
        C_Created,
        Unlocked,
    }
    private static Dictionary<Sounds, float> soundTimerDictionary;


    public static void Initialize()
    {
        soundTimerDictionary = new Dictionary<Sounds, float>();
        soundTimerDictionary[Sounds.C_Explosion] = 0f;
    }
    public static void PlaySound(Sounds sound)
    {
        if (CanPlaySound(sound)) { 
        GameObject soundGameObject = new GameObject("Sound");
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
            audioSource.PlayOneShot(GetAudioClip(sound));
        }

    }

    private static bool CanPlaySound(Sounds sound)
    {
        switch (sound)
        {
            default:
                return true;
            case Sounds.C_Explosion:
                if (soundTimerDictionary.ContainsKey(sound))
                {
                    float lastTimePlayed = soundTimerDictionary[sound];
                    float playerExplosionTimerMax = .05f;
                    if (lastTimePlayed + playerExplosionTimerMax < Time.time)
                    {
                        soundTimerDictionary[sound] = Time.time;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return true;
                }
                //break;
        }
    }

    private static AudioClip GetAudioClip(Sounds sound)
    {
        foreach (SoundCollectionScript.SoundAudioClip soundAudioClip in SoundCollectionScript.i.audioClipArray)
        {
            if (soundAudioClip.sound == sound)
            {
                return soundAudioClip.audioClip;
            }
        }
        Debug.LogError("Sound " + sound + " not found!");
        return null;
    }
}
