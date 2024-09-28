using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] private Transform scorePointParent;
    [SerializeField] private Image scorePointImage;
    private Image[] scorePointsImage;
    private Sprite defaultSprite;
    private Sprite pointSprite;
    private int currentScore;

    public void SetData(int points, Sprite defaultSprite, Sprite pointSprite) 
    {
        this.defaultSprite = defaultSprite;
        this.pointSprite = pointSprite;

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

    

}
