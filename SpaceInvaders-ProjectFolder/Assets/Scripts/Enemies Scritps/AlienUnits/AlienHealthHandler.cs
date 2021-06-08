using System;
using UnityEngine;

[RequireComponent(typeof(Alien))]
[RequireComponent(typeof(Health))]
public class AlienHealthHandler : MonoBehaviour
{
    
    [SerializeField] Health health;

    public static event Action<AlienType> OnAlienDie;

    void Awake() => health.OnDie += HandleDie;
    void OnDestroy() => health.OnDie -= HandleDie;


    void HandleDie()
    {
        OnAlienDie?.Invoke(GetComponent<Alien>().Type);
        Destroy(gameObject);
    }

}
