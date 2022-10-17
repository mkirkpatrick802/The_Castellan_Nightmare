using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Vector2 spawnZone;
    [SerializeField] private int numberToSpawn;
    [SerializeField] private GameObject enemyToSpawn;
    [SerializeField] private WallController wall;
    private List<Transform> _enemiesSpawned = new List<Transform>();
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

    private void Update()
    {
        if (_enemiesSpawned.Count == 0)
        {
            _manager.UpdateGameState(GameState.SpawnEnemies);
        }
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
            spawned.SetTarget(wall.targets);
            spawned.SetSpawner(this);
            _enemiesSpawned.Add(spawned.transform);
        }
        
        _manager.UpdateGameState(GameState.EnemiesActive);
    }

    public void EnemyDestroyed(Transform destroyed)
    {
        _enemiesSpawned.Remove(destroyed);
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(spawnZone.x, spawnZone.y, 0 ));
    }
}
