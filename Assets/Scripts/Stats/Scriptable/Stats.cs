using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stats", menuName = "Game/Stats/EntityStats")]
public class Stats : ScriptableObject
{
    public List<Stat> StatList = new();
    public List<DefenceModifier> Resistances = new();
    public List<PenetrationModifier> Penetrations = new();

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (SyncWithEnum())
            UnityEditor.EditorUtility.SetDirty(this); // чтобы изменения сохранились при реимпорте
    }
#endif

    private bool SyncWithEnum()
    {
        bool changed = false;
        var seen = new HashSet<StatType>();

        // убрать дубли и типы, которых больше нет в enum
        changed |= StatList.RemoveAll(s =>
            !Enum.IsDefined(typeof(StatType), s.StatType) || !seen.Add(s.StatType)) > 0;

        // добавить недостающие
        foreach (StatType type in Enum.GetValues(typeof(StatType)))
        {
            if (seen.Add(type) == false) continue;
            StatList.Add(new Stat(type));
            changed = true;
        }

        if (changed)
            StatList.Sort((a, b) => ((int)a.StatType).CompareTo((int)b.StatType));

        return changed;
    }
}
