using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text bestScoreText;

    void Update()
    {
        scoreText.text = "Score:" + ScoreManager.currentScore;
        bestScoreText.text = "Best:" + ScoreManager.bestScore;
    }
}