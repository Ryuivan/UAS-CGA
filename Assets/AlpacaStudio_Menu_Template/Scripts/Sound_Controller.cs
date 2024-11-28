using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class Sound_Controller : MonoBehaviour
{
    private AudioSource _audioSource;
    private bool _isMute = false;

    [Header("UI Elements")]
    public Button soundToggleButton; // Tombol untuk toggle suara
    public Sprite iconSound; // Ikon untuk unmute
    public Sprite iconMute; // Ikon untuk mute

    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();

        // Cek PlayerPrefs untuk status mute
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

        // Set audio sesuai status
        _audioSource.mute = _isMute;
        UpdateButtonIcon();
    }

    public void ToggleSound()
    {
        _isMute = !_isMute; // Toggle status mute
        _audioSource.mute = _isMute;

        // Simpan status mute di PlayerPrefs
        PlayerPrefs.SetInt("_Mute", _isMute ? 1 : 0);

        // Update ikon tombol
        UpdateButtonIcon();
    }

    private void UpdateButtonIcon()
    {
        if (soundToggleButton != null)
        {
            // Ubah ikon tombol berdasarkan status mute
            soundToggleButton.image.sprite = _isMute ? iconMute : iconSound;
        }
    }
}
