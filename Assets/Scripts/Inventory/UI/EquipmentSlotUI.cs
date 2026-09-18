using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentSlotUI : MonoBehaviour
{
    [SerializeField] private Image _iconImage;
    [SerializeField] private Image _border;
    [SerializeField] private Button _slotButton;
    [SerializeField] private EquipmentSlot _slot;

    private IEquipment _item;

    private void Awake()
    {
        GameManager.OnPlayerLiveStatusUpdate += ResetSlot;
    }

    private void OnEnable()
    {
        EventBroker.OnItemEquip += ItemEquipHandler;
    }
    private void OnDisable()
    {
        EventBroker.OnItemEquip -= ItemEquipHandler;
    }

    public void Setup(IEquipment item)
    {
        if (item != null)
        {
            _item = item;

            _iconImage.sprite = item.Icon;
        }
        else
        {
            _iconImage.sprite = null;
        }

        _slotButton.onClick.RemoveAllListeners();
        _slotButton.onClick.AddListener(() => {
            if (!_item.IsEquipped) return;
            EventBroker.ItemUnequip(_item);
            _iconImage.sprite = null;
        });
    }
    private void ItemEquipHandler(IEquipment equipment)
    {
        if (equipment.Slot == _slot)
        {
            Setup(equipment);
        }
    }
    private void ResetSlot(bool isReset)
    {
        _iconImage.sprite = null;
    }
    private void OnDestroy()
    {
        GameManager.OnPlayerLiveStatusUpdate -= ResetSlot;
    }
}
