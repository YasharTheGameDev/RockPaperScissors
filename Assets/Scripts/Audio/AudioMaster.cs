using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioMaster : MonoBehaviour
{
    public static AudioMaster Instance;
    private void Awake()
    {
        Instance = this;
    }

    [SerializeField] private Image[] btnsImage;
    [SerializeField] private Sprite audioOnSprite;
    [SerializeField] private Sprite audioOffSprite;
    private bool audioPlayState = true;
    public void OnPressPlayStateBtn() 
    {
        audioPlayState = !audioPlayState;
        foreach (var item in btnsImage) 
        {
            item.sprite = audioPlayState ? audioOnSprite : audioOffSprite;
        }
    }

    [SerializeField] private AudioSource audioSource;

    [SerializeField] private AudioFile audioFile;
    public void PressBtn() 
    {
        audioSource.volume = audioFile.Volume;
        audioSource.pitch = audioFile.Pitch;
        audioSource.PlayOneShot(audioFile.Clip);
    }
}
