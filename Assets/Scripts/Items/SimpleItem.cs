using System;
using UnityEngine;

[Serializable]
public class SimpleItem : IItem
{
    protected SimpleItemSO data;

    protected ItemType _type;

    public string Id => data.Id;
    public string Name => data.Name;
    public string Description => data.Description;
    public Sprite Icon => data.Icon;
    public ItemType Type => ItemType.Material;
    public bool IsStackable => data.IsStackable;

    public SimpleItem(SimpleItemSO data)
    {
        this.data = data;
    }
}
