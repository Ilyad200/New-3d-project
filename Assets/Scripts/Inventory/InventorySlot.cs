using UnityEngine;

public class InventorySlot
{
    public IItem Item;
    public int Quantity = 1;
    public bool IsEmpty => Item == null;
}
