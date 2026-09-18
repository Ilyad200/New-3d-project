using System;
using UnityEngine;

public interface IItem
{
    public string Id { get; }
    public string Name { get; }
    public string Description { get; }
    public ItemType Type { get; }
    public Sprite Icon { get; }
    public bool IsStackable { get; }
}
