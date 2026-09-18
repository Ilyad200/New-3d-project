using UnityEngine;

public enum HealType 
{ 
    Instant, 
    OverTime, 
    Lifesteal, 
    Potion, 
    Totem, 
    Environment
}

public static class HealthTypeExtensions
{
    public static string ToDisplayName(this HealType type) => type switch
    {
        HealType.Instant => "Постоянное",
        HealType.Lifesteal => "Вампиризм",
        HealType.Potion => "Зелье",
        HealType.Totem => "Тотем",
        _ => type.ToString()
    };
}
