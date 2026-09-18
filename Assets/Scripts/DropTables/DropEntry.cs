using UnityEngine;

[CreateAssetMenu(menuName = "Game/Drop Entry/Base Entry", fileName = "DropEntry_")]
public class DropEntry : ScriptableObject
{
    [SerializeField] private SimpleItemSO _item;

    [SerializeField][Range(0f, 1f)] private float _dropChance = 1f;
    [SerializeField][Min(1)] private int _minQuantity = 1;
    [SerializeField][Min(1)] private int _maxQuantity = 1;

    public SimpleItemSO Item => _item;
    public float DropChance => _dropChance;
    public int MinQuantity => _minQuantity;
    public int MaxQuantity => _maxQuantity;
}
