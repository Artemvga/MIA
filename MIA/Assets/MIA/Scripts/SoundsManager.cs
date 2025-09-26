using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    [field:SerializeField] public SoundsSettings _soundsSettings { get; private set; }
    [SerializeField] private AudioSource _soundsAudioSource;

    public void PlaySound(string name)
    {
        var sound = _soundsSettings.Sounds.Find(s => s.Name == name);
        if (sound == null)
        {
            throw new System.Exception("No Sound found with name: " + name);
        }

        _soundsAudioSource.PlayOneShot(sound.Sound, sound.Volume);
    }
}
