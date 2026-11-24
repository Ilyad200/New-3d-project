using System.Collections.Generic;
using UnityEngine;

public class PlayerReset : MonoBehaviour
{
    public Stats Stats { get; set; }

    private IHealthProvider _healthProvider;
    private IHaveStats _playerStats;
    private Inventory _inventory;

    private void Start()
    {
        _playerStats = GetComponent<IHaveStats>();
        _healthProvider = GetComponent<IHealthProvider>();
        _inventory = GetComponentInChildren<Inventory>();
    }
    public void ResetPlayer()
    {
        transform.position = new Vector3(0, 1, 0);

        //_statsDict = new Dictionary<StatType, float>() {
        //    { StatType.Attack, _stats.Attack },
        //    { StatType.Defense, _stats.Defense },
        //    { StatType.Speed, _stats.Speed },
        //    { StatType.Health, _stats.HP },
        //    { StatType.AttackRange, _stats.AttackRange },
        //    { StatType.AttackSpeed, _stats.AttackSpeed },
        //    { StatType.Regeneration, _stats.Regeneration },
        //};
        foreach (var s in Stats.StatList)
        {
            _playerStats.SetStatType(s.StatType, s.Value);
        }

        _inventory.ClearInventory();
        if (TryGetComponent<PlayerEquipmentVisuals>(out var visuals)) visuals.ClearAll();

        _healthProvider.Revival(_playerStats.Get(StatType.Health));
    }
}