public static class ScoreManager
{
    public static int currentScore = 0;
    public static int bestScore = 0;

    public static void ResetScore()
    {
        currentScore = 0;
    }

    public static void AddScore(int amount)
    {
        currentScore += amount;

        if (currentScore > bestScore)
        {
            bestScore = currentScore;
        }
    }
}