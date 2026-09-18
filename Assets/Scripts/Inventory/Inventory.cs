using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private int _capacity = 100;
    public int Capacity => _capacity;

    private readonly List<InventorySlot> _slots = new();
    public List<InventorySlot> Slots => _slots;

    public event Action<int> OnSlotUpdated;

    private Dictionary<EquipmentSlot, IEquipment> _equipments = new() 
    {
        { EquipmentSlot.Helmet, null },
        { EquipmentSlot.Chestplate, null },
        { EquipmentSlot.Gauntlet, null },
        { EquipmentSlot.Legins, null },
        { EquipmentSlot.Boots, null },
        { EquipmentSlot.Accessory, null },
        { EquipmentSlot.RightArm, null },
        { EquipmentSlot.LeftArm, null }
    };

    public Dictionary<EquipmentSlot, IEquipment> Equipments => _equipments;

    private void Awake()
    {
        for (int i = 0; i < _capacity; i++)
            _slots.Add(new InventorySlot());
    }
    private void OnEnable()
    {
        EventBroker.OnItemEquip += EquipItem;
        EventBroker.OnItemUnequip += UnequipItem;
    }
    private void OnDisable()
    {
        EventBroker.OnItemEquip -= EquipItem;
        EventBroker.OnItemUnequip -= UnequipItem;
    }

    public bool AddItem(IItem newItem, int amount = 1)
    {
        if (newItem == null || amount <= 0) return false;

        
        if (newItem.IsStackable && newItem is not IEquipment)
        {
            for (int i = 0; i < _slots.Count; i++)
            {
                if (!_slots[i].IsEmpty && _slots[i].Item.Id == newItem.Id)
                {
                    _slots[i].Quantity += amount;
                    OnSlotUpdated?.Invoke(i);
                    return true;
                }
            }
        }
        
        for (int i = 0; i < _slots.Count; i++)
        {
            if (_slots[i].IsEmpty)
            {
                _slots[i].Item = newItem;
                _slots[i].Quantity = amount;
                OnSlotUpdated?.Invoke(i);

                return true;
            }
        }

        Debug.LogWarning("[Инвентарь] Нет свободного места!");
        return false;
    }

    public (IItem item, int quantity) GetSlotData(int index)
    {
        if (index < 0 || index >= _slots.Count) return (null, 0);
        var slot = _slots[index];
        return (slot.Item, slot.Quantity);
    }

    public void EquipItem(IEquipment equipment)
    {
        if (_equipments.TryGetValue(equipment.Slot, out IEquipment curEquipment))
        {
            curEquipment?.Unequip();
        }
        _equipments[equipment.Slot] = equipment;
        equipment.Equip();
    }
    public void UnequipItem(IEquipment equipment)
    {
        _equipments[equipment.Slot] = null;
        equipment.Unequip();
    }
    public int GetItemsCount(string id)
    {
        int count = 0;
        foreach (var slot in _slots)
        {
            if (slot.IsEmpty) continue;
            if (slot.Item.Id == id)
            {
                count += slot.Quantity;
            }
        }
        return count;
    }

    public void ClearInventory()
    {
        _slots.Clear();
        for (int i = 0; i < _capacity; i++)
            _slots.Add(new InventorySlot());
        _equipments = new()
        {
            { EquipmentSlot.Helmet, null },
            { EquipmentSlot.Chestplate, null },
            { EquipmentSlot.Gauntlet, null },
            { EquipmentSlot.Legins, null },
            { EquipmentSlot.Boots, null },
            { EquipmentSlot.Accessory, null },
            { EquipmentSlot.RightArm, null },
            { EquipmentSlot.LeftArm, null }
        };
    }
}
