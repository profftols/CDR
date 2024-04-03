using System;
using UnityEngine;
using UnityEngine.Audio;

public class SoundMenu : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup _master;

    private float _minVolume = -80f;
    private float _maxVolume = 0f;

    public void ChangeVolume(float volume)
    {
        _master.audioMixer.SetFloat("MasterVolume", Mathf.Lerp(_minVolume, _maxVolume, volume));
    }

    public void ChangeVolumeSound(float volume)
    {
        _master.audioMixer.SetFloat("SoundVolume", Mathf.Lerp(_minVolume, _maxVolume, volume));
    }

    public void ChangeVolumeMusic(float volume)
    {
        _master.audioMixer.SetFloat("MusicVolume", Mathf.Lerp(_minVolume, _maxVolume, volume));
    }

    public void ToggleSound()
    {
        _master.audioMixer.GetFloat("MasterVolume", out var value);
        
        if (value == _maxVolume)
        {
            _master.audioMixer.SetFloat("MasterVolume", _minVolume);
        }
        else
        {
            _master.audioMixer.SetFloat("MasterVolume", _maxVolume);
        }
    }
}