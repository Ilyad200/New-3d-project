using UnityEngine;

public readonly struct DamageModifiers
{
    public float FlatBonus { get; }
    public float GlobalMultiplier { get; }
    public float ArmorPenetrationFlat { get; }
    public float ArmorPenetrationPercent { get; }
    public bool ForceCritical { get; }

    public DamageModifiers(float flat = 0f, float mult = 1f, float penFlat = 0f, float penPercent = 0f, bool crit = false)
    {
        FlatBonus = flat;
        GlobalMultiplier = mult;
        ArmorPenetrationFlat = penFlat;
        ArmorPenetrationPercent = Mathf.Clamp01(penPercent);
        ForceCritical = crit;
    }

    // Иммутабельное объединение с явными правилами стака
    public DamageModifiers Combine(DamageModifiers other)
    {
        return new DamageModifiers(
            flat: FlatBonus + other.FlatBonus,
            mult: GlobalMultiplier * other.GlobalMultiplier, // мультипликаторы перемножаются
            penFlat: ArmorPenetrationFlat + other.ArmorPenetrationFlat,
            penPercent: Mathf.Clamp01(ArmorPenetrationPercent + other.ArmorPenetrationPercent),
            crit: ForceCritical || other.ForceCritical
        );
    }

    // Удобные методы для создания изменённых копий
    public DamageModifiers WithFlatBonus(float value) => new DamageModifiers(value, GlobalMultiplier, ArmorPenetrationFlat, ArmorPenetrationPercent, ForceCritical);
    public DamageModifiers WithMultiplier(float value) => new DamageModifiers(FlatBonus, value, ArmorPenetrationFlat, ArmorPenetrationPercent, ForceCritical);
}