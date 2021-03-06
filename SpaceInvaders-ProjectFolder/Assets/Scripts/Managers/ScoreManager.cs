using System;
using UnityEngine;

public class ScoreManager : Singleton<ScoreManager>
{
    public static int CurrentScore { get; private set; } = 0;

    public static event Action<int> OnScoreUpdated;

    protected override void Awake()
    {
        base.Awake();
        AlienHealthHandler.OnAlienDie += HandleAlienDie;
    }
    void OnDestroy()    => AlienHealthHandler.OnAlienDie -= HandleAlienDie;
    void Start()        => CurrentScore = 0;


    void AddScore(int value)
    {
        CurrentScore += value;
        OnScoreUpdated?.Invoke(CurrentScore);
    }

    void HandleAlienDie(AlienType alien)
    {
        if (alien == AlienType.Boss)
            AddScore(UnityEngine.Random.Range(40, 200));
        else
            AddScore((int)alien);
    }
}
