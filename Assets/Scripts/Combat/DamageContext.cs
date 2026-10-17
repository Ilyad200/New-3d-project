using System.Collections.Generic;
using UnityEngine;

public class DamageContext
{
    public GameObject Attacker { get; private set; }
    public Transform Target { get; private set; }
    public float BaseAmount { get; private set; }
    public DamageModifiers Modifiers { get; private set; }

    private List<DamageComponent> _components = new List<DamageComponent>();
    public IReadOnlyList<DamageComponent> Components => _components;

    public DamageContext(GameObject attacker, float amount, Transform target, DamageModifiers? modifiers = null)
    {
        Attacker = attacker;
        BaseAmount = amount;
        Target = target;
        Modifiers = modifiers ?? default;
    }

    // Метод для добавления частей урона
    public void AddComponent(DamageComponent component)
    {
        _components.Add(component);
    }

    // Удобный метод для быстрого добавления
    public void AddDamage(float amount, DamageType type, bool isCrit = false, DamageModifiers? modifiers = null, string sourceTag = null)
    {
        _components.Add(new DamageComponent(amount, type, isCrit, modifiers, sourceTag));
    }
}
