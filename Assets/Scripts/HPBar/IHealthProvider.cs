using System;
using UnityEngine;

public interface IHealthProvider
{
    float MaxHP { get; set; }
    float CurrentHP { get; }
    bool IsAlive { get; }
    public event Action<DamageEventArgs> OnDamaged;
    public event Action<HealEventArgs> OnHealed;
    public event Action<float, float> OnMaxHealthChanged;
    public event Action<float, float> OnRevived;
    public event Action OnDied;

    public void Revival(float amount);
}
