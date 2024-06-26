using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager singleton;

    private AudioSource source;
    public AudioClip jumpSFX;
    public AudioClip attackSFX;

    // Start is called before the first frame update
    void Start()
    {
        singleton = this;

        source = GetComponent<AudioSource>();
    }

    public void PlaySFX(AudioClip sound, float volume = .5f)
    {
        source.PlayOneShot(sound, volume);
    }
}