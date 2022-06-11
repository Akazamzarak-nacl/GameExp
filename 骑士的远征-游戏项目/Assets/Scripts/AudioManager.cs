using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Audio;
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    public void AudioPlay(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
        
    }

}
