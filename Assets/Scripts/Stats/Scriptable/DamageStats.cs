using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DamageStats", menuName = "Game/Stats/DamageStats")]
public class DamageStats : ScriptableObject
{
    [SerializeField]
    public Dictionary<StatId, float> Stats;
}
