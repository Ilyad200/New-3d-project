using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private float _radius;
    [SerializeField] private int _maxEnemyCount;
    [SerializeField] private float _respawnDelay;

    private void Start()
    {
        if (_enemyPrefab == null) return;

        for (int i = 0; i < _maxEnemyCount; i++)
        {
            var enemyInst = Instantiate(_enemyPrefab, GetRandomSpawnPosition(), Quaternion.identity, transform);
            var enemy = enemyInst.GetComponent<BaseEnemy>();
            enemy.Revive(this);
        }
    }

    public void OnEnemyDied(BaseEnemy enemy)
    {
        enemy.gameObject.SetActive(false);
        StartCoroutine(RespawnAfterDelay(enemy));
    }

    private IEnumerator RespawnAfterDelay(BaseEnemy enemy)
    {
        yield return new WaitForSeconds(_respawnDelay);

        enemy.transform.SetPositionAndRotation(GetRandomSpawnPosition(), Quaternion.identity);
        enemy.gameObject.SetActive(true);
        enemy.Revive(this);
    }

    private Vector3 GetRandomSpawnPosition()
    {
        var offset = Random.onUnitCircle * _radius;
        return transform.position + new Vector3(offset.x, 0, offset.y);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}
