using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class Music_Player : MonoBehaviour
{
    [Tooltip("_audioSource defines the Audio Source component in this scene.")]
    AudioSource _audioSource;
    [Tooltip("_audioTracks defines the audio clips to be played continuously throughout the scene.")]
    public AudioClip[] _audioTracks;

    [Space(20)]
    [Header("Music Player Options")]
    [Tooltip("_playTracks acts as the Play/Stop function of the Music Player")]
    public bool _playTracks;
    [Tooltip("Skips to the next available _audioTracks clip.")]
    public bool _nextTrack;
    [Tooltip("Skips to the previous _audioTracks clip")]
    public bool _prevTrack;
    [Tooltip("Loops the current _audioTracks clip.")]
    public bool _loopTrack;

    [Space(20)]
    [Header("Debugging/ReadOnly")]
    [Tooltip("_playTracks is a ReadOnly variable that displays the current _audioTracks clip that is playing")]
    public int _playingTrack;
    [Tooltip("_isMute returns the status of muting function in the Sound_Controller.")]
    public bool _isMute = false;

    [Header("Mute Button Settings")]
    public Button muteButton; // Referensi ke tombol mute
    public Sprite iconSound;  // Ikon untuk audio aktif
    public Sprite iconMute;   // Ikon untuk audio mute
    public Image muteButtonChildImage; // Referensi ke Image child tombol mute

    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = _audioTracks[0];
        _playingTrack = 0;

        // Load mute status dari PlayerPrefs
        if (PlayerPrefs.HasKey("_Mute"))
        {
            int _value = PlayerPrefs.GetInt("_Mute");
            _isMute = _value == 1;
        }
        else
        {
            _isMute = false;
            PlayerPrefs.SetInt("_Mute", 0);
        }

        // Set status awal audio
        _audioSource.mute = _isMute;
        if (!_isMute) _audioSource.Play();

        UpdateButtonIcon();
    }

    void Update()
    {
        if (!_playTracks) _audioSource.Stop();
        if (_playTracks && !_audioSource.isPlaying) StartPlayer();
        _audioSource.loop = _loopTrack;

        if (_nextTrack) NextTrack();
        if (_prevTrack) PreviousTrack();
    }

    public void ToggleMute()
    {
        _isMute = !_isMute;
        _audioSource.mute = _isMute;

        // Simpan status mute di PlayerPrefs
        PlayerPrefs.SetInt("_Mute", _isMute ? 1 : 0);

        // Update ikon tombol
        UpdateButtonIcon();
    }

    private void UpdateButtonIcon()
    {
        if (muteButtonChildImage != null)
        {
            muteButtonChildImage.sprite = _isMute ? iconMute : iconSound;
        }
    }

    public void StartPlayer()
    {
        if (!_loopTrack) // Jika Audio Source tidak loop, putar track berikutnya
        {
            NextTrack();
        }
        else // Jika loop, putar ulang track yang sama
        {
            _audioSource.Play();
        }
    }

    public void NextTrack()
    {
        _nextTrack = false;
        _audioSource.Stop();

        int _newCount = _playingTrack + 1; // Cari track berikutnya
        if (_newCount > _audioTracks.Length - 1)
        {
            // Jika di akhir daftar, kembali ke awal
            _audioSource.clip = _audioTracks[0];
            _playingTrack = 0;
        }
        else
        {
            _audioSource.clip = _audioTracks[_newCount];
            _playingTrack = _newCount;
        }

        _audioSource.Play();
    }

    public void PreviousTrack()
    {
        _prevTrack = false;
        _audioSource.Stop();

        int _newCount = _playingTrack - 1; // Cari track sebelumnya
        if (_newCount < 0)
        {
            // Jika di awal daftar, kembali ke akhir
            _audioSource.clip = _audioTracks[_audioTracks.Length - 1];
            _playingTrack = _audioTracks.Length - 1;
        }
        else
        {
            _audioSource.clip = _audioTracks[_newCount];
            _playingTrack = _newCount;
        }

        _audioSource.Play();
    }
}
