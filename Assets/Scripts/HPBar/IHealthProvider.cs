using System;
using UnityEngine;

public interface IHealthProvider
{
    float MaxHP { get; }
    float CurrentHP { get; }
    bool IsAlive { get; }
    public event Action<DamageEventArgs> OnDamaged;
    public event Action<HealEventArgs> OnHealed;
    public event Action<float, float> OnMaxHealthChanged;
    public event Action<float, float> OnRevived;
    public event Action<DamageContext> OnDied;

    void Restore(float value);
    void Reduce(float value);

    public void Revival(float amount);
}
