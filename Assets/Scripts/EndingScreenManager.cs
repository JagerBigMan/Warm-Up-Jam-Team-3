using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingScreen : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text bestScoreText;

    void Start()
    {
        scoreText.text = "Score: " + ScoreManager.currentScore;
        bestScoreText.text = "Best: " + ScoreManager.bestScore;
    }

    public void Replay()
    {
        ScoreManager.ResetScore();
        SceneManager.LoadScene("Game");
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }
}