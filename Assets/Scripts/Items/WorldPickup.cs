using UnityEngine;

public class WorldPickup : MonoBehaviour
{
    [SerializeField] private string _pickupSoundName;
    private IItem _item;
    private int _quantity;

    public void Setup(IItem item, int quantity)
    {
        _item = item;
        _quantity = quantity;
    }

    public bool TryPickup(Inventory inventory)
    {
        if (_item == null || inventory == null) return false;

        if (inventory.AddItem(_item, _quantity))
        {
            SoundManager.Instance.PlaySound(_pickupSoundName);
            Destroy(gameObject);
            return true;
        }

        return false;
    }
}
