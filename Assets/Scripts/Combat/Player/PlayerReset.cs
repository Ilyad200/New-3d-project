using System.Collections.Generic;
using UnityEngine;

public class PlayerReset : MonoBehaviour
{
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
        CharacterController controller = GetComponent<CharacterController>();

        if (controller != null)
        {
            controller.enabled = false; // Отключаем, чтобы не перезаписал позицию
            transform.position = new Vector3(0, 1, 0);
            controller.enabled = true;
        }
        else
        {
            transform.position = new Vector3(0, 1, 0);
        }

        _healthProvider.Revival(_playerStats.GetStatValue(StatType.Health));
    }
}