using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState
{
    Patrol,
    Wait,
    Chase,
    Attack
}

public class PatrolEnemy : BaseEnemy
{
    private Transform _target;
    [SerializeField] private float _patrolRadius = 10f;
    [SerializeField] private float _threshold = 1f;
    [SerializeField] private float _waitingTime = 1f;

    private Vector3 _nextPatrolPoint;
    private Coroutine _patrolRoutine;
    private DetectionTrigger _trigger;
    private EnemyState _currentState;

    protected override void Awake()
    {
        base.Awake();
        _nextPatrolPoint = transform.position;
        _trigger = GetComponentInChildren<DetectionTrigger>();

        _trigger.OnPlayerIn += Chase;
        _trigger.OnPlayerOut += Patrol;
    }
    private void OnDisable()
    {
        _healthProvider.OnDied -= Die;
        if (_patrolRoutine != null) StopCoroutine(_patrolRoutine);
    }
    public override void Revive(Spawner spawner)
    {
        base.Revive(spawner);

        _target = null;
        _currentState = EnemyState.Patrol;
        _healthProvider.OnDied += Die;

        _trigger.Collider.radius = Stats[StatType.DetectionRange];
    }

    private void FixedUpdate()
    {
        switch (_currentState)
        {
            case EnemyState.Patrol:
                if (Vector3.Distance(transform.position, _nextPatrolPoint) <= _agent.stoppingDistance + _threshold || _agent.pathStatus == NavMeshPathStatus.PathInvalid)
                {
                    StartCoroutine(Wait());
                }
                _agent.SetDestination(_nextPatrolPoint);
                break;
            case EnemyState.Chase:
                if (Vector3.Distance(transform.position, _target.position) <= Stats[StatType.AttackRange])
                {
                    _agent.ResetPath();
                    Attack();
                }
                else 
                {
                    _agent.SetDestination(_target != null ? _target.position : _nextPatrolPoint); 
                }
                break;
        }
    }
    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(_waitingTime);
        SetNewPatrolPoint();
    }
    private void SetNewPatrolPoint()
    {
        Vector2 offset = Random.insideUnitCircle * _patrolRadius;
        _nextPatrolPoint = (_spawner != null ? _spawner.transform.position : transform.position) +
                                new Vector3(offset.x, _spawner != null ? _spawner.transform.position.y : transform.position.y, offset.y);
    }
    private void Chase(Transform target)
    {
        Debug.Log($"[Enemy] player enter");
        StopCoroutine(Wait());
        _target = target;
        _currentState = EnemyState.Chase;
    }
    private void Patrol(Transform target)
    {
        if (_target != target) return;
        Debug.Log($"[Enemy] player exit");
        _target = null;
        _currentState = EnemyState.Patrol;
    }
    private void Attack()
    {
        if (!_canAttack) return;

        _canAttack = false;
        var health = _target.GetComponent<IDamageable>();
        health?.TakeDamage(new DamageContext(gameObject, Stats[StatType.Attack], _target));

        StartCoroutine(AttckReload());
    }
    private IEnumerator AttckReload()
    {
        yield return new WaitForSeconds(Stats[StatType.AttackSpeed]);
        _canAttack = true;
    }
    private void Die()
    {
        _agent.ResetPath();

        SoundManager.Instance.PlaySound(_deathSoundName);

        StopAllCoroutines();

        _spawner.OnEnemyDied(this);
    }

    private void OnDestroy()
    {
        _trigger.OnPlayerIn -= Chase;
        _trigger.OnPlayerOut -= Patrol;
    }

    private void OnDrawGizmos()
    {
        if (_spawner == null) return;

        Gizmos.color = Color.orange;
        Gizmos.DrawWireSphere(transform.position, Stats[StatType.DetectionRange]);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_spawner.transform.position, _patrolRadius);
    }
}
