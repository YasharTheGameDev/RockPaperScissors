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
    private string audioPlayState;
    public void OnAudioMute() 
    {
        audioPlayState = audioPlayState == "On" ? "Off" : "On";
        foreach (var item in btnsImage) 
        {
            item.sprite = audioPlayState == "On" ? audioOnSprite : audioOffSprite;
        }
        PlayerPrefs.SetString("_Audio", audioPlayState);
        PlayerPrefs.Save();
    }

    [SerializeField] private AudioSource audioSource;

    [SerializeField] private AudioFile pressBtnAudio;
    public void PressBtn() 
    {
        PlayAudio(pressBtnAudio);
    }

    public void PlayAudio(AudioFile audioFile) 
    {
        if (audioPlayState == "On")
        {
            audioSource.volume = audioFile.Volume;
            audioSource.pitch = audioFile.Pitch;
            audioSource.PlayOneShot(audioFile.Clip);
        }
    }

    private void Start()
    {
        audioPlayState = PlayerPrefs.GetString("_Audio");
        if (string.IsNullOrEmpty(audioPlayState))
        {
            audioPlayState = "On";
            PlayerPrefs.SetString("_Audio", audioPlayState);
            PlayerPrefs.Save();
        }

        foreach (var item in btnsImage)
        {
            item.sprite = audioPlayState == "On" ? audioOnSprite : audioOffSprite;
        }
    }
}
