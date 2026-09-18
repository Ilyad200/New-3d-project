using System.Collections.Generic;
using UnityEngine;

public class Equipment : SimpleItem, IEquipment
{
    private EquipmentSO _equipmentData;

    private Dictionary<StatType, float> _stats;
    private bool _isEquipped;
    public bool IsEquipped => _isEquipped;
    public Dictionary<StatType, float> Stats => _stats;
    public EquipmentSlot Slot => _equipmentData.Slot;
    public new ItemType Type => ItemType.Equipment;
    public Equipment(EquipmentSO equipmentSO) : base(equipmentSO)
    {
        _equipmentData = equipmentSO;
        _isEquipped = false;
        _stats = new Dictionary<StatType, float>()
        {
            { StatType.Attack, equipmentSO.Attack },
            { StatType.Defense, equipmentSO.Defence },
            { StatType.Speed, equipmentSO.Speed },
            { StatType.Health, equipmentSO.Health },
            { StatType.Durability, equipmentSO.Durability },
            { StatType.Regeneration, equipmentSO.Regenerate }
        }; 
    }

    public void Equip()
    {
        _isEquipped = true;

        var playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        foreach (var stat in _stats)
        {
            if (playerStats.Stats.TryGetValue(stat.Key, out var value))
            {
                playerStats.SetStatType(stat.Key, value + stat.Value);
            }
        }
    }
    public void Unequip()
    {
        _isEquipped = false;

        var playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        foreach (var stat in _stats)
        {
            if (playerStats.Stats.TryGetValue(stat.Key, out var value))
            {
                playerStats.SetStatType(stat.Key, value - stat.Value);
            }
        }
    }
}
