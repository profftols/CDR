using UnityEngine;

public class MusicBackground : SoundMenu
{
    [SerializeField] private AudioSource _music;
    
    protected override void Start()
    {
        _slider.onValueChanged.AddListener(ChangeVolume);
    }
    
    protected override void ChangeVolume(float volume)
    {
        _master.audioMixer.SetFloat(MusicVolume, Mathf.Lerp(_minVolume, _maxVolume, volume));
    }
}
