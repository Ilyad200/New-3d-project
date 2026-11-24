using UnityEngine;

public interface IHealable
{
    void Heal(HealContext context);
    
    IHealthProvider HealthProvider { get; }
}
