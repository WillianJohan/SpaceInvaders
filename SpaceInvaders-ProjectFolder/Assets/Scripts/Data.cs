using UnityEngine;

public static class Data
{
    public static readonly string HighScoreKey = "HighScoreKey";

    public static void Save(int score)
    {
        if (score < Load())
            return;

        PlayerPrefs.SetInt(HighScoreKey, score);
    }

    public static int Load() => PlayerPrefs.GetInt(HighScoreKey, 0);

}
