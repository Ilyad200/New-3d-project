using UnityEngine;

public class HealContext
{
    public GameObject Source { get; private set; }
    public HealType Type { get; private set; }
    public float BaseAmount { get; private set; }
    public bool IsCritical { get; private set; }
    public DamageModifiers Modifiers { get; private set; }

    public HealContext(GameObject source, float amount, HealType type = HealType.Instant, bool isCrit = false, DamageModifiers? modifiers = null)
    {
        Source = source;
        Type = type;
        BaseAmount = amount;
        IsCritical = isCrit;
        Modifiers = modifiers ?? default;
    }

    //Замена выражению `with { BaseAmount = newAmount }`
    public HealContext WithAmount(float newAmount) =>
        new HealContext(Source, newAmount, Type, IsCritical, Modifiers);

    public HealContext WithSource(GameObject newSource) =>
        new HealContext(newSource, BaseAmount, Type, IsCritical, Modifiers);
}
