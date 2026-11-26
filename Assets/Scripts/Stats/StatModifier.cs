using System;

public class StatModifier
{
    public Guid Id;
    public ModifierType Type;
    public float Value;

    public object Source;
    public SourceCategory category;

    public StatModifier(Guid id, ModifierType type, float value, object source)
    {
        Id = id;
        Type = type;
        Value = value;
        Source = source;
    }
}
