using UnityEngine;

public interface IDamageable
{
    void TakeDamage(DamageContext context);

    IHealthProvider HealthProvider { get; }
}
