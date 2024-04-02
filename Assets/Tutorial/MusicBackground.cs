using UnityEngine;

public class MusicBackground : SoundMenu
{
    [SerializeField] private AudioSource _music;
    
    private void Start()
    {
        _music.Play();
    }
    
    public override void ChangeVolume(float volume)
    {
        _music.volume = Mathf.Lerp(_minVolume, _maxVolume, volume);
    }
}
