using UnityEngine;

public class Sound : SoundMenu
{
    [SerializeField] private AudioSource _sound;

    public void Play()
    {
        _sound.Play();
    }
    
    public override void ChangeVolume(float volume)
    {
        _sound.volume = Mathf.Lerp(_minVolume, _maxVolume, volume);
    }
}
