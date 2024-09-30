using UnityEngine;

[CreateAssetMenu(fileName = "New Audio file", menuName = "Audio/Audio File", order = 1)]
public class AudioFile : ScriptableObject
{
    [field: SerializeField] public AudioClip Clip { get; private set; }
    [field: SerializeField] public float Volume { get; private set; }
    [field: SerializeField] public float Pitch { get; private set; }
}
