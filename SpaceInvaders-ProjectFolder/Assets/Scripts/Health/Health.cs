using System;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField, Min(1)] int maxHealth = 100;
    public int MaxHealth { get => maxHealth; }
    public int CurrentHealth { get; private set; }

    public event Action<int, int> OnHealthUpdated;
    public event Action OnDamage;
    public event Action OnDie;

    void Start() => CurrentHealth = MaxHealth;

    public void InstantKill() => DealDamage(CurrentHealth);
    public virtual void DealDamage(int damageAmount)
    {
        int damage = (int)Mathf.Clamp(damageAmount, 0, damageAmount);
        CurrentHealth = Mathf.Clamp(CurrentHealth - damage, 0, maxHealth);
        HandleHealthUpdated();
        HandleOnDamage();
        
        if(CurrentHealth <= 0)
            HandleOnDie();
    }

    protected virtual void HandleHealthUpdated()    => OnHealthUpdated?.Invoke(CurrentHealth, maxHealth);
    protected virtual void HandleOnDamage()         => OnDamage?.Invoke();
    protected virtual void HandleOnDie()            => OnDie?.Invoke();

    
}
