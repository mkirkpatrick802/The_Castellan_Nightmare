using Pathfinding;
using System.Collections.Generic;
using UnityEngine;

public class AllyController : MonoBehaviour
{
    private Transform _target;
    private AIDestinationSetter _setter;
    private HealthHolder _healthHolder;
    //private 


    private void Awake()
    {
        _setter = GetComponent<AIDestinationSetter>();
    }

    private void OnEnable()
    {
        EnemySpawner.allEnemiesSpawned += StartCombat;
        EnemySpawner.enemyKilled += EnemyKilled;
    }

    private void OnDisable()
    {
        EnemySpawner.allEnemiesSpawned -= StartCombat;
        EnemySpawner.enemyKilled -= EnemyKilled;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy")) return;


    }

    private void StartCombat(List<Transform> enemies)
    {
        Transform closestEnemy = null;
        float minDistance = Mathf.Infinity;

        foreach (Transform enemy in enemies)
        {
            EnemyController controller = enemy.GetComponent<EnemyController>();
            if (controller.IsInCombat) continue;

            float dist = Vector2.Distance(enemy.position, transform.position);
            if (dist < minDistance)
            {
                closestEnemy = enemy.transform;
                minDistance = dist;
            }
        }

        if (!closestEnemy) return;
        _target = closestEnemy;
        _setter.target = _target;
        _target.GetComponent<EnemyController>().AttackAlly(transform);
    }

    private void EnemyKilled(EnemyData data)
    {
        if (_target != data.killedEnemy) return;

        _target = null;
        StartCombat(data.newEnemyList);
    }
}
