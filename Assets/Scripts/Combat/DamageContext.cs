using UnityEngine;

public class DamageContext
{
    public GameObject Source { get; private set; }
    public DamageType Type { get; private set; }
    public float BaseAmount { get; private set; }
    public bool IsCritical { get; private set; }
    public DamageModifiers Modifiers { get; private set; }
    public Transform Target { get; private set; }

    public DamageContext(GameObject source, float amount, Transform target, DamageType type = DamageType.Physical, bool isCrit = false, DamageModifiers? modifiers = null)
    {
        Source = source;
        BaseAmount = amount;
        Target = target;
        Type = type;
        IsCritical = isCrit;
        Modifiers = modifiers ?? default;
    }
}
