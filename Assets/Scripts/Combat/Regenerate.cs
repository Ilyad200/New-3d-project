using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Regenerate : MonoBehaviour
{
    private IHealable _healable;
    private IHealthProvider _healthProvider;
    private EntityStats _stats;
    private Coroutine _regenRoutine;

    private void Awake()
    {
        _healable = GetComponent<IHealable>();
        _healthProvider = GetComponent<IHealthProvider>();
        _stats = GetComponent<EntityStats>();
    }
    private void OnDisable()
    {
        StopRegen();
    }
    private IEnumerator Regeneration()
    {
        while(_healthProvider.IsAlive)
        {
            yield return new WaitForSeconds(5f);
            _healable.Heal(new HealContext(gameObject, _stats.Get(StatType.Regeneration)));
        }
    }
    public void StartRegen()
    {
        if (_stats.Get(StatType.Regeneration) <= 0) return;
        if (_regenRoutine != null) return; // уже идёт

        _regenRoutine = StartCoroutine(Regeneration());
    }
    public void StopRegen()
    {
        if (_regenRoutine == null) return;

        StopCoroutine(_regenRoutine);
        _regenRoutine = null;
    }
}
