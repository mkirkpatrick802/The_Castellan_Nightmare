using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spawner : MonoBehaviour
{ 
    [SerializeField] protected Vector2 spawnZone;
    [SerializeField] protected Vector2 spawnZoneCenter;
    [SerializeField] protected int numberToSpawn;
    [SerializeField] protected GameObject toSpawn;
    protected List<Transform> _spawned = new List<Transform>();

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
        if (state != GameState.StartWave) return;
        Spawn();
    }

    protected abstract void Spawn();

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(spawnZoneCenter, new Vector3(spawnZone.x, spawnZone.y, 0));
    }
}
