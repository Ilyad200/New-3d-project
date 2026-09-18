using System.Collections.Generic;
using UnityEngine;

public interface IEquipment : IItem
{
    public bool IsEquipped { get; }
    public Dictionary<StatType, float> Stats { get; }
    public EquipmentSlot Slot { get; }

    public void Equip();
    public void Unequip();
}
