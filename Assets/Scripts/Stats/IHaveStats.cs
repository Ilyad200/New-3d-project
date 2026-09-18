using System.Collections.Generic;
using UnityEngine;

public interface IHaveStats
{
    Dictionary<StatType, float> Stats { get; }

    public void SetStatType(StatType type, float amount);
}
