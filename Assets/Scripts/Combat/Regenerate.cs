using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Regenerate : MonoBehaviour
{
    [SerializeField] private float interval = 5f;

    private IHealthProvider health;
    private IHealable healable;
    private Stat regenStat;
    private float timer;

    private void Awake()
    {
        health = GetComponent<IHealthProvider>();
        healable = GetComponent<IHealable>();
        regenStat = GetComponent<EntityStats>().GetStat(StatType.Regeneration);
    }

    private void Update()
    {
        // Мёртвым и полностью здоровым таймер не нужен
        if (health.CurrentHP <= 0f || health.CurrentHP >= health.MaxHP || regenStat.Value <= 0)
        {
            timer = 0f;
            return;
        }

        timer += Time.deltaTime;
        float step = Mathf.Max(0.01f, interval);   // защита от нуля в инспекторе

        while (timer >= step)
        {
            timer -= step;
            healable.Heal(new HealContext(gameObject, regenStat.Value));
        }
    }
}
