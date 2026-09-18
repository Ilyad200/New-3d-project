using System;
using UnityEngine;

[Serializable]
public struct HealEventArgs
{
    public float PreviousHP;
    public float CurrentHP;
    public float MaxHP;
    public HealContext Context;
}
