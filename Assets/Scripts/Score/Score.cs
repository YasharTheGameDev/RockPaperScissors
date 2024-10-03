using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] private Transform scorePointParent;
    [SerializeField] private Image scorePointImage;
    [SerializeField] private Image userIcon;

    private Image[] scorePointsImage;
    private Sprite defaultSprite;
    private Sprite pointSprite;
    private int currentScore;

    #region [- Behaviours -]
    public void SetData(int points, Sprite defaultSprite, Sprite pointSprite, Sprite userSprite)
    {
        this.defaultSprite = defaultSprite;
        this.pointSprite = pointSprite;

        userIcon.sprite = userSprite;

        scorePointsImage = new Image[points];

        scorePointsImage[0] = scorePointImage;
        scorePointsImage[0].sprite = this.defaultSprite;
        for (int i = 1; i < points; i++)
        {
            Image pointScore = Instantiate(scorePointImage, scorePointParent);
            scorePointsImage[i] = pointScore;
            scorePointsImage[i].sprite = this.defaultSprite;
        }
    }
    public void AddScore()
    {
        scorePointsImage[currentScore].sprite = pointSprite;
        currentScore++;
    }
    public bool Win()
    {
        return currentScore == scorePointsImage.Length;
    }
    public void OnReset()
    {
        currentScore = 0;
        if (scorePointsImage != null && scorePointsImage.Length > 0)
        {
            for (int i = 1; i < scorePointsImage.Length; i++)
            {
                Destroy(scorePointsImage[i]);
            }
            scorePointsImage = null;
        }
    } 
    #endregion

}
