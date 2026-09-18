using System.Collections.Generic;
using UnityEngine;

public class EntityDrops : MonoBehaviour
{
    [SerializeField] private List<DropEntry> dropTable = new();
    [SerializeField] private List<DropEquipment> equipmentDropTable = new();
    [SerializeField] private GameObject pickupPrefab;

    private IHealthProvider health;

    private void Awake()
    {
        health = GetComponent<IHealthProvider>();
    }
    private void OnEnable()
    {
        if (health != null)
            health.OnDied += DropItems;
    }
    private void OnDisable()
    {
        health.OnDied -= DropItems;
    }
    public void DropItems()
    {
        foreach (var entry in dropTable)
        {
            if (entry.Item == null) continue;
            if (Random.value > entry.DropChance) continue;

            int qty = Random.Range(entry.MinQuantity, entry.MaxQuantity + 1);

            SimpleItem newItem = new(entry.Item);
            SpawnPickup(newItem, qty);
        }
        foreach (var entry in equipmentDropTable)
        {
            if (entry.Item == null) continue;
            if (Random.value > entry.DropChance) continue;

            int qty = Random.Range(entry.MinQuantity, entry.MaxQuantity + 1);

            Equipment newItem = new(entry.Item);
            SpawnPickup(newItem, qty);
        }
    }

    private void SpawnPickup(IItem item, int quantity)
    {
        Vector3 spawnPos = transform.position + Vector3.forward;

        var pickup = Instantiate(pickupPrefab, spawnPos, Quaternion.identity);
        pickup.GetComponent<WorldPickup>().Setup(item, quantity);
    }
}
