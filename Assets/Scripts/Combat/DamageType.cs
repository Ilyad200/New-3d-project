using UnityEngine;

public enum DamageType
{
    Physical,
    Fire,
    Ice,
    Lightning,
    Poison,
    Magic,
    True,      // Игнорирует броню и резисты
    Axe, Pickaxe, Shovel // Интсрументы
}

public static class DamageTypeExtensions
{
    public static string ToDisplayName(this DamageType type) => type switch
    {
        DamageType.Physical => "Физический",
        DamageType.Fire => "Огонь",
        DamageType.Ice => "Лёд",
        DamageType.True => "Чистый",
        _ => type.ToString()
    };
}
