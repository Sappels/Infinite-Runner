using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public AudioSource audioSource;

    public static AudioManager Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public bool IsSpecificClipPlaying(AudioSource source, AudioClip clip)
    {
        // Return true if the source is playing and the clip matches
        return source.isPlaying && source.clip == clip;
    }
}