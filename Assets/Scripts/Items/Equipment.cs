using System;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : SimpleItem, IEquipment
{
    private EquipmentSO _equipmentData;

    private bool _isEquipped;
    public bool IsEquipped => _isEquipped;
    public EquipmentSlot Slot => _equipmentData.Slot;
    public EquipmentSO Data => _equipmentData;
    public new ItemType Type => ItemType.Equipment;
    public Equipment(EquipmentSO equipmentSO) : base(equipmentSO)
    {
        _equipmentData = equipmentSO;
        _isEquipped = false;
    }

    public void Equip(ICombatSource owner)
    {
        _isEquipped = true;

        var ownerStat = owner.GetComponent<IHaveStats>();
        if (ownerStat == null) return;

        foreach (var stat in _equipmentData.Stats)
        {
            ownerStat.GetStat(stat.Key.StatType, stat.Key.DamageType).AddModifier(Guid.NewGuid(), ModifierType.Flat, stat.Value, this);            
        }
    }

    public void Unequip(ICombatSource owner)
    {
        _isEquipped = false;

        var ownerStat = owner.GetComponent<IHaveStats>();
        if (ownerStat == null) return;

        foreach (var stat in _equipmentData.Stats)
        {
            playerStats.GetStat(stat.Key.StatType, stat.Key.DamageType).RemoveAllFromSource(this);
        }
    }
}