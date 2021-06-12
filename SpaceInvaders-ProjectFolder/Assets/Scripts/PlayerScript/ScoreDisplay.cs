using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] TextMesh scoreText;

    void Awake()        => ScoreManager.OnScoreUpdated += UpdateScoreMesh;
    void OnDestroy()    => ScoreManager.OnScoreUpdated -= UpdateScoreMesh;

    void Start()
    {
        scoreText.text = ScoreManager.CurrentScore.ToString();
    }

    void UpdateScoreMesh(int score)
    {
        scoreText.text = score.ToString();
    }

}
