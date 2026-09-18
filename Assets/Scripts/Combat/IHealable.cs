using UnityEngine;

public interface IHealable
{
    void Heal(HealContext context);

    float MaxHP { get; }
    float CurrentHP { get; }
}
