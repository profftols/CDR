using UnityEngine;

public class Sound : SoundMenu
{
    [SerializeField] private AudioSource _sound;

    public void Play()
    {
        _sound.Play();
    }
}
