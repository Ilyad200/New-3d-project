using System.Collections.Generic;
using UnityEngine;

public class DamageContext
{
    public GameObject Attacker { get; private set; }
    public GameObject Target { get; private set; }
    public float BaseAmount { get; private set; }
    public DamageModifier Modifiers { get; private set; }

    private List<DamageComponent> _components = new List<DamageComponent>();
    public IReadOnlyList<DamageComponent> Components => _components;

    public DamageContext(GameObject attacker, GameObject target)
    {
        Attacker = attacker;
        Target = target;
    }

    // Метод для добавления частей урона
    public void AddComponent(DamageComponent component)
    {
        _components.Add(component);
    }

    // Удобный метод для быстрого добавления
    public void AddComponent(GameObject source, float amount, DamageType type, bool isCrit = false, DamageModifier? modifiers = null, DamageTags[] damageTags = null)
    {
        _components.Add(new DamageComponent(source, amount, type, isCrit, modifiers, damageTags));
    }
}
