using System;
using System.Collections.Generic;
using UnityEngine;

public class CombatProfile
{
    public List<StatBlock> StatBlocks {  get; private set; }

    public void AddStatBlock(StatBlock statBlock)
    {
        StatBlocks.Add(statBlock);
    }

    public float Get(Stat stat)
    {
        var finalCount = 0f;
        foreach (StatBlock statBlock in StatBlocks)
        {
            foreach (var s in statBlock.Entries)
            {
                if (s.StatType != stat.StatType || s.IsTyped() && s.Op != stat.Op) continue;

                finalCount += s.Value;
            }
        }
        return finalCount;
    }
}