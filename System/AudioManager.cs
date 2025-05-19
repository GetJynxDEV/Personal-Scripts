using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("Audio Clip")]
    public AudioClip[] bg;
    public AudioClip[] sfx;
    public AudioClip[] btns;

    [Header("Variables")]
    //Current BG track
    public int currentTrack;

    //Music Fade Out Duration
    [SerializeField] private float fadeDuration = 2f; 

    public static AudioManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void playMusic()
    {
        //This will play the Background Track

        musicSource.clip = bg[currentTrack];
        musicSource.Play();
    }

    public void playSFX(AudioClip clip)
    {
        //This will play any SFX sound

        sfxSource.PlayOneShot(clip);
    }

    public void playBTN(AudioClip clip)
    {
        //This will play any Btn sound
        sfxSource.PlayOneShot(clip);
    }
}
