using System.Collections.Generic;
using UnityEngine;

public interface IHaveStats
{
    public float Get(StatType type);

    public void SetStatType(StatType type, float amount);
}
