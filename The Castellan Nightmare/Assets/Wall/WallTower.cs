using System.Collections;
using UnityEngine;

public class WallTower : Upgrade
{
    [Header("Tower Settings")]
    [SerializeField] private EnemySpawner spawner;
    [SerializeField] private GameObject projectile;

    [Header("Projectile Settings")]
    [SerializeField] private float fireRate;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float damage;
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
        if (state == GameState.EnemiesActive)
        {
            _lastCoroutine = StartCoroutine(Firing());
        }
        else
        {
            if (_lastCoroutine == null) return;
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
        EnemyController closestEnemy = spawner.FindClosestEnemy(transform.position);
        Transform t = transform;
        Vector2 towerPosition = t.position;
        Vector2 targetPosition = closestEnemy.transform.position;

        float distanceToEnemy = Vector2.Distance(targetPosition, towerPosition);
        float timeToHit = distanceToEnemy / projectileSpeed;
        Vector2 targetVelocity = (closestEnemy.Velocity * timeToHit);
        Vector2 finalPosition = targetPosition + targetVelocity;

        Vector2 direction = finalPosition - towerPosition;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        TowerProjectile spawnedProjectile = Instantiate(projectile, towerPosition, Quaternion.AngleAxis(angle - 90, Vector3.forward)).GetComponent<TowerProjectile>();
        spawnedProjectile.Spawned(damage, projectileSpeed);
    }

    protected override void UpgradeSystem()
    {
        if (Coins.coins < upgradeCost) return;
        base.UpgradeSystem();
        damage += Scaler(damage);
        projectileSpeed += Scaler(projectileSpeed);
        fireRate -= Scaler(fireRate);
        print(level + " " + damage + " " + projectileSpeed + " " + fireRate);
    }

    protected override float Scaler(float value)
    {
        return value * Mathf.Pow(levelScaling, level);
    }
}

