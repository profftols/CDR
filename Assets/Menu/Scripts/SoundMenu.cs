using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundMenu : MonoBehaviour
{
    [SerializeField] protected Button _button;
    [SerializeField] protected AudioMixerGroup _master;
    [SerializeField] protected Slider _slider;
    
    protected const string SoundVolume = "SoundVolume";
    protected const string MusicVolume = "MusicVolume";
    private const string MasterVolume = "MasterVolume";
    
    protected float _minVolume = -80f;
    protected float _maxVolume = 0f;
    
    protected virtual void Start()
    {
        _button.onClick.AddListener(ToggleSound);
        _slider.onValueChanged.AddListener(ChangeVolume);
    }

    protected virtual void ChangeVolume(float volume)
    {
        _master.audioMixer.SetFloat(MasterVolume, Mathf.Lerp(_minVolume, _maxVolume, volume));
    }

    private void ToggleSound()
    {
        _master.audioMixer.GetFloat(MasterVolume, out var value);
        
        if (value == _maxVolume)
        {
            _master.audioMixer.SetFloat(MasterVolume, _minVolume);
        }
        else
        {
            _master.audioMixer.SetFloat(MasterVolume, _maxVolume);
        }
    }
}