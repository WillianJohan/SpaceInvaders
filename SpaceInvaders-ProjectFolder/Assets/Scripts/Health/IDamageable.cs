using System;

public interface IDamageable
{
    event Action OnDamage;
    void DealDamage(int damageAmount);
    void InstantKill();
}
