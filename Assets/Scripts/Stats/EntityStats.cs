using System;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class EntityStats : MonoBehaviour, IHaveStats
{
    [SerializeField]
    private DamageStats baseStats;

    private readonly Dictionary<StatId, Stat> stats = new();

    private void Awake()
    {
        ComputeStats();
    }
    private void ComputeStats()
    {
        if (baseStats == null) return;

        foreach (var kvp in baseStats.Stats)
        {
            var stat = GetStat(kvp.Key.StatType, kvp.Key.DamageType);
            stat.RemoveAllFromSource(baseStats);   // повторный вызов не дублирует
            stat.AddModifier(Guid.NewGuid(), ModifierType.Flat, kvp.Value, baseStats);
        }
    }
    public Stat GetStat(StatType type, DamageType damageType = DamageType.None)
    {
        var id = new StatId(type, damageType);
        if (!stats.TryGetValue(id, out var stat))
            stats[id] = stat = new Stat();   // создаётся лениво
        return stat;
    }
    public float GetStatValue(StatType type, DamageType damageType = DamageType.None)
    {
        return stats.TryGetValue(new StatId(type, damageType), out var s) ? s.Value : 0f;
    }
}
