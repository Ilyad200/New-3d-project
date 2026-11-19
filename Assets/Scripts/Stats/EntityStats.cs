using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    public StatType Type;
    public float Value;

    public Stat(StatType type, float value = 0f)
    {
        Type = type;
        Value = value;
    }
}

public class EntityStats : MonoBehaviour, IHaveStats
{
    [SerializeField] private Stats _stats;
    private Dictionary<StatType, float> _statsDictionary = new();
    public List<DefenceModifier> Resistances { get; private set; }
    public List<PenetrationModifier> Penetrations { get; private set; }


    private IHealthProvider _healthProvider;
    private Regenerate _regenerate;

    private void Awake()
    {
        _healthProvider = GetComponent<IHealthProvider>();
        _regenerate = GetComponent<Regenerate>();

        //Stats = new Dictionary<StatType, float>() { 
        //    { StatType.Attack, _stats.Attack },
        //    { StatType.Defense, _stats.Defense },
        //    { StatType.Speed, _stats.Speed },
        //    { StatType.Health, _stats.HP },
        //    { StatType.AttackRange, _stats.AttackRange },
        //    { StatType.AttackSpeed, _stats.AttackSpeed },
        //    { StatType.Regeneration, _stats.Regeneration },
        //};
        foreach (var s in _stats.StatList)
        {
            _statsDictionary.Add(s.Type, s.Value);
        }
        Resistances = _stats.Resistances;
        Penetrations = _stats.Penetrations;

    }
    void Start()
    {
        _healthProvider.MaxHP = _statsDictionary[StatType.Health];
        _healthProvider.Revival(_statsDictionary[StatType.Health]);
    }
    public float Get(StatType type)
    {
        return _statsDictionary.TryGetValue(type, out var value) ? value : 0f;
    }
    public void SetStatType(StatType type, float amount)
    {
        _statsDictionary[type] = amount;
        if (type == StatType.Health) _healthProvider.MaxHP = amount;
        if (type == StatType.Regeneration)
        {
            if (amount <= 0) _regenerate.StopRegen();
            else _regenerate.StartRegen();
        }

    }
}
