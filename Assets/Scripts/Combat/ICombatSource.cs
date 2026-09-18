using UnityEngine;

public enum Faction { Player, Enemy, Neutral, Ally, Environment }

public interface ICombatSource
{
    GameObject Owner { get; }
    Faction Faction { get; }
    int Level { get; }
    //bool IsValid { get; }

    //// Базовые модификаторы (могут быть переопределены в наследниках)
    //float GetFlatBonus(DamageType type);
    //float GetMultiplier(DamageType type);
    //float GetArmorPenetration(DamageType type);
}