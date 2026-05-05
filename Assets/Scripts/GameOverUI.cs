using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text bestScoreText;

    void Awake()
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

        Time.timeScale = 0f;
    }
}