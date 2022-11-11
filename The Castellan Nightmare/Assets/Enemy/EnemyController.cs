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
    private EnemyPathAI _path;
    private EnemySpawner _spawner;
    private bool _quitting;
    private bool _isInCombat = false;
    
    public Vector2 Velocity { get => _path.velocity; private set { } }

    public bool IsInCombat { get => _isInCombat; private set { } }

    private void Awake()
    {
        _path = GetComponent<EnemyPathAI>();
        _setter = GetComponent<AIDestinationSetter>();
    }

    public void SetSpawner(EnemySpawner spawner)
    {
        _spawner = spawner;
    }

    public void SetTarget(List<Transform> wallTargets)
    {
        Transform closestTarget = null;
        float smallestDistance = 0;

        foreach (Transform target in wallTargets)
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

    public void AttackAlly(Transform ally)
    {
        _isInCombat = true;
        _setter.target = ally;
    }

    public void TakeDamage(float takeDamage)
    {
        health -= damage;
        if(health <= 0) Destroy(gameObject);
    }

    public void TargetReached()
    {
        //print(gameObject.name + " Attacking " + _setter.target + "!!!");
        _isInCombat = true;
        StartCoroutine(Attack(_setter.target));
    }

    private IEnumerator Attack(Transform target)
    {
        while (true)
        {
            if (!target.CompareTag("Wall")) yield return null;
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
