using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using static UnityEngine.EventSystems.EventTrigger;

public class AllyController : MonoBehaviour
{
    private Transform _target;

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
        closestEnemy.GetComponent<EnemyController>().AttackAlly(transform);
    }

    private void EnemyKilled(EnemyData data)
    {
        if(_target != data.killedEnemy) return;

        _target = null;
        StartCombat(data.newEnemyList);
    }
}
