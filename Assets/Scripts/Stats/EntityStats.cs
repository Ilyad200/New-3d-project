using System.Collections.Generic;
using UnityEngine;

public class EntityStats : MonoBehaviour, IHaveStats
{
    [SerializeField] private Stats _stats;
    public Dictionary<StatType, float> Stats { get; private set; }

    private IHealthProvider _healthProvider;
    private Regenerate _regenerate;

    private void Awake()
    {
        _healthProvider = GetComponent<IHealthProvider>();
        _regenerate = GetComponent<Regenerate>();

        Stats = new Dictionary<StatType, float>() { 
            { StatType.Attack, _stats.Attack },
            { StatType.Defense, _stats.Defense },
            { StatType.Speed, _stats.Speed },
            { StatType.Health, _stats.HP },
            { StatType.AttackRange, _stats.AttackRange },
            { StatType.AttackSpeed, _stats.AttackSpeed },
            { StatType.Regeneration, _stats.Regeneration },
        };
    }
    void Start()
    {
        _healthProvider.MaxHP = Stats[StatType.Health];
        _healthProvider.Revival(Stats[StatType.Health]);
    }
    public void SetStatType(StatType type, float amount)
    {
        Stats[type] = amount;
        if (type == StatType.Health) _healthProvider.MaxHP = amount;
        if (type == StatType.Regeneration)
        {
            if (amount <= 0) _regenerate.StopRegen();
            else _regenerate.StartRegen();
        }

    }
}
