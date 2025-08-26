using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Source")]
    //Create two children of Audio Manager with Audio Source component
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("Audio Clip")]
    public AudioClip[] bg;
    public AudioClip[] sfx;
    public AudioClip[] player;
    public AudioClip[] btns;

    [Header("Variables")]
    //Current BG track
    public int currentTrack;

    //SCENE MANAGER || SCENE SYSTEM script
    SceneSystem scene;

    private void Awake()
    {
        scene = FindAnyObjectByType<SceneSystem>();
    }

    private void Start()
    {
        if (scene.currentSceneIndex == 0)
        {
            currentTrack = 0;
        }

        else if (scene.currentSceneIndex >= 1)
        {
            currentTrack = 1;
        }

        playMusic();
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

    public void stopSFX()
    {
        sfxSource.Stop();
    }
}
