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
    public float Attack => _attack;
    public float Defence => _defence;
    public float Speed => _speed;
    public float Health => _health;
    public float Regenerate => _regenerate;
    public int Durability => _durability;
    public EquipmentSlot Slot => _slot;
    public new bool IsStackable => false;
}
