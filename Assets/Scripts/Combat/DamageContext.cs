using System.Collections.Generic;
using UnityEngine;

public class DamageContext
{
    public GameObject Attacker { get; private set; }
    public GameObject[] Targets { get; private set; }
    public float BaseAmount { get; private set; }
    public DamageModifiers Modifiers { get; private set; }

    private List<DamageComponent> _components = new List<DamageComponent>();
    public IReadOnlyList<DamageComponent> Components => _components;

    public DamageContext(GameObject attacker, GameObject[] targets, float amount, DamageModifiers? modifiers = null)
    {
        Attacker = attacker;
        BaseAmount = amount;
        Targets = targets;
        Modifiers = modifiers ?? default;
    }

    // Метод для добавления частей урона
    public void AddComponent(DamageComponent component)
    {
        _components.Add(component);
    }

    // Удобный метод для быстрого добавления
    public void AddDamage(GameObject source, float amount, DamageType type, bool isCrit = false, DamageModifiers? modifiers = null, DamageTags[] damageTags = null)
    {
        _components.Add(new DamageComponent(source, amount, type, isCrit, modifiers, damageTags));
    }
}
