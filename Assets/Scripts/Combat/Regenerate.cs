using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Regenerate : MonoBehaviour
{
    private IHealable _healable;
    private IHealthProvider _healthProvider;
    private PlayerStats _stats;
    private bool _isRegenerating = false;

    private void Awake()
    {
        _healable = GetComponent<IHealable>();
        _healthProvider = GetComponent<IHealthProvider>();
        _stats = GetComponent<PlayerStats>();
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
            _healable.Heal(new HealContext(gameObject, _stats.Stats[StatType.Regeneration]));
        }
    }
    public void StartRegen()
    {
        if (_stats.Stats[StatType.Regeneration] <= 0) return;
        if (!_isRegenerating)
        {
            StartCoroutine(Regeneration());
            _isRegenerating = true;
        }
    }
    public void StopRegen()
    {
        StopCoroutine(Regeneration());
        _isRegenerating = false;
    }
}
