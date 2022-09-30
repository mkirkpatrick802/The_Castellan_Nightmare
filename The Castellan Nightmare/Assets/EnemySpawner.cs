using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Vector2 spawnZone;
    [SerializeField] private int numberToSpawn;
    [SerializeField] private GameObject enemyToSpawn;
    [SerializeField] private WallController wall;
    
    private void OnEnable()
    {
        GameManager.gameStateChanged += GameStateChanged;
    }

    private void OnDisable()
    {
        GameManager.gameStateChanged -= GameStateChanged;
    }

    private void GameStateChanged(GameState state)
    {
        if(state != GameState.SpawnEnemies) return;
        SpawnEnemies();
    }

    private void SpawnEnemies()
    {
        var yTransform = transform.position.y;
        var spawnX = spawnZone.x / 2;
        var spawnY = spawnZone.y;
        for (var i = 0; i < numberToSpawn; i++)
        {
            var spawned = Instantiate(enemyToSpawn, new Vector3(Random.Range(spawnX * -1, spawnX), Random.Range((spawnY * -1) + yTransform, spawnY + yTransform), 0), Quaternion.identity, transform).GetComponent<EnemyController>();
            spawned.SetTarget(wall.targets[Random.Range(0, wall.targets.Count)]);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(spawnZone.x, spawnZone.y, 0 ));
    }
}
