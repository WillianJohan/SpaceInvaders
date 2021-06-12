using UnityEngine;

public static class DataManager
{
    public static readonly string HighScoreKey = "HighScoreKey";

    public static void SaveScore(int score)
    {
        if (score < LoadScore())
            return;

        PlayerPrefs.SetInt(HighScoreKey, score);
    }

    public static int LoadScore() => PlayerPrefs.GetInt(HighScoreKey, 0);

}
