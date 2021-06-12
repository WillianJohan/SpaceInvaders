using System;
using UnityEngine;

[RequireComponent(typeof(Alien))]
[RequireComponent(typeof(Health))]
public class AlienHealthHandler : MonoBehaviour
{
    
    [SerializeField] Health health;
    AlienType type;

    public static event Action<AlienType> OnAlienDie;
    public static event Action<AlienType> OnAlienSpawn;

    void Awake()        => health.OnDie += HandleDie;
    void OnDestroy()    => health.OnDie -= HandleDie;

    void Start()
    {
        type = GetComponent<Alien>().Type;
        OnAlienSpawn?.Invoke(type);
    }

    void HandleDie()
    {
        OnAlienDie?.Invoke(type);
        Destroy(gameObject);
    }

}
