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
        //_stats = new Dictionary<StatType, float>()
        //{
        //    { StatType.Attack, equipmentSO.Attack },
        //    { StatType.Defense, equipmentSO.Defence },
        //    { StatType.Speed, equipmentSO.Speed },
        //    { StatType.Health, equipmentSO.Health },
        //    { StatType.Durability, equipmentSO.Durability },
        //    { StatType.Regeneration, equipmentSO.Regenerate }
        //};
    }

    public void Equip()
    {
        _isEquipped = true;

        var playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<IHaveStats>();
        foreach (var stat in _equipmentData.Stats)
        {
            playerStats.GetStat(stat.Key.StatType, stat.Key.DamageType).AddModifier(Guid.NewGuid(), ModifierType.Flat, stat.Value, this);            
        }
    }
    public void Unequip()
    {
        _isEquipped = false;

        var playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<IHaveStats>();
        foreach (var stat in _equipmentData.Stats)
        {
            playerStats.GetStat(stat.Key.StatType, stat.Key.DamageType).RemoveAllFromSource(this);
        }
    }
}