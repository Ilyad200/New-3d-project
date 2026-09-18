using System;
using UnityEngine;

public static class EventBroker
{
    public static event Action<IEquipment> OnItemEquip;
    public static event Action<IEquipment> OnItemUnequip;
    public static void ItemEquip(IEquipment item) => OnItemEquip?.Invoke(item);
    public static void ItemUnequip(IEquipment item) => OnItemUnequip?.Invoke(item);
}
