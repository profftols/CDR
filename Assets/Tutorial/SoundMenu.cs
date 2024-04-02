using System;
using UnityEngine;
using UnityEngine.Audio;

public class SoundMenu : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup _master;

    protected float _minVolume = -80f;
    protected float _maxVolume = 0f;
    
    private bool _enabled = true;

    public virtual void ChangeVolume(float volume)
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
        if (_enabled)
        {
            _master.audioMixer.SetFloat("MasterVolume", _minVolume);
            _enabled = false;
        }
        else
        {
            _master.audioMixer.SetFloat("MasterVolume", _maxVolume);
            _enabled = true;
        }
    }
}