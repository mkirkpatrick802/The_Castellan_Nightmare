using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Vector2 spawnZone;
    [SerializeField] private int numberToSpawn;
    [SerializeField] private GameObject enemyToSpawn;
    [SerializeField] private WallController wall;
    private List<EnemyController> _enemiesSpawned = new List<EnemyController>();
    private GameManager _manager;

    private void Awake()
    {
        _manager = GameManager.Instance;
    }

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
            EnemyController spawned = Instantiate(enemyToSpawn, new Vector3(Random.Range(spawnX * -1, spawnX), Random.Range((spawnY * -1) + yTransform, spawnY + yTransform), 0), Quaternion.identity, transform).GetComponent<EnemyController>();
            spawned.SetTarget(wall.targets);
            spawned.SetSpawner(this);
            _enemiesSpawned.Add(spawned);
        }
    }

    public void EnemyDestroyed(EnemyController destroyed)
    {
        _enemiesSpawned.Remove(destroyed);
        
        if(_enemiesSpawned.Count == 0)
            _manager.UpdateGameState(GameState.SpawnEnemies);
    }

    public EnemyController FindClosestEnemy(Vector2 pos)
    {
        Transform closest = null;
        float minDistance = Mathf.Infinity;
        foreach (EnemyController enemy in _enemiesSpawned)
        {
            float dist = Vector2.Distance(enemy.transform.position, pos);
            if (dist < minDistance)
            {
                closest = enemy.transform;
                minDistance = dist;
            }
        }

        return closest.GetComponent<EnemyController>();
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(spawnZone.x, spawnZone.y, 0 ));
    }
}
