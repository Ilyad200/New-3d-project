using UnityEngine;
using System.Collections;
using static Unity.VisualScripting.Dependencies.Sqlite.SQLite3;

[RequireComponent(typeof(HealthComponent))]
public class ResourceNode : MonoBehaviour, IDamageFilter
{
    [SerializeField] private DamageType _requiredType = DamageType.Pickaxe;
    [SerializeField] private float _respawnDelay;

    protected IHealthProvider _healthProvider;
    private void Awake()
    {
        _healthProvider = GetComponent<HealthComponent>();
    }

    public bool CanTakeDamage(DamageContext ctx) => ctx.Type == _requiredType;

    private void OnEnable() => GetComponent<HealthComponent>().OnDied += OnDied;
    private void OnDisable() => GetComponent<HealthComponent>().OnDied -= OnDied;

    private void OnDied(DamageContext ctx)
    {
        gameObject.SetActive(false);
        StartCoroutine(RespawnAfterDelay());
    }
    private IEnumerator RespawnAfterDelay()
    {
        yield return new WaitForSeconds(_respawnDelay);

        gameObject.SetActive(true);
        _healthProvider.Revival(_healthProvider.MaxHP);
    }
}