using System;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class PlayerHealthHandler : MonoBehaviour
{
    [SerializeField] Health health;

    public static event Action OnPlayerDie;
    public static event Action OnPlayerSpawn;

    void Awake()        => health.OnDie += HandleDie;
    void OnDestroy()    => health.OnDie -= HandleDie;
    void Start()        => OnPlayerSpawn?.Invoke();

    void HandleDie()
    {
        OnPlayerDie?.Invoke();
        Destroy(gameObject);
    }

}
