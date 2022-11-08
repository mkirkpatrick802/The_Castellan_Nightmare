using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : Spawner
{ 
    [SerializeField] private WallController wall;
    private GameManager _manager;

    private void Awake()
    {
        _manager = GameManager.Instance;
    }

    protected override void Spawn()
    {
        float yTransform = spawnZoneCenter.y;
        float spawnX = spawnZone.x / 2;
        float spawnY = spawnZone.y;
        for (int i = 0; i < numberToSpawn; i++)
        {
            EnemyController spawned = Instantiate(toSpawn, new Vector3(Random.Range(spawnX * -1, spawnX), Random.Range((spawnY * -1) + yTransform, spawnY + yTransform), 0), Quaternion.identity, transform).GetComponent<EnemyController>();
            spawned.SetTarget(wall.targets);
            spawned.SetSpawner(this);
            _spawned.Add(spawned.transform);
        }
    }

    public void EnemyDestroyed(EnemyController destroyed)
    {
        _spawned.Remove(destroyed.transform);
        
        if(_spawned.Count == 0)
            _manager.UpdateGameState(GameState.StartWave);
    }

    public EnemyController FindClosestEnemy(Vector2 pos)
    {
        Transform closest = null;
        float minDistance = Mathf.Infinity;

        foreach (Transform enemy in _spawned)
        {
            float dist = Vector2.Distance(enemy.position, pos);
            if (dist < minDistance)
            {
                closest = enemy.transform;
                minDistance = dist;
            }
        }

        return closest.GetComponent<EnemyController>();
    }
}
