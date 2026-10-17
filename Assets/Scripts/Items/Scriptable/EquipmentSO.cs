using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Equipment", menuName = "Game/Items/Equipment")]
public class EquipmentSO : SimpleItemSO
{
    [SerializeField] private float _attack;
    [SerializeField] private float _defence;
    [SerializeField] private float _speed;
    [SerializeField] private float _health;
    [SerializeField] private float _regenerate;
    [SerializeField] private int _durability;
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

    public float Attack => _attack;
    public float Defence => _defence;
    public float Speed => _speed;
    public float Health => _health;
    public float Regenerate => _regenerate;
    public int Durability => _durability;
    public EquipmentSlot Slot => _slot;
    public DamageType DamageType => _damageType;

    public GameObject HeldPrefab => _heldPrefab;
    public Vector3 HeldPosition => _heldPosition;
    public Vector3 HeldEulerAngles => _heldEulerAngles;
    public Vector3 HeldScale => _heldScale;

    public new bool IsStackable => false;
}
