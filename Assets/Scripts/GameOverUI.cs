using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text bestScoreText;

    void Start()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);

        if (scoreText != null)
        {
            scoreText.text = "Score: " + ScoreManager.currentScore;
        }

        if (bestScoreText != null)
        {
            bestScoreText.text = "Best: " + ScoreManager.bestScore;
        }
    }
}