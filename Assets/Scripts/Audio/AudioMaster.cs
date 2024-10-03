using UnityEngine;
using UnityEngine.UI;

public class AudioMaster : MonoBehaviour
{
    public static AudioMaster Instance;

    [SerializeField] private Image[] btnsImage;
    [SerializeField] private Sprite audioOnSprite;
    [SerializeField] private Sprite audioOffSprite;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioFile pressBtnAudio;

    private string audioPlayState;

    #region [- Behaviours -]
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
    #endregion

    #region [- Unity -]
    private void Awake()
    {
        Instance = this;
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
    #endregion
}
