using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class BaseEnemy : MonoBehaviour, IHaveStats
{
    protected NavMeshAgent _agent;
    [SerializeField] protected EnemyStats _stats;
    [SerializeField] protected string _deathSoundName;
    protected Dictionary<StatType, float> _statsDict;
    public Dictionary<StatType, float> Stats { get => _statsDict; }
    protected bool _canAttack = true;

    protected IHealthProvider _healthProvider;

    protected Spawner _spawner;

    protected virtual void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _healthProvider = GetComponent<IHealthProvider>();

        _statsDict = new Dictionary<StatType, float>() {
            { StatType.Attack, _stats.Attack },
            { StatType.Defense, _stats.Defense },
            { StatType.Speed, _stats.Speed },
            { StatType.Health, _stats.HP },
            { StatType.AttackRange, _stats.AttackRange },
            { StatType.AttackSpeed, _stats.AttackSpeed },
            { StatType.DetectionRange, _stats.DetectionRange },
        };
    }
    public virtual void Revive(Spawner spawner)
    {
        _spawner = spawner;
        _agent.speed = Stats[StatType.Speed];
        _healthProvider.MaxHP = Stats[StatType.Health];
        _healthProvider.Revival(Stats[StatType.Health]);

        _canAttack = true;
    }

    public void SetStatType(StatType type, float amount)
    {
        _statsDict[type] = amount;
    }
}
