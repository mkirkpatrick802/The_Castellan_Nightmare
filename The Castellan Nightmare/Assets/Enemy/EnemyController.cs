using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float attackTime;
    [SerializeField] private int health;
    private AIDestinationSetter _setter;
    private EnemySpawner _spawner;
    private bool _quitting;
    
    private void Awake()
    {
        _setter = GetComponent<AIDestinationSetter>();
    }

    public void SetSpawner(EnemySpawner spawner)
    {
        _spawner = spawner;
    }
    
    public void SetTarget(List<Transform> newTarget)
    {
        Transform closestTarget = null;
        float smallestDistance = 0;
        foreach (var target in newTarget)
        {
            var testDistance = Vector2.Distance(transform.position, target.position);
            if (!closestTarget) {closestTarget = target; smallestDistance = testDistance;}
            if (!(testDistance < smallestDistance)) continue;
            
            closestTarget = target;
            smallestDistance = testDistance;
        }
        
        if(!closestTarget) print("Target Null");
        _setter.target = closestTarget;
    }

    public void TakeDamage(int takeDamage)
    {
        health -= damage;
        if(health <= 0) Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Wall")) return;
        
        StartCoroutine(Attack());
    }

    private IEnumerator Attack()
    {
        while (true)
        {
            yield return new WaitForSeconds(attackTime);
            WallHealth.Health -= damage;
            //print("Wall Health is Currently: " + WallHealth.Health);
        }
        
        // ReSharper disable once IteratorNeverReturns
    }

    private void OnApplicationQuit()
    {
        _quitting = true;
    }

    private void OnDestroy()
    {
        if (_quitting) return;
        if(_spawner)
            _spawner.EnemyDestroyed(this);
    }
}
