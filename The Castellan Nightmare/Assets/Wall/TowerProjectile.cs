using System.Collections;
using UnityEngine;

public class TowerProjectile : MonoBehaviour
{
    [SerializeField] private float aliveTime;
    private float damage;
    private float projectileSpeed;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void Spawned(float damage, float projectileSpeed)
    {
        this.damage = damage;
        this.projectileSpeed = projectileSpeed;
        _rb.velocity = transform.up * projectileSpeed;
        StartCoroutine(DeathTimer());
    }

    private IEnumerator DeathTimer()
    {
        yield return new WaitForSeconds(aliveTime);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Enemy")) return;
        col.GetComponent<EnemyController>().TakeDamage(damage);
        StopCoroutine(DeathTimer());
        Destroy(gameObject);
    }
}

