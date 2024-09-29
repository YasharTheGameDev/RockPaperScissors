using UnityEngine;

[System.Serializable]
public class AudioFile
{
    [field: SerializeField] public AudioClip Clip { get; private set; }
    [field: SerializeField] public float Volume { get; private set; }
    [field: SerializeField] public float Pitch { get; private set; }
}
