using UnityEngine;

public enum DamageType
{
    None = 0,
    Physical = 1,
    Fire = 2,
    Ice = 3,
    Lightning = 4,
    Poison = 5,
    Magic = 6,
    True = 7,      // Игнорирует броню и резисты
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
