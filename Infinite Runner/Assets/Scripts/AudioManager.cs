using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{

    public AudioSource audioSource;
    public float musicVolume;
    public Slider musicVolumeSlider;

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
            DontDestroyOnLoad(this);
        }

    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        musicVolume = musicVolumeSlider.value;
        audioSource.volume = musicVolume;
    }

    private void Update()
    {
        audioSource.volume = musicVolume;
        
        musicVolume = musicVolumeSlider.value;
        
    }

    public bool IsSpecificClipPlaying(AudioSource source, AudioClip clip)
    {
        // Return true if the source is playing and the clip matches
        return source.isPlaying && source.clip == clip;
    }
}