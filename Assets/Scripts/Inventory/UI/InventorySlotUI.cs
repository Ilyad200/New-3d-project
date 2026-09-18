using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour
{
    [SerializeField] private Image _iconImage;
    [SerializeField] private Image _border;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _quantityText;
    [SerializeField] private Button _slotButton;

    private IItem _item;

    private bool _isSelected = false;

    public void Setup(IItem item, int quantity)
    {
        EventBroker.OnItemEquip -= ItemEquipHandler;

        _isSelected = false;
        _border.color = Color.gray;

        if (item != null)
        {
            _item = item;
            _iconImage.gameObject.SetActive(true);
            _nameText.gameObject.SetActive(true);
            _nameText.text = item.Name;
            _quantityText.gameObject.SetActive(quantity > 1);
            _quantityText.text = quantity.ToString();

            _iconImage.sprite = item.Icon;

            if (item is IEquipment equipment1)
            {
                _border.color = equipment1.IsEquipped ? Color.orange : Color.gray;
            }
        }
        else
        {
            _iconImage.gameObject.SetActive(false);
            _quantityText.gameObject.SetActive(false);
            _nameText.gameObject.SetActive(false);
        }

        _slotButton.onClick.RemoveAllListeners();
        _slotButton.onClick.AddListener(() => {
            _isSelected = !_isSelected;
            _border.color = _isSelected ? Color.red : Color.gray;

            if (item is IEquipment equipment)
            {
                EventBroker.ItemEquip(equipment);
            }
        });

        if (item is IEquipment equipment)
        {
            EventBroker.OnItemEquip += ItemEquipHandler;
        }
    }
    private void ItemEquipHandler(IEquipment equipment)
    {
        if (_item is IEquipment equipmentItem)
        {
            _border.color = equipmentItem.IsEquipped ? Color.orange : Color.gray;
        }
    }
}
