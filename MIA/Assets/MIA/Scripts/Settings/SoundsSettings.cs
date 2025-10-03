using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundsSettings", menuName = "Settings/Sounds/New Sounds Settings")]
public class SoundsSettings : ScriptableObject
{
    public List<SoundSettings> Sounds;
}
