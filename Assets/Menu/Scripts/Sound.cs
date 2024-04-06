using UnityEngine;

public class Sound : SoundMenu
{
    [SerializeField] private AudioSource _sound;
    
    protected override void Start()
    {
        _button.onClick.AddListener(PlaySound);
        _slider.onValueChanged.AddListener(ChangeVolume);
    }
    
    protected override void ChangeVolume(float volume)
    {
        _master.audioMixer.SetFloat(SoundVolume, Mathf.Lerp(_minVolume, _maxVolume, volume));
    }
    
    private void PlaySound()
    {
        _sound.Play();
    }
}
