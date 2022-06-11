using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class AudioVolumeManager : MonoBehaviour
{
    public AudioMixer audioMixer;
    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("MusicVolume", volume);
    }
    public void SetEffectVolume(float volume)
    {
        audioMixer.SetFloat("EffectVolume", volume);
    }
}
