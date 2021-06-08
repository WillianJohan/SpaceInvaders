using UnityEngine;

public class ScoreManager : Singleton<ScoreManager>
{
    public int CurrentScore { get; private set; } = 0;

    void Awake()
    {
        AlienHealthHandler.OnAlienDie += HandleAlienDie;
    }

    void OnDestroy()
    {
        AlienHealthHandler.OnAlienDie -= HandleAlienDie;
    }

    void AddScore(int value)
    {
        CurrentScore += value;
    }

    void HandleAlienDie(AlienType alien)
    {
        if (alien == AlienType.Boss)
            AddScore(Random.Range(40, 200));
        else
            AddScore((int)alien);
    }

}
