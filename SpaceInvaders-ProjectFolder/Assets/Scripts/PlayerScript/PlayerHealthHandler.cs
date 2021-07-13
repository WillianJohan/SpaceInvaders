using System;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class PlayerHealthHandler : MonoBehaviour
{
    [SerializeField] Health health;

    public static event Action OnPlayerDie;
    public static event Action OnPlayerHit;
    public static event Action OnPlayerSpawn;


    [Header("UI Components")]
    [SerializeField] TextMesh lifeCountValue;

    void Awake() 
    { 
        health.OnDie += HandleDie;
        health.OnDamage += HandleOnDamage;
    }
    void OnDestroy() 
    {
        health.OnDie -= HandleDie;
        health.OnDamage -= HandleOnDamage;
    }
    void Start()
    { 
        OnPlayerSpawn?.Invoke();
        lifeCountValue.text = health.CurrentHealth.ToString();
    }



    void HandleOnDamage()
    {
        OnPlayerHit?.Invoke();
        lifeCountValue.text = health.CurrentHealth.ToString();
    }
    void HandleDie()
    {
        OnPlayerDie?.Invoke();
        Destroy(gameObject);
    }

}
