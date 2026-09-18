using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryWindowUI : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;
    [SerializeField] private Transform _slotsContainer;
    [SerializeField] private InventorySlotUI _slotPrefab;
    [SerializeField] private GameObject _windowPanel;

    public PlayerInputs _playerInputs;

    private List<InventorySlotUI> _uiSlots = new List<InventorySlotUI>();
    private bool _isOpen = false;

    private void OnEnable()
    {
        _inventory.OnSlotUpdated += UpdateSingleSlot;
    }

    private void OnDisable()
    {
        _inventory.OnSlotUpdated -= UpdateSingleSlot;
    }

    private void Awake()
    {
        _playerInputs = new PlayerInputs();

        _playerInputs.Player.Interact.performed += OpenOrClose;
        _playerInputs.Player.Enable();

        GameManager.OnGamePaused += ChangeInventoryStatus;
        GameManager.OnPlayerLiveStatusUpdate += ChangeInventoryStatus;
        GameManager.OnStatWindowUpdate += ChangeInventoryStatus;
    }

    private void Start()
    {
        for (int i = 0; i < _inventory.Capacity; i++)
        {
            var slot = Instantiate(_slotPrefab, _slotsContainer);
            slot.gameObject.SetActive(false);
            _uiSlots.Add(slot);
        }

        _windowPanel.SetActive(false);
    }

    void OpenOrClose(InputAction.CallbackContext context)
    {
        _isOpen = !_isOpen;
        GameManager.InventoryUpdate(_isOpen);
        _windowPanel.SetActive(_isOpen);
        if (_isOpen) Refresh();
    }
    private void ChangeInventoryStatus(bool isClose)
    {
        if (isClose)
        {
            _isOpen = false;
            _playerInputs.Player.Disable();
            _windowPanel.SetActive(false);
        }
        else _playerInputs.Player.Enable();
    }

    public void Refresh()
    {
        var items = _inventory.Slots;

        for (int i = 0; i < _uiSlots.Count; i++)
        {
            _uiSlots[i].Setup(items[i].Item, items[i].Quantity);
            _uiSlots[i].gameObject.SetActive(items[i].Item != null);
        }
    }
    private void UpdateSingleSlot(int index)
    {
        if (index < 0 || index >= _uiSlots.Count) return;
        if (!_isOpen) return;

        var (item, qty) = _inventory.GetSlotData(index);

        _uiSlots[index].Setup(item, qty);
        _uiSlots[index].gameObject.SetActive(item != null);
    }

    private void OnDestroy()
    {
        GameManager.OnGamePaused -= ChangeInventoryStatus;
        GameManager.OnPlayerLiveStatusUpdate -= ChangeInventoryStatus;
        GameManager.OnStatWindowUpdate -= ChangeInventoryStatus;
    }
}
