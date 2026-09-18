using System.Collections.Generic;
using UnityEngine;

public class PlayerReset : MonoBehaviour
{
    [SerializeField] private Stats _stats;
    private IHealthProvider _healthProvider;
    private PlayerStats _playerStats;
    private Inventory _inventory;
    private Dictionary<StatType, float> _statsDict;

    private void Start()
    {
        _playerStats = GetComponent<PlayerStats>();
        _healthProvider = GetComponent<IHealthProvider>();
        _inventory = GetComponentInChildren<Inventory>();
    }
    public void ResetPlayer()
    {
        transform.position = new Vector3(0, 1, 0);

        _statsDict = new Dictionary<StatType, float>() {
            { StatType.Attack, _stats.Attack },
            { StatType.Defense, _stats.Defense },
            { StatType.Speed, _stats.Speed },
            { StatType.Health, _stats.HP },
            { StatType.AttackRange, _stats.AttackRange },
            { StatType.AttackSpeed, _stats.AttackSpeed },
            { StatType.Regeneration, _stats.Regeneration },
        };

        foreach (var stat in _statsDict)
        {
            _playerStats.SetStatType(stat.Key, stat.Value);
        }
        _inventory.ClearInventory();

        _healthProvider.Revival(_statsDict[StatType.Health]);
    }
}
