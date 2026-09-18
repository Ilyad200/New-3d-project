using UnityEngine;

public interface IDamageable
{
    void TakeDamage(DamageContext context);

    float MaxHP { get; }
    float CurrentHP { get; }
}
