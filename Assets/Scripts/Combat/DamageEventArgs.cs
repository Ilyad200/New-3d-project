using System;
using UnityEngine;

[Serializable]
public struct DamageEventArgs
{
    public float PreviousHP;
    public float CurrentHP;
    public float MaxHP;
    public DamageContext Context;
}
