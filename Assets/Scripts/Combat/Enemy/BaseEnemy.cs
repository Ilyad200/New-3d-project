using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class BaseEnemy : MonoBehaviour, ICombatSource
{
    protected NavMeshAgent _agent;
    [SerializeField] protected string _deathSoundName;

    protected bool _canAttack = true;

    public IHealthProvider HealthProvider { get; protected set; }
    public IHaveStats Stats { get; protected set; }
    public Faction Faction { get; } = Faction.Enemy;

    protected Spawner _spawner;

    protected virtual void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        HealthProvider = GetComponent<IHealthProvider>();

        Stats = GetComponent<IHaveStats>();
    }
    public virtual void Revive(Spawner spawner)
    {
        _spawner = spawner;
        _agent.speed = Stats.Stats[StatType.Speed];
        HealthProvider.MaxHP = Stats.Stats[StatType.Health];
        HealthProvider.Revival(Stats.Stats[StatType.Health]);

        _canAttack = true;
    }

    public void SetStatType(StatType type, float amount)
    {
        Stats.SetStatType(type, amount);
    }
}
