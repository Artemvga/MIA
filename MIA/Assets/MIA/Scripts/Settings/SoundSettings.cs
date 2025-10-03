using UnityEngine;

[CreateAssetMenu(fileName = "SoundSettings", menuName = "Settings/Sounds/New Sound Settings")]
public class SoundSettings : ScriptableObject
{
    public AudioClip Sound;

    [Range(0, 1)]
    public float Volume;

    public string Name;
}
