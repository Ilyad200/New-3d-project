using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public interface IHaveStats
{
    Stat GetStat(StatType type, DamageType damageType = DamageType.None);
    float GetStatValue(StatType type, DamageType damageType = DamageType.None);
}
