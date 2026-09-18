using System;
using UnityEngine;

public class Dummy : MonoBehaviour, ICombatSource
{
    public GameObject Owner { get; }
    public Faction Faction { get; }
    public int Level { get; }
    public bool IsValid { get; }


    private IHealthProvider health;
    private IHealable healable;

    private EntityDrops drops;

    private void Awake()
    {
        health = GetComponent<IHealthProvider>();
        healable = GetComponent<IHealable>();

        drops = GetComponent<EntityDrops>();
    }
    private void OnEnable()
    {
        health.OnDied += Die;
    }
    private void OnDisable()
    {
        health.OnDied -= Die;
    }

    private void Die()
    {
        healable.Heal(new HealContext(gameObject, healable.MaxHP));
    }

    public float GetFlatBonus(DamageType type) => 0f;
    public float GetMultiplier(DamageType type) => 1f;
    public float GetArmorPenetration(DamageType type) => 0f;
}
