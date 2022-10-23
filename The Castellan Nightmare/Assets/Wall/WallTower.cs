using System.Collections;
using UnityEngine;

public class WallTower : Upgrade
{
    [Header("Tower Settings")]
    [SerializeField] private EnemySpawner spawner;
    [SerializeField] private GameObject projectile;
    [SerializeField] private float fireRate;
    private Coroutine _lastCoroutine = null;

    protected override void OnEnable()
    {
        base.OnEnable();
        GameManager.gameStateChanged += GameStateChanged;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        GameManager.gameStateChanged -= GameStateChanged;
    }

    private void GameStateChanged(GameState state)
    {
        //print(state);
        if (state == GameState.EnemiesActive)
        {
            //print("Start Firing");
            _lastCoroutine = StartCoroutine(Firing());
        }
        else
        {
            if (_lastCoroutine == null) return;
            //print("Stop Firing");
            StopCoroutine(_lastCoroutine);
        }
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
        Vector3 tPos = t.position;
        Vector2 direction = closestEnemy.position - tPos;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        TowerProjectile spawnedProjectile = Instantiate(projectile, tPos, Quaternion.AngleAxis(angle - 90, Vector3.forward)).GetComponent<TowerProjectile>();
        spawnedProjectile.Spawned();
    }

    protected override void UpgradeSystem()
    {
        if (Coins.coins < upgradeCost) return;
        base.UpgradeSystem();
    }
}

