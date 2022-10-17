using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WallTower : MonoBehaviour
{
    [SerializeField] private EnemySpawner spawner;
    [SerializeField] private GameObject projectile;
    [SerializeField] private float fireRate;
    //[SerializeField] private int damage;

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
        if (state == GameState.EnemiesActive)
            StartCoroutine(Firing());
        else 
            StopCoroutine(Firing());
    }

    private IEnumerator Firing()
    {
        while (true)
        {
            yield return new WaitForSeconds(fireRate);
            Fire();   
        }
    }

    private void Fire()
    {
        Transform closestEnemy = spawner.FindClosestEnemy(transform.position);
        
        Transform t = transform;
        TowerProjectile spawnedProjectile = Instantiate(projectile, t.position, Quaternion.identity, t).GetComponent<TowerProjectile>();
        spawnedProjectile.Spawned(closestEnemy);
    }
}
