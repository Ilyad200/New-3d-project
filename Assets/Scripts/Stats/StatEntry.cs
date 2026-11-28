using UnityEngine;

public class StatEntry
{
    public Stat Stat;
    public DamageType Type;     // нужен только для типизированных
    public ModOp Op;            // Flat или Percent
    public float Value;         // для Percent: 0.2 = 20%
}
