using UnityEngine;
using System.Collections;
using static Unity.VisualScripting.Dependencies.Sqlite.SQLite3;
using System.Linq;

[RequireComponent(typeof(HealthComponent))]
public class ResourceNode : MonoBehaviour, IDamageFilter
{
    [SerializeField] private DamageTags _requiredType = DamageTags.Resource_Pickaxe;
    [SerializeField] private float _respawnDelay;

    protected IHealthProvider _healthProvider;
    private void Awake()
    {
        _healthProvider = GetComponent<HealthComponent>();
    }

    public bool CanTakeDamage(DamageComponent component) => component.DamageTags.Contains(_requiredType);

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