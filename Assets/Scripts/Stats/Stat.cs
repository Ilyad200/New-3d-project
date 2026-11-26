using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class Stat
{
    private List<StatModifier> modifiers = new();
    private float currentValue;

    public event Action<Stat> Changed;

    public float Value => currentValue;
    public IReadOnlyList<StatModifier> Modifiers => modifiers;

    public void AddModifier(StatModifier modifier)
    {
        modifiers.Add(modifier);
        Recalculate();
    }
    public void AddModifier(Guid id, ModifierType type, float value, object source)
    {
        var modifier = new StatModifier(id, type, value, source);
        modifiers.Add(modifier);
        Recalculate();
    }
    public void RemoveModifier(StatModifier modifier)
    {
        if (modifiers.Remove(modifier)) Recalculate();
    }
    public void RemoveAllFromSource(object source)
    {
        if (modifiers.RemoveAll(m => m.Source.Equals(source)) > 0) Recalculate();
    }
    public void RemoveAllModifiers()
    {
        modifiers.Clear(); Recalculate();
    }

    private void Recalculate()
    {
        var flatBonus = 0f;
        var multBonus = 1f;
        foreach (var modifier in modifiers)
        {
            if (modifier.Type == ModifierType.Flat)
                flatBonus += modifier.Value;
            if (modifier.Type == ModifierType.Percentage)
                multBonus *= (1 + modifier.Value);
        }
        currentValue = flatBonus * multBonus;
        Changed?.Invoke(this);
    }
}
