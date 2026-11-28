using System;
using UnityEngine;

public class DamageModifier : ScriptableObject
{
    public DamageType Type { get; }
    public float Flat { get; }
    public float Multiplier { get; }
    public DamageModifier(DamageType type, float flat, float multiplier)
    {
        Type = type;
        Flat = flat;
        Multiplier = multiplier;
    }
    public DamageModifier Combine(DamageModifier other)
    {
        return new DamageModifier(
            type: other.Type,
            flat: Flat + other.Flat,
            multiplier: Multiplier * other.Multiplier
        );
    }
    public DamageModifier WithFlat(float value) => new(Type, value, Multiplier);
    public DamageModifier WithMultiplier(float value) => new(Type, Flat, value);
}

public class DefenceModifier : ScriptableObject
{
    public DamageType Type { get; }
    public float Flat { get; }
    public float Multiplier { get; }
    public DefenceModifier(DamageType type, float flat, float multiplier)
    {
        Type = type;
        Flat = flat;
        Multiplier = multiplier;
    }
    public DefenceModifier Combine(DefenceModifier other)
    {
        return new DefenceModifier(
            type: other.Type,
            flat: Flat + other.Flat,
            multiplier: Multiplier * other.Multiplier
        );
    }
    public DefenceModifier WithFlat(float value) => new (Type, value, Multiplier);
    public DefenceModifier WithMultiplier(float value) => new (Type, Flat, value);
}

public class PenetrationModifier : ScriptableObject
{
    public DamageType Type { get; }
    public float Flat { get; }
    public float Multiplier { get; }
    public PenetrationModifier(DamageType type, float flat, float multiplier)
    {
        Type = type;
        Flat = flat;
        Multiplier = multiplier;
    }
    public PenetrationModifier Combine(PenetrationModifier other)
    {
        return new PenetrationModifier(
            type: other.Type,
            flat: Flat + other.Flat,
            multiplier: Multiplier * other.Multiplier
        );
    }
    public PenetrationModifier WithFlat(float value) => new(Type, value, Multiplier);
    public PenetrationModifier WithMultiplier(float value) => new(Type, Flat, value);
}