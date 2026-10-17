using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Показывает экипированные предметы на персонаже (оружие в руке и т.д.).
/// Вешается на Player. Слушает EventBroker, поэтому с Inventory напрямую не связан.
/// </summary>
public class PlayerEquipmentVisuals : MonoBehaviour
{
    [Serializable]
    private struct SlotAnchor
    {
        public EquipmentSlot Slot;
        public Transform Anchor;
    }

    // Куда крепить модель для каждого слота. Для RightArm/LeftArm можно не заполнять,
    // если у персонажа Humanoid-риг: тогда возьмутся кости кистей.
    [SerializeField] private List<SlotAnchor> _anchors = new();

    [SerializeField] private Animator _animator;

    // Контроллер аниматора по умолчанию (без оружия / "кулаки"), берём при старте.
    private RuntimeAnimatorController _baseController;

    private readonly Dictionary<EquipmentSlot, Transform> _anchorBySlot = new();
    private readonly Dictionary<EquipmentSlot, (IEquipment item, GameObject view)> _views = new();

    private void Awake()
    {
        foreach (var a in _anchors)
        {
            if (a.Anchor != null) _anchorBySlot[a.Slot] = a.Anchor;
        }

        var animator = GetComponentInChildren<Animator>();
        TryAddBone(animator, EquipmentSlot.RightArm, HumanBodyBones.RightHand);
        TryAddBone(animator, EquipmentSlot.LeftArm, HumanBodyBones.LeftHand);
    }

    private void TryAddBone(Animator animator, EquipmentSlot slot, HumanBodyBones bone)
    {
        if (_anchorBySlot.ContainsKey(slot)) return;
        if (animator == null || !animator.isHuman) return;

        var t = animator.GetBoneTransform(bone);
        if (t != null) _anchorBySlot[slot] = t;
    }

    private void OnEnable()
    {
        EventBroker.OnItemEquip += OnEquip;
        EventBroker.OnItemUnequip += OnUnequip;
    }

    private void OnDisable()
    {
        EventBroker.OnItemEquip -= OnEquip;
        EventBroker.OnItemUnequip -= OnUnequip;
    }

    private void OnEquip(IEquipment item)
    {
        if (item == null) return;
        if (!_anchorBySlot.TryGetValue(item.Slot, out var anchor) || anchor == null) return;

        // Повторный клик по уже надетому предмету — ничего не пересоздаём.
        if (_views.TryGetValue(item.Slot, out var current) && current.item == item && current.view != null) return;

        RemoveView(item.Slot);

        var data = item.Data;
        if (data == null || data.HeldPrefab == null) return;

        var view = Instantiate(data.HeldPrefab, anchor);
        var t = view.transform;
        t.localPosition = data.HeldPosition;
        t.localRotation = Quaternion.Euler(data.HeldEulerAngles);
        t.localScale = Vector3.Scale(data.HeldPrefab.transform.localScale, data.HeldScale);

        StripPhysics(view);
        _views[item.Slot] = (item, view);
    }

    private void OnUnequip(IEquipment item)
    {
        if (item == null) return;
        if (_views.TryGetValue(item.Slot, out var current) && current.item == item)
            RemoveView(item.Slot);
    }

    /// <summary>Убрать все модели (вызывается при сбросе игрока).</summary>
    public void ClearAll()
    {
        foreach (var pair in _views)
        {
            if (pair.Value.view != null) Destroy(pair.Value.view);
        }
        _views.Clear();
    }

    private void RemoveView(EquipmentSlot slot)
    {
        if (!_views.TryGetValue(slot, out var current)) return;

        if (current.view != null) Destroy(current.view);
        _views.Remove(slot);
    }

    // Коллайдеры на модели в руке мешали бы CharacterController и физике.
    private static void StripPhysics(GameObject go)
    {
        foreach (var c in go.GetComponentsInChildren<Collider>()) c.enabled = false;
        foreach (var rb in go.GetComponentsInChildren<Rigidbody>()) rb.isKinematic = true;
    }
}