using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Equipment", menuName = "Game/Items/Equipment")]
public class EquipmentSO : SimpleItemSO
{
    [SerializeField] private Dictionary<StatId, float> _stats;
    [SerializeField] private EquipmentSlot _slot;
    [SerializeField] private DamageType _damageType;

    [Header("ћодель в руке / на теле")]
    [SerializeField] private GameObject _heldPrefab;
    [SerializeField] private Vector3 _heldPosition;
    [SerializeField] private Vector3 _heldEulerAngles;
    [SerializeField] private Vector3 _heldScale = Vector3.one;

    [Header("јнимаци€ атаки (только дл€ оружи€, слот RightArm)")]
    [Tooltip("Override Controller с клипом атаки дл€ этого оружи€. ≈сли не задан, используетс€ анимаци€ по умолчанию (кулаки).")]
    [SerializeField] private AnimatorOverrideController _attackOverride;

    public Dictionary<StatId, float> Stats => _stats;
    public EquipmentSlot Slot => _slot;
    public DamageType DamageType => _damageType;

    public GameObject HeldPrefab => _heldPrefab;
    public Vector3 HeldPosition => _heldPosition;
    public Vector3 HeldEulerAngles => _heldEulerAngles;
    public Vector3 HeldScale => _heldScale;

    public new bool IsStackable => false;
}
