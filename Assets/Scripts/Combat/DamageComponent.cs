using UnityEngine;

public class DamageComponent
{
    public float Amount { get; private set; }
    public DamageType Type { get; private set; }
    public bool IsCritical { get; private set; }
    public DamageModifiers Modifiers { get; private set; }

    // Тег для специфической фильтрации (например, "Mining", "Poison", "Weapon")
    public string SourceTag { get; private set; }

    public DamageComponent(float amount, DamageType type, bool isCrit = false, DamageModifiers? modifiers = null, string sourceTag = null)
    {
        Amount = amount;
        Type = type;
        IsCritical = isCrit;
        Modifiers = modifiers ?? default;
        SourceTag = sourceTag;
    }
}
