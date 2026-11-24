using System;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour, IHealthProvider
{
    [SerializeField] private float _maxHP = 50f;
    private float _currentHP;

    public event Action<float, float> OnHealthChanged; //currentHP, maxHP

    public float CurrentHP => _currentHP;
    public float MaxHP { 
        get => _maxHP;
        set {
            if (value <= 0) return;
            _maxHP = value;
            if (_currentHP > _maxHP) _currentHP = _maxHP;
            OnMaxHealthChanged?.Invoke(_currentHP, _maxHP);
        } }
    public bool IsAlive => _currentHP > 0;

    public event Action<DamageEventArgs> OnDamaged;
    public event Action<HealEventArgs> OnHealed;
    public event Action<float, float> OnMaxHealthChanged;
    public event Action<float, float> OnRevived;
    public event Action<DamageContext> OnDied;

    private void Awake()
    {
        _currentHP = _maxHP;
    }

    public void Restore(float value)
    {
        _currentHP = Math.Min(_maxHP, _currentHP + value);
    }
    public void Reduce(float value)
    {
        _currentHP = Math.Max(0, _currentHP - value);
    }

    public void TakeDamage(DamageContext context)
    {
        if (!IsAlive) return;
        //if (_filter != null && !_filter.CanTakeDamage(context)) return;

        float takenDamage = 0;
        Debug.Log($"[HealthComponent] TakeDamage");
        foreach (var component in context.Components)
        {
            takenDamage += component.Amount;
        }
        Debug.Log($"[HealthComponent] {takenDamage}");
        if (context.Target.TryGetComponent<IHaveStats>(out IHaveStats targetStats))
        {
            takenDamage = Mathf.Max(0, takenDamage - targetStats.Get(StatType.Resistance));
        }

        _currentHP = Mathf.Max(_currentHP - takenDamage, 0f);

        OnDamaged?.Invoke(new DamageEventArgs
        {
            CurrentHP = _currentHP,
            MaxHP = _maxHP
        });

        if (_currentHP <= 0f)
        {
            OnDied?.Invoke(context);
        }
    }

    public void Heal(HealContext context)
    {
        if (!IsAlive) return;

        _currentHP = Mathf.Min(_currentHP + context.BaseAmount, _maxHP);
        OnHealed?.Invoke(new HealEventArgs
        {
            CurrentHP = _currentHP,
            MaxHP = _maxHP
        });
    }

    public void Revival(float amount)
    {
        _currentHP = amount;
        OnRevived?.Invoke(_currentHP, _maxHP);
    }
}