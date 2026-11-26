using System;

[Serializable]
public struct StatId : IEquatable<StatId>
{
    public StatType StatType;
    public DamageType DamageType;

    public StatId(StatType type, DamageType damageType = DamageType.None)
    {
        StatType = type;
        DamageType = damageType;
    }

    public bool Equals(StatId o) => StatType == o.StatType && DamageType == o.DamageType;
    public override bool Equals(object o) => o is StatId s && Equals(s);
    public override int GetHashCode() => HashCode.Combine(StatType, DamageType);
}
