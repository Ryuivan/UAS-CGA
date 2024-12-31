using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] 
    private AudioClip music;

    [SerializeField] 
    private AudioClip[] sadMusic;

    [SerializeField] 
    private AudioClip[] happyMusic;

    public AudioSource audioSource;
    public AudioSource countdownAudioSource;
    public AudioSource sfxAudioSource; 
    private int sadMusicIndex;
    private int happyMusicIndex;

    // Awake dipanggil sebelum Start
    void Awake()
    {
        // Inisialisasi index dari PlayerPrefs
        sadMusicIndex = PlayerPrefs.GetInt("SadMusicIndex", 0);
        happyMusicIndex = PlayerPrefs.GetInt("HappyMusicIndex", 0);

        // Pastikan ada AudioSource di objek
        if (GetComponent<AudioSource>() == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = true; 
            audioSource.loop = true;
            audioSource.volume = 0.75f;
        }

        if (sfxAudioSource == null)
        {
            sfxAudioSource = gameObject.AddComponent<AudioSource>();
            sfxAudioSource.playOnAwake = false;
            sfxAudioSource.volume = 0.75f;
        }

        if (countdownAudioSource == null)
        {
            countdownAudioSource = gameObject.AddComponent<AudioSource>();
            countdownAudioSource.playOnAwake = false;
            countdownAudioSource.loop = true;
            countdownAudioSource.volume = 0.75f;
        }
    }

    // Start dipanggil setelah Awake
    void Start()
    {
        if (PlayerPrefs.GetInt("_Mute") == 0) PlayMusic(music);
        else audioSource.Stop();
    }

    public void PlaySadMusic()
    {
        if (sadMusic.Length > 0)
        {
            PlayOnce(sadMusic[sadMusicIndex]);
            sadMusicIndex = (sadMusicIndex + 1) % sadMusic.Length; 
            PlayerPrefs.SetInt("SadMusicIndex", sadMusicIndex);
        }
    }

    public void PlayHappyMusic()
    {
        if (happyMusic.Length > 0)
        {
            PlayOnce(happyMusic[happyMusicIndex]);
            happyMusicIndex = (happyMusicIndex + 1) % happyMusic.Length; 
            PlayerPrefs.SetInt("HappyMusicIndex", happyMusicIndex);
        }
    }

    private void PlayMusic(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.clip = clip; 
            audioSource.loop = true; 
            audioSource.Play();
        }
    }

    public void PlaySFXCountdown(AudioClip clip)
    {
        if (clip != null && countdownAudioSource != null && !countdownAudioSource.isPlaying)
        {
            Debug.Log("Playing countdown SFX");
            countdownAudioSource.clip = clip;
            countdownAudioSource.loop = true;
            countdownAudioSource.Play();
            Debug.Log("AudioClip name: " + clip.name);
        }
    }

    public void StopSFXCountdown()
    {
        if (countdownAudioSource != null)
        {
            countdownAudioSource.Stop();
        }
        
    }

    public void PlayOnce(AudioClip clip)
    {
        if (clip != null && sfxAudioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}
