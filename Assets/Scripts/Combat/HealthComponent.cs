using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class HealthComponent : MonoBehaviour, IDamageable, IHealable, IHealthProvider
{
    private float _currentHP;
    private float _lastMaxHP;
    private IDamageFilter _filter;
    private Stat _maxHPStat;
    private bool clampDirty;

    public float CurrentHP => _currentHP;
    public float MaxHP => _maxHPStat.Value;
    public bool IsAlive => _currentHP > 0;

    public event Action<DamageEventArgs> OnDamaged;
    public event Action<HealEventArgs> OnHealed;
    public event Action<float, float> OnMaxHealthChanged;
    public event Action<float, float> OnRevived;
    public event Action<DamageContext> OnDied;

    private void Awake()
    {
        _filter = GetComponent<IDamageFilter>();
        _maxHPStat = GetComponent<IHaveStats>().GetStat(StatType.Health);
    }
    private void Start()
    {
        _currentHP = MaxHP;
        _lastMaxHP = MaxHP;
    }
    private void OnEnable() => _maxHPStat.Changed += OnMaxChanged;
    private void OnDisable() => _maxHPStat.Changed -= OnMaxChanged;
    public void OnMaxChanged(Stat stat) => clampDirty = true;

    private void LateUpdate()
    {
        if (!clampDirty) return;
        clampDirty = false;

        if (_lastMaxHP < MaxHP)
        {
            _currentHP += MaxHP - _lastMaxHP;
        }

        _lastMaxHP = MaxHP;
        _currentHP = Mathf.Min(_currentHP, MaxHP);
        OnMaxHealthChanged?.Invoke(_currentHP, MaxHP);
    }
    public void TakeDamage(DamageContext context)
    {
        if (!IsAlive) return;
        if (_filter != null && !_filter.CanTakeDamage(context.Components[0])) return;

        float takenDamage = context.BaseAmount;
        if (context.Target.TryGetComponent<IHaveStats>(out IHaveStats targetStats))
        {
            //takenDamage = Mathf.Max(0, context.BaseAmount - targetStats.Stats[StatType.Defense]);
        }

        _currentHP = Mathf.Max(_currentHP - takenDamage, 0f);

        OnDamaged?.Invoke(new DamageEventArgs
        {
            CurrentHP = _currentHP,
            MaxHP = MaxHP
        });

        if (_currentHP <= 0f)
        {
            OnDied?.Invoke(context);
        }
    }

    public void Heal(HealContext context)
    {
        if (!IsAlive) return;

        _currentHP = Mathf.Min(_currentHP + context.BaseAmount, MaxHP);
        OnHealed?.Invoke(new HealEventArgs
        {
            CurrentHP = _currentHP,
            MaxHP = MaxHP
        });
    }

    public void Revival(float amount)
    {
        _currentHP = amount;
        OnRevived?.Invoke(_currentHP, MaxHP);
    }
}