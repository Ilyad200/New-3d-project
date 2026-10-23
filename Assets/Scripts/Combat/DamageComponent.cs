using UnityEngine;

public class DamageComponent
{
    public GameObject Source { get; private set; }
    public float Amount { get; private set; }
    public DamageType Type { get; private set; }
    public bool IsCritical { get; private set; }
    public DamageModifiers Modifiers { get; private set; }

    // Тег для специфической фильтрации (например, "DoT", "AoE")
    public DamageTags[] DamageTags { get; private set; }

    public DamageComponent(GameObject source, float amount, DamageType type, bool isCrit = false, DamageModifiers? modifiers = null, DamageTags[] damageTags = null)
    {
        Source = source;
        Amount = amount;
        Type = type;
        IsCritical = isCrit;
        Modifiers = modifiers ?? default;
        DamageTags = damageTags;
    }
}
