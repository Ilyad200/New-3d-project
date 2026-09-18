using UnityEngine;

[CreateAssetMenu(fileName = "SimpleItem", menuName = "Game/TestItem")]
public class SimpleItemSO : ScriptableObject
{
    [SerializeField] protected string _id;
    [SerializeField] protected string _name;
    [SerializeField] protected string _description;
    [SerializeField] protected Sprite _icon;
    [SerializeField] protected bool _isStackable;

    public string Id => _id;
    public string Name => _name;
    public string Description => _description;
    public Sprite Icon => _icon;
    public bool IsStackable => _isStackable;
}
