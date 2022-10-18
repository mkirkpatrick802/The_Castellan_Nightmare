using System.Collections;
using UnityEngine;

public class TowerProjectile : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float aliveTime;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void Spawned()
    {
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

