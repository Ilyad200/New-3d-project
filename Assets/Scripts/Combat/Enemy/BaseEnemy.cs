using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class BaseEnemy : MonoBehaviour
{
    protected NavMeshAgent _agent;
    [SerializeField] protected string _deathSoundName;
    protected bool _canAttack = true;

    protected IHealthProvider _healthProvider;
    protected IHaveStats _stats;

    protected Spawner _spawner;

    protected virtual void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _healthProvider = GetComponent<IHealthProvider>();
        _stats = GetComponent<IHaveStats>();
    }
    public virtual void Revive(Spawner spawner)
    {
        _spawner = spawner;

        _agent.speed = _stats.GetStatValue(StatType.Speed);
        _healthProvider.Revival(_stats.GetStatValue(StatType.Health));

        _canAttack = true;
    }
}
