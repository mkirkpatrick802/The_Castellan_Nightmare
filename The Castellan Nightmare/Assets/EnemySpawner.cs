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
        float yTransform = transform.position.y;
        float spawnX = spawnZone.x / 2;
        float spawnY = spawnZone.y;
        for (int i = 0; i < numberToSpawn; i++)
        {
            Instantiate(enemyToSpawn, new Vector3(Random.Range(spawnX * -1, spawnX), Random.Range((spawnY * -1) + yTransform, spawnY + yTransform), 0), Quaternion.identity, transform);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(spawnZone.x, spawnZone.y, 0 ));
    }
}
